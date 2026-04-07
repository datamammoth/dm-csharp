namespace DataMammoth.Models;

public record DmTask(
    string Id,
    string Type,
    string Status,
    string? ResourceType,
    string? ResourceId,
    string? StartedAt,
    string? CompletedAt,
    object? Result,
    string? Error,
    string CreatedAt
);
