using Application.Brokers.Queries.GetTopBrokersByListings;
using AutoMapper;
using System.Text.Json;
using System.Threading;
using Property = Domain.Entities.Property;

namespace Infrastructure.Clients.PropertyApi;

public sealed class PropertyApiClient(HttpClient httpClient, IMapper mapper) : IPropertyApiClient
{
    // Due to the limitation of the API 25 is the maximum supported page size
    private const int PageSize = 25;

    private readonly HttpClient _httpClient = httpClient;

    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<Property>> GetPropertiesForSaleAsync(PropertyFilter filter, CancellationToken cancellationToken)
    {
        int page = 1;
        var url = BuildUrl(filter);


        do
        {
            url += $"&page={page}";
            using var response = await _httpClient.GetAsync($"/shows?page={page}", cancellationToken);



        } while (true);





        throw new NotImplementedException();
    }

    private static string BuildUrl(PropertyFilter filter)
    {
        var url = $"?type=koop&zo=/{Uri.EscapeDataString(filter.CityName)}";

        if (filter.HasGarden)
        {
            url += "/tuin";
        }

        url += $"&pagesize={PageSize}";
        return url;
    }
}