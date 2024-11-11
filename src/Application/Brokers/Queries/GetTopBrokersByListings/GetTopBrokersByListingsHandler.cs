using MediatR;

namespace Application.Brokers.Queries.GetTopBrokersByListings;

public class GetTopBrokersByListingsHandler(IPropertyApiClient propertyApiClient)
    : IRequestHandler<GetTopBrokersByListingsQuery, List<BrokerDto>>
{
    public async Task<List<BrokerDto>> Handle(GetTopBrokersByListingsQuery request, CancellationToken cancellationToken)
    {
        var filter = new PropertyFilter(request.CityName, request.HasGarden);
        var properties = await propertyApiClient.GetPropertiesForSaleAsync(filter, cancellationToken);

        var brokers = properties
            .GroupBy(p => new { p.BrokerId, p.BrokerName })
            .Select(group => new BrokerDto
            {
                Id = group.Key.BrokerId,
                Name = group.Key.BrokerName,
                PropertiesCount = group.Count()
            })
            .OrderByDescending(p => p.PropertiesCount)
            .Take(request.BrokersNumber)
            .ToList();

        return brokers;
    }
}