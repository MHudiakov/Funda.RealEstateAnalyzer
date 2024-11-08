namespace Application.Brokers.Queries.GetTopBrokersByListings;

public class BrokerDto
{
    public int Id { get; set; }

    public required string Name { get; set; }
    
    public int PropertiesCount { get; set; }
}