using Application.Brokers.Queries.GetTopBrokersByListings;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Application.UnitTests;

public class GetTopBrokersByListingsTests
{
    [Test]
    public async Task Handler_ShouldReturnExpectedTopBrokersListingsForCity()
    {
        var apiClientMock = BuildApiClientMock();

        var handler = new GetTopBrokersByListingsHandler(apiClientMock.Object);

        var query = new GetTopBrokersByListingsQuery
        {
            CityName = "Amsterdam",
            HasGarden = true,
            BrokersNumber = 3
        };

        var brokers = await handler.Handle(query, CancellationToken.None);

        brokers.Count.Should().Be(3);
        brokers[0].Id.Should().Be(1);
        brokers[0].PropertiesCount.Should().Be(3);
        brokers[1].Id.Should().Be(2);
        brokers[1].PropertiesCount.Should().Be(2);
    }

    [Test]
    public async Task WhenBrokersNumberExceedsMaximumBrokersCount_Handler_ShouldReturnMaximumBrokersCount()
    {
        var apiClientMock = BuildApiClientMock();

        var handler = new GetTopBrokersByListingsHandler(apiClientMock.Object);

        var query = new GetTopBrokersByListingsQuery
        {
            CityName = "Amsterdam",
            HasGarden = true,
            BrokersNumber = 4
        };

        var brokers = await handler.Handle(query, CancellationToken.None);

        brokers.Count.Should().Be(4);
        brokers[0].Id.Should().Be(1);
    }

    private Mock<IPropertyApiClient> BuildApiClientMock()
    {
        var apiClientMock = new Mock<IPropertyApiClient>();

        apiClientMock.Setup(x => x.GetPropertiesForSaleAsync(It.IsAny<PropertyFilter>(), CancellationToken.None))
            .ReturnsAsync(GetFakeProperties);

        return apiClientMock;
    }

    private List<Property> GetFakeProperties =>
    [
        new("Prop1", 1, "Broker1"),
        new("Prop2", 1, "Broker1"),
        new("Prop3", 1, "Broker1"),
        new("Prop4", 2, "Broker2"),
        new("Prop5", 2, "Broker2"),
        new("Prop6", 3, "Broker3"),
        new("Prop7", 4, "Broker4")
    ];
}