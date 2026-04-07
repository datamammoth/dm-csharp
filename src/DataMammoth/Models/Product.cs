namespace DataMammoth.Models;

public record Product(
    string Id,
    string Name,
    string? Slug,
    string? Description,
    string? CategoryId,
    string? Type,
    bool IsActive,
    ServerSpecs? Specs,
    string CreatedAt,
    string UpdatedAt
);
