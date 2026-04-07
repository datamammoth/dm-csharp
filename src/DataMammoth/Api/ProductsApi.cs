using System.Text.Json;

namespace DataMammoth.Api;

/// <summary>Products API -- browse hosting plans, addons, and pricing.</summary>
public class ProductsApi
{
    private readonly DmHttpClient _client;
    internal ProductsApi(DmHttpClient client) => _client = client;

    public Task<JsonDocument> ListAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/products", query, ct);

    public Task<JsonDocument> GetAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/products/{id}", ct: ct);

    public Task<JsonDocument> AddonsAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/products/{id}/addons", ct: ct);

    public Task<JsonDocument> OptionsAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/products/{id}/options", ct: ct);

    public Task<JsonDocument> PricingAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/products/{id}/pricing", ct: ct);

    public Task<JsonDocument> CategoriesAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/categories", query, ct);
}
