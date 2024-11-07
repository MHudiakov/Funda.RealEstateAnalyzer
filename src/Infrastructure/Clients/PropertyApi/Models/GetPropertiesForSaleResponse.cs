using System.Text.Json.Serialization;

namespace Infrastructure.Clients.PropertyApi.Models;

internal class GetPropertiesForSaleResponse
{
    [JsonPropertyName("Objects")]
    internal IEnumerable<PropertyDto> Properties { get; set; }

    internal Paging Paging { get; set; }
}