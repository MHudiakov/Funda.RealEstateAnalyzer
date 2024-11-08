using System.Text.Json.Serialization;

namespace Infrastructure.Clients.PropertyApi.Models;

public class GetPropertiesForSaleResponse
{
    [JsonPropertyName("Objects")]
    public required IEnumerable<PropertyDto> Properties { get; set; }

    public required Paging Paging { get; set; }
}