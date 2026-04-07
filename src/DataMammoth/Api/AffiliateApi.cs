using System.Text.Json;

namespace DataMammoth.Api;

/// <summary>Affiliate API -- referral links, commissions, payouts, and materials.</summary>
public class AffiliateApi
{
    private readonly DmHttpClient _client;
    internal AffiliateApi(DmHttpClient client) => _client = client;

    public Task<JsonDocument> MeAsync(CancellationToken ct = default) => _client.GetAsync("/affiliate/me", ct: ct);
    public Task<JsonDocument> ListCommissionsAsync(Dictionary<string, string>? query = null, CancellationToken ct = default) => _client.GetAsync("/affiliate/commissions", query, ct);
    public Task<JsonDocument> ListReferralsAsync(Dictionary<string, string>? query = null, CancellationToken ct = default) => _client.GetAsync("/affiliate/referrals", query, ct);
    public Task<JsonDocument> RequestPayoutAsync(object? parameters = null, CancellationToken ct = default) => _client.PostAsync("/affiliate/payout-request", parameters, ct);
    public Task<JsonDocument> ListMaterialsAsync(CancellationToken ct = default) => _client.GetAsync("/affiliate/materials", ct: ct);
}
