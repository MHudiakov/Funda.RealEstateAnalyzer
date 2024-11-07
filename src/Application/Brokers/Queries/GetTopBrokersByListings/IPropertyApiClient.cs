using Domain.Entities;

namespace Application.Brokers.Queries.GetTopBrokersByListings;

public interface IPropertyApiClient
{
    Task<IEnumerable<Property>> GetPropertiesForSaleAsync(PropertyFilter filter, CancellationToken cancellationToken);
}