using MediatR;

namespace Application.Brokers.Queries.GetTopBrokersByListings;

public class GetTopBrokersByListingsQuery : IRequest<List<BrokerDto>>
{
    public int BrokersNumber { get; init; } = 10;

    public string CityName { get; init; } = "Amsterdam";

    public bool HasGarden { get; init; }

}