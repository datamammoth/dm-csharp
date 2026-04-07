namespace DataMammoth.Models;

public record Webhook(
    string Id,
    string Url,
    IReadOnlyList<string> Events,
    bool Active,
    string? Secret,
    string CreatedAt,
    string UpdatedAt
);
