using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DataMammoth.Exceptions;

namespace DataMammoth;

/// <summary>
/// Low-level HTTP client with authentication, retry logic, and error mapping.
/// </summary>
public class DmHttpClient
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;
    private readonly int _maxRetries;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
    };

    public DmHttpClient(string apiKey, string baseUrl, int maxRetries = 3, TimeSpan? timeout = null)
    {
        _baseUrl = baseUrl.TrimEnd('/');
        _maxRetries = maxRetries;
        _client = new HttpClient { Timeout = timeout ?? TimeSpan.FromSeconds(30) };
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.UserAgent.ParseAdd("datamammoth-csharp/0.1.0");
    }

    public Task<JsonDocument> GetAsync(string path, Dictionary<string, string>? query = null, CancellationToken ct = default)
    {
        var url = BuildUrl(path, query);
        return ExecuteAsync(new HttpRequestMessage(HttpMethod.Get, url), ct);
    }

    public Task<JsonDocument> PostAsync(string path, object? body = null, CancellationToken ct = default)
    {
        var msg = new HttpRequestMessage(HttpMethod.Post, BuildUrl(path))
        {
            Content = JsonContent(body),
        };
        return ExecuteAsync(msg, ct);
    }

    public Task<JsonDocument> PatchAsync(string path, object? body = null, CancellationToken ct = default)
    {
        var msg = new HttpRequestMessage(HttpMethod.Patch, BuildUrl(path))
        {
            Content = JsonContent(body),
        };
        return ExecuteAsync(msg, ct);
    }

    public Task<JsonDocument> PutAsync(string path, object? body = null, CancellationToken ct = default)
    {
        var msg = new HttpRequestMessage(HttpMethod.Put, BuildUrl(path))
        {
            Content = JsonContent(body),
        };
        return ExecuteAsync(msg, ct);
    }

    public Task<JsonDocument> DeleteAsync(string path, CancellationToken ct = default)
    {
        return ExecuteAsync(new HttpRequestMessage(HttpMethod.Delete, BuildUrl(path)), ct);
    }

    // ── Internal ──────────────────────────────────────────────────

    private string BuildUrl(string path, Dictionary<string, string>? query = null)
    {
        path = path.StartsWith('/') ? path : "/" + path;
        var sb = new StringBuilder(_baseUrl).Append(path);
        if (query is { Count: > 0 })
        {
            sb.Append('?');
            foreach (var (k, v) in query)
                sb.Append(Uri.EscapeDataString(k)).Append('=').Append(Uri.EscapeDataString(v)).Append('&');
            sb.Length--; // remove trailing &
        }
        return sb.ToString();
    }

    private static StringContent? JsonContent(object? body)
    {
        if (body == null) return null;
        var json = JsonSerializer.Serialize(body, JsonOptions);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private async Task<JsonDocument> ExecuteAsync(HttpRequestMessage request, CancellationToken ct)
    {
        int attempts = 0;

        while (true)
        {
            HttpResponseMessage response;
            try
            {
                // Clone the request for retries (HttpRequestMessage can only be sent once)
                using var clone = await CloneRequestAsync(request);
                response = await _client.SendAsync(clone, ct);
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
            {
                if (attempts < _maxRetries)
                {
                    attempts++;
                    await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, attempts)), ct);
                    continue;
                }
                throw new DataMammothException("Request failed: " + ex.Message, inner: ex);
            }

            var status = (int)response.StatusCode;

            if (status is >= 200 and < 300)
            {
                var body = await response.Content.ReadAsStringAsync(ct);
                return string.IsNullOrWhiteSpace(body)
                    ? JsonDocument.Parse("{}")
                    : JsonDocument.Parse(body);
            }

            // Retry on 429 / 5xx
            if ((status == 429 || status >= 500) && attempts < _maxRetries)
            {
                attempts++;
                var retryAfter = response.Headers.RetryAfter?.Delta ?? TimeSpan.FromSeconds(Math.Pow(2, attempts));
                await Task.Delay(retryAfter, ct);
                continue;
            }

            // Parse error
            await ThrowApiException(response, ct);
        }
    }

    private static async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage req)
    {
        var clone = new HttpRequestMessage(req.Method, req.RequestUri);
        foreach (var header in req.Headers)
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

        if (req.Content != null)
        {
            var body = await req.Content.ReadAsByteArrayAsync();
            clone.Content = new ByteArrayContent(body);
            if (req.Content.Headers.ContentType != null)
                clone.Content.Headers.ContentType = req.Content.Headers.ContentType;
        }

        return clone;
    }

    private static async Task ThrowApiException(HttpResponseMessage response, CancellationToken ct)
    {
        var status = (int)response.StatusCode;
        var body = await response.Content.ReadAsStringAsync(ct);

        string requestId = "";
        string errorCode = "";
        string message = $"API error ({status})";

        try
        {
            using var doc = JsonDocument.Parse(body);
            if (doc.RootElement.TryGetProperty("meta", out var meta) &&
                meta.TryGetProperty("request_id", out var rid))
                requestId = rid.GetString() ?? "";

            if (doc.RootElement.TryGetProperty("errors", out var errors) &&
                errors.ValueKind == JsonValueKind.Array)
            {
                foreach (var err in errors.EnumerateArray())
                {
                    if (err.TryGetProperty("code", out var code)) errorCode = code.GetString() ?? "";
                    if (err.TryGetProperty("message", out var msg)) message = msg.GetString() ?? message;
                    break;
                }
            }
        }
        catch { /* ignore parse errors */ }

        throw status switch
        {
            401 or 403 => new AuthException(message, status, requestId, errorCode),
            404 => new NotFoundException(message, requestId, errorCode),
            429 => new RateLimitException(message,
                (int)(response.Headers.RetryAfter?.Delta?.TotalSeconds ?? 60), requestId),
            400 or 422 => new ValidationException(message, status, requestId),
            _ => new DataMammothException(message, status, requestId, errorCode),
        };
    }
}
