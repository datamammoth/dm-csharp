using System.Text.Json;

namespace DataMammoth.Api;

/// <summary>Billing API -- invoices, subscriptions, balance, and payment methods.</summary>
public class BillingApi
{
    private readonly DmHttpClient _client;
    internal BillingApi(DmHttpClient client) => _client = client;

    // -- Invoices
    public Task<JsonDocument> ListInvoicesAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/invoices", query, ct);
    public Task<JsonDocument> GetInvoiceAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/invoices/{id}", ct: ct);
    public Task<JsonDocument> PayInvoiceAsync(string id, object? parameters = null, CancellationToken ct = default)
        => _client.PostAsync($"/invoices/{id}/pay", parameters, ct);

    // -- Subscriptions
    public Task<JsonDocument> ListSubscriptionsAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/subscriptions", query, ct);
    public Task<JsonDocument> GetSubscriptionAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/subscriptions/{id}", ct: ct);

    // -- Balance
    public Task<JsonDocument> GetBalanceAsync(CancellationToken ct = default)
        => _client.GetAsync("/balance", ct: ct);
    public Task<JsonDocument> ListTransactionsAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/balance/transactions", query, ct);

    // -- Payment Methods
    public Task<JsonDocument> ListPaymentMethodsAsync(CancellationToken ct = default)
        => _client.GetAsync("/payment-methods", ct: ct);

    // -- Orders
    public Task<JsonDocument> ListOrdersAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/orders", query, ct);
    public Task<JsonDocument> GetOrderAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/orders/{id}", ct: ct);

    // -- Promo
    public Task<JsonDocument> ValidatePromoAsync(object parameters, CancellationToken ct = default)
        => _client.PostAsync("/promo/validate", parameters, ct);
}
