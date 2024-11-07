using Application.Brokers.Queries.GetTopBrokersByListings;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BrokersController(IPropertyApiClient propertyApiClient) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Property>>> GetTopBrokersByListings([FromQuery] GetTopBrokersByListingsQuery query)
    {
        IEnumerable<Property> t = await propertyApiClient.GetPropertiesForSaleAsync(new PropertyFilter("Amsterdam", false),
            CancellationToken.None);

        return Ok(t);
    }
}