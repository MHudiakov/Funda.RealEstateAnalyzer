using Application.Brokers.Queries.GetTopBrokersByListings;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BrokersController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BrokerDto>>> GetTopBrokersByListings([FromQuery] GetTopBrokersByListingsQuery query)
    {
        return await Mediator.Send(query);
    }
}