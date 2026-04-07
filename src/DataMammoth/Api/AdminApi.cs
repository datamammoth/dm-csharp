using System.Text.Json;

namespace DataMammoth.Api;

/// <summary>Admin API -- user management, roles, tenants, audit log, dashboard.</summary>
public class AdminApi
{
    private readonly DmHttpClient _client;
    internal AdminApi(DmHttpClient client) => _client = client;

    // -- Users
    public Task<JsonDocument> ListUsersAsync(Dictionary<string, string>? query = null, CancellationToken ct = default) => _client.GetAsync("/admin/users", query, ct);
    public Task<JsonDocument> GetUserAsync(string id, CancellationToken ct = default) => _client.GetAsync($"/admin/users/{id}", ct: ct);
    public Task<JsonDocument> UpdateUserAsync(string id, object parameters, CancellationToken ct = default) => _client.PatchAsync($"/admin/users/{id}", parameters, ct);

    // -- Roles
    public Task<JsonDocument> ListRolesAsync(CancellationToken ct = default) => _client.GetAsync("/admin/roles", ct: ct);
    public Task<JsonDocument> GetRoleAsync(string id, CancellationToken ct = default) => _client.GetAsync($"/admin/roles/{id}", ct: ct);
    public Task<JsonDocument> CreateRoleAsync(object parameters, CancellationToken ct = default) => _client.PostAsync("/admin/roles", parameters, ct);
    public Task<JsonDocument> UpdateRoleAsync(string id, object parameters, CancellationToken ct = default) => _client.PatchAsync($"/admin/roles/{id}", parameters, ct);
    public Task<JsonDocument> DeleteRoleAsync(string id, CancellationToken ct = default) => _client.DeleteAsync($"/admin/roles/{id}", ct);

    // -- Tenants
    public Task<JsonDocument> ListTenantsAsync(Dictionary<string, string>? query = null, CancellationToken ct = default) => _client.GetAsync("/admin/tenants", query, ct);
    public Task<JsonDocument> GetTenantAsync(string id, CancellationToken ct = default) => _client.GetAsync($"/admin/tenants/{id}", ct: ct);
    public Task<JsonDocument> UpdateTenantAsync(string id, object parameters, CancellationToken ct = default) => _client.PatchAsync($"/admin/tenants/{id}", parameters, ct);

    // -- Admin-scoped lists
    public Task<JsonDocument> ListServersAsync(Dictionary<string, string>? query = null, CancellationToken ct = default) => _client.GetAsync("/admin/servers", query, ct);
    public Task<JsonDocument> ListInvoicesAsync(Dictionary<string, string>? query = null, CancellationToken ct = default) => _client.GetAsync("/admin/invoices", query, ct);
    public Task<JsonDocument> ListTicketsAsync(Dictionary<string, string>? query = null, CancellationToken ct = default) => _client.GetAsync("/admin/tickets", query, ct);
    public Task<JsonDocument> GetTicketAsync(string id, CancellationToken ct = default) => _client.GetAsync($"/admin/tickets/{id}", ct: ct);
    public Task<JsonDocument> ListLeadsAsync(Dictionary<string, string>? query = null, CancellationToken ct = default) => _client.GetAsync("/admin/leads", query, ct);

    // -- Audit & Dashboard
    public Task<JsonDocument> ListAuditLogAsync(Dictionary<string, string>? query = null, CancellationToken ct = default) => _client.GetAsync("/admin/audit-log", query, ct);
    public Task<JsonDocument> DashboardStatsAsync(CancellationToken ct = default) => _client.GetAsync("/admin/dashboard/stats", ct: ct);

    // -- Masquerade
    public Task<JsonDocument> MasqueradeAsync(string userId, CancellationToken ct = default) => _client.PostAsync($"/admin/masquerade/{userId}", ct: ct);
}
