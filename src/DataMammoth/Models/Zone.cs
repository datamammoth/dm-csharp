namespace DataMammoth.Models;

public record Zone(
    string Id,
    string Name,
    string? Slug,
    string? Country,
    string? City,
    bool IsActive,
    string CreatedAt,
    string UpdatedAt
);
