using DataMammoth.Api;

namespace DataMammoth;

/// <summary>
/// DataMammoth C# SDK -- official async client for the DataMammoth API v2.
/// <para>
/// Usage:
/// <code>
/// var dm = new DataMammothClient("dm_live_...");
/// var servers = await dm.Servers.ListAsync();
/// var server = await dm.Servers.GetAsync("srv_abc123");
/// </code>
/// </para>
/// </summary>
public class DataMammothClient
{
    private readonly DmHttpClient _httpClient;

    public ServersApi Servers { get; }
    public ProductsApi Products { get; }
    public BillingApi Billing { get; }
    public SupportApi Support { get; }
    public AccountApi Account { get; }
    public AdminApi Admin { get; }
    public AffiliateApi Affiliate { get; }
    public WebhooksApi Webhooks { get; }
    public TasksApi Tasks { get; }
    public ZonesApi Zones { get; }

    /// <summary>
    /// Create a new DataMammoth client with default settings.
    /// </summary>
    /// <param name="apiKey">Your DataMammoth API key (dm_live_... or dm_test_...)</param>
    /// <param name="baseUrl">API base URL (override for self-hosted or staging)</param>
    /// <param name="maxRetries">Max retry attempts for 429/5xx responses</param>
    /// <param name="timeout">Request timeout</param>
    public DataMammothClient(
        string apiKey,
        string baseUrl = "https://app.datamammoth.com/api/v2",
        int maxRetries = 3,
        TimeSpan? timeout = null)
    {
        _httpClient = new DmHttpClient(apiKey, baseUrl, maxRetries, timeout);

        Servers = new ServersApi(_httpClient);
        Products = new ProductsApi(_httpClient);
        Billing = new BillingApi(_httpClient);
        Support = new SupportApi(_httpClient);
        Account = new AccountApi(_httpClient);
        Admin = new AdminApi(_httpClient);
        Affiliate = new AffiliateApi(_httpClient);
        Webhooks = new WebhooksApi(_httpClient);
        Tasks = new TasksApi(_httpClient);
        Zones = new ZonesApi(_httpClient);
    }

    /// <summary>Access the underlying HTTP client for custom requests.</summary>
    public DmHttpClient GetHttpClient() => _httpClient;
}
