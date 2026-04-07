using System.Text.Json;

namespace DataMammoth.Api;

/// <summary>Account API -- profile, API keys, sessions, 2FA, notifications.</summary>
public class AccountApi
{
    private readonly DmHttpClient _client;
    internal AccountApi(DmHttpClient client) => _client = client;

    public Task<JsonDocument> MeAsync(CancellationToken ct = default) => _client.GetAsync("/me", ct: ct);
    public Task<JsonDocument> UpdateProfileAsync(object parameters, CancellationToken ct = default) => _client.PatchAsync("/me", parameters, ct);
    public Task<JsonDocument> ChangePasswordAsync(object parameters, CancellationToken ct = default) => _client.PostAsync("/me/change-password", parameters, ct);

    public Task<JsonDocument> ListApiKeysAsync(CancellationToken ct = default) => _client.GetAsync("/me/api-keys", ct: ct);
    public Task<JsonDocument> CreateApiKeyAsync(object parameters, CancellationToken ct = default) => _client.PostAsync("/me/api-keys", parameters, ct);
    public Task<JsonDocument> DeleteApiKeyAsync(string id, CancellationToken ct = default) => _client.DeleteAsync($"/me/api-keys/{id}", ct);

    public Task<JsonDocument> ListSessionsAsync(CancellationToken ct = default) => _client.GetAsync("/me/sessions", ct: ct);
    public Task<JsonDocument> RevokeSessionAsync(string id, CancellationToken ct = default) => _client.DeleteAsync($"/me/sessions/{id}", ct);

    public Task<JsonDocument> Get2faAsync(CancellationToken ct = default) => _client.GetAsync("/me/2fa", ct: ct);
    public Task<JsonDocument> Update2faAsync(object parameters, CancellationToken ct = default) => _client.PostAsync("/me/2fa", parameters, ct);

    public Task<JsonDocument> ListNotificationsAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/me/notifications", query, ct);
    public Task<JsonDocument> ListActivityAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/me/activity", query, ct);
}
