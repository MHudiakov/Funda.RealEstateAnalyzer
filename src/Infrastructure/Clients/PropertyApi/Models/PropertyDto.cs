using System.Text.Json.Serialization;

namespace Infrastructure.Clients.PropertyApi.Models;

public class PropertyDto
{
    public string Id { get; set; }

    [JsonPropertyName("MakelaarId")]
    public int BrokerId { get; set; }

    [JsonPropertyName("MakelaarNaam")]
    public string BrokerName { get; set; }
}