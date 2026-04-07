using System.Text.Json;

namespace DataMammoth.Api;

/// <summary>Webhooks API -- manage webhook endpoints, deliveries, and events.</summary>
public class WebhooksApi
{
    private readonly DmHttpClient _client;
    internal WebhooksApi(DmHttpClient client) => _client = client;

    public Task<JsonDocument> ListAsync(Dictionary<string, string>? query = null, CancellationToken ct = default) => _client.GetAsync("/webhooks", query, ct);
    public Task<JsonDocument> GetAsync(string id, CancellationToken ct = default) => _client.GetAsync($"/webhooks/{id}", ct: ct);
    public Task<JsonDocument> CreateAsync(object parameters, CancellationToken ct = default) => _client.PostAsync("/webhooks", parameters, ct);
    public Task<JsonDocument> UpdateAsync(string id, object parameters, CancellationToken ct = default) => _client.PatchAsync($"/webhooks/{id}", parameters, ct);
    public Task<JsonDocument> DeleteAsync(string id, CancellationToken ct = default) => _client.DeleteAsync($"/webhooks/{id}", ct);

    public Task<JsonDocument> ListDeliveriesAsync(string id, Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync($"/webhooks/{id}/deliveries", query, ct);
    public Task<JsonDocument> TestAsync(string id, CancellationToken ct = default) => _client.PostAsync($"/webhooks/{id}/test", ct: ct);
    public Task<JsonDocument> ListEventsAsync(CancellationToken ct = default) => _client.GetAsync("/webhooks/events", ct: ct);
}
