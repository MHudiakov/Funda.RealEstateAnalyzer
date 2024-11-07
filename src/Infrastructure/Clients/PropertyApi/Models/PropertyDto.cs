using System.Text.Json.Serialization;

namespace Infrastructure.Clients.PropertyApi.Models;

internal class PropertyDto
{
    internal string Id { get; set; }

    [JsonPropertyName("MakelaarId")]
    internal int BrokerId { get; set; }

    [JsonPropertyName("MakelaarNaam")]
    internal string BrokerName { get; set; }
}