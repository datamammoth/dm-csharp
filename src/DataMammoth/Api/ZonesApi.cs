using System.Text.Json;

namespace DataMammoth.Api;

/// <summary>Zones API -- hosting zones (regions) and available OS images.</summary>
public class ZonesApi
{
    private readonly DmHttpClient _client;
    internal ZonesApi(DmHttpClient client) => _client = client;

    public Task<JsonDocument> ListAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/zones", query, ct);

    public Task<JsonDocument> ListImagesAsync(string zoneId, Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync($"/zones/{zoneId}/images", query, ct);
}
