using System.Text.Json.Serialization;

namespace Infrastructure.Clients.PropertyApi.Models;

internal class Paging
{
    [JsonPropertyName("AantalPaginas")]
    internal int TotalPages { get; init; }
}