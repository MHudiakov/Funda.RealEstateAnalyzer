using System.Text.Json.Serialization;

namespace Infrastructure.Clients.PropertyApi.Models;

public class GetPropertiesForSaleResponse
{
    [JsonPropertyName("Objects")]
    public IEnumerable<PropertyDto> Properties { get; set; }

    public Paging Paging { get; set; }
}