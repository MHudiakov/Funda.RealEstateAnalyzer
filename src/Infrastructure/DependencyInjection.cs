using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Brokers.Queries.GetTopBrokersByListings;
using Infrastructure.Clients.PropertyApi;
using Infrastructure.Common;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddHttpClient<IPropertyApiClient, PropertyApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue<string>("FundaApiBaseUrl")!);
                client.Timeout = TimeSpan.FromMinutes(2);
            })
            .AddPolicyHandler(RetryPolicy.GetRetryPolicy());

        return services;
    }
}