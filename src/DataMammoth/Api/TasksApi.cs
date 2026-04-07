using System.Text.Json;
using DataMammoth.Exceptions;

namespace DataMammoth.Api;

/// <summary>Tasks API -- track async operations (server provisioning, etc.).</summary>
public class TasksApi
{
    private readonly DmHttpClient _client;
    internal TasksApi(DmHttpClient client) => _client = client;

    public Task<JsonDocument> ListAsync(Dictionary<string, string>? query = null, CancellationToken ct = default)
        => _client.GetAsync("/tasks", query, ct);

    public Task<JsonDocument> GetAsync(string id, CancellationToken ct = default)
        => _client.GetAsync($"/tasks/{id}", ct: ct);

    /// <summary>Poll a task until it completes or times out.</summary>
    public async Task<JsonDocument> AwaitAsync(string id, int intervalMs = 2000, int timeoutMs = 300_000, CancellationToken ct = default)
    {
        var start = DateTimeOffset.UtcNow;

        while (true)
        {
            var task = await GetAsync(id, ct);
            var status = task.RootElement.GetProperty("data").GetProperty("status").GetString();

            if (status is "completed" or "failed")
                return task;

            if ((DateTimeOffset.UtcNow - start).TotalMilliseconds >= timeoutMs)
                throw new DataMammothException($"Task {id} timed out after {timeoutMs}ms", 408);

            await Task.Delay(intervalMs, ct);
        }
    }
}
