using System.Text.Json;

namespace DataMammoth.Api;

/// <summary>Support API -- tickets, replies, departments, and knowledge base.</summary>
public class SupportApi
{
    private readonly DmHttpClient _client;
    internal SupportApi(DmHttpClient client) => _client = client;

    public Task<JsonDocument> ListTicketsAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/tickets", query, ct);
    public Task<JsonDocument> GetTicketAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/tickets/{id}", ct: ct);
    public Task<JsonDocument> CreateTicketAsync(object parameters, CancellationToken ct = default)
        => _client.PostAsync("/tickets", parameters, ct);
    public Task<JsonDocument> ReplyToTicketAsync(string id, object parameters, CancellationToken ct = default)
        => _client.PostAsync($"/tickets/{id}/replies", parameters, ct);
    public Task<JsonDocument> TicketFeedbackAsync(string id, object parameters, CancellationToken ct = default)
        => _client.PostAsync($"/tickets/{id}/feedback", parameters, ct);

    public Task<JsonDocument> ListDepartmentsAsync(CancellationToken ct = default)
        => _client.GetAsync("/tickets/departments", ct: ct);

    public Task<JsonDocument> ListArticlesAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/kb/articles", query, ct);
    public Task<JsonDocument> GetArticleAsync(string slug, CancellationToken ct = default)
        => _client.GetAsync($"/kb/articles/{slug}", ct: ct);
}
