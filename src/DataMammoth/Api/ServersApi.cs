using System.Text.Json;

namespace DataMammoth.Api;

/// <summary>Servers API -- provision, manage, and monitor VPS instances.</summary>
public class ServersApi
{
    private readonly DmHttpClient _client;
    internal ServersApi(DmHttpClient client) => _client = client;

    public Task<JsonDocument> ListAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/servers", query, ct);

    public Task<JsonDocument> GetAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/servers/{id}", ct: ct);

    public Task<JsonDocument> CreateAsync(object parameters, CancellationToken ct = default)
        => _client.PostAsync("/servers", parameters, ct);

    public Task<JsonDocument> UpdateAsync(string id, object parameters, CancellationToken ct = default)
        => _client.PatchAsync($"/servers/{id}", parameters, ct);

    public Task<JsonDocument> DeleteAsync(string id, CancellationToken ct = default)
        => _client.DeleteAsync($"/servers/{id}", ct);

    public Task<JsonDocument> ActionAsync(string id, string action, object? parameters = null, CancellationToken ct = default)
        => _client.PostAsync($"/servers/{id}/actions/{action}", parameters, ct);

    public Task<JsonDocument> MetricsAsync(string id, Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync($"/servers/{id}/metrics", query, ct);

    public Task<JsonDocument> EventsAsync(string id, Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync($"/servers/{id}/events", query, ct);

    public Task<JsonDocument> ConsoleAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/servers/{id}/console", ct: ct);

    // -- Snapshots
    public Task<JsonDocument> ListSnapshotsAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/servers/{id}/snapshots", ct: ct);

    public Task<JsonDocument> CreateSnapshotAsync(string id, object? parameters = null, CancellationToken ct = default)
        => _client.PostAsync($"/servers/{id}/snapshots", parameters, ct);

    public Task<JsonDocument> DeleteSnapshotAsync(string serverId, string snapId, CancellationToken ct = default)
        => _client.DeleteAsync($"/servers/{serverId}/snapshots/{snapId}", ct);

    // -- Firewall
    public Task<JsonDocument> GetFirewallAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/servers/{id}/firewall", ct: ct);

    public Task<JsonDocument> UpdateFirewallAsync(string id, object rules, CancellationToken ct = default)
        => _client.PutAsync($"/servers/{id}/firewall", rules, ct);
}
