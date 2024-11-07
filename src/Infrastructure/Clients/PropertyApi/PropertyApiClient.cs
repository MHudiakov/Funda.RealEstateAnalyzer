using Application.Brokers.Queries.GetTopBrokersByListings;
using AutoMapper;
using System.Text.Json;
using Infrastructure.Clients.PropertyApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Property = Domain.Entities.Property;

namespace Infrastructure.Clients.PropertyApi;

public sealed class PropertyApiClient(HttpClient httpClient, IMapper mapper, IMemoryCache memoryCache) : IPropertyApiClient
{
    // Due to the limitation of the API 25 is the maximum supported page size
    private const int PageSize = 25;

    private const int CacheLifetimeInMinutes = 5;

    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task<IEnumerable<Property>> GetPropertiesForSaleAsync(PropertyFilter filter, CancellationToken cancellationToken)
    {
        int page = 1;
        var properties = new List<Property>();
        var cacheKey = $"GetPropertiesForSale_{filter.CityName}_{filter.HasGarden}";

        var cachedProperties = GetCachedData(cacheKey);

        if (cachedProperties is not null)
        {
            return cachedProperties;
        }

        do
        {
            var url = BuildUrl(filter, page);
            using var response = await httpClient.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {   
                var errorMessage = $"Attempt to retrieve properties by url: {url} was not successful";
                throw new ApiException(errorMessage, response.StatusCode);
            }

            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            var getPropertiesForSaleResponse = await JsonSerializer.DeserializeAsync<GetPropertiesForSaleResponse>(stream, _options, cancellationToken);

            if (getPropertiesForSaleResponse is null || 
                !getPropertiesForSaleResponse.Properties.Any())
            {
                break;
            }

            properties.AddRange(mapper.Map<List<Property>>(getPropertiesForSaleResponse.Properties));
            page++;

        } while (true);

        CacheData(cacheKey, properties);

        return properties;
    }

    private static string BuildUrl(PropertyFilter filter, int page)
    {
        var url = $"?type=koop&zo=/{Uri.EscapeDataString(filter.CityName)}";

        if (filter.HasGarden)
        {
            url += "/tuin";
        }

        url += $"&pagesize={PageSize}&page={page}";
        return url;
    }

    public void CacheData(string key, object data)
    {
        memoryCache.Set(key, data, TimeSpan.FromMinutes(CacheLifetimeInMinutes));
    }

    public List<Property>? GetCachedData(string key)
    {
        if (memoryCache.TryGetValue(key, out var cachedData))
        {
            return cachedData as List<Property>;
        }

        return null;
    }
}