using System.Text.Json.Serialization;

namespace Infrastructure.Clients.PropertyApi.Models;

public class Paging
{
    [JsonPropertyName("AantalPaginas")]
    public int TotalPages { get; init; }
}