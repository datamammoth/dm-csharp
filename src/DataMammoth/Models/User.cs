namespace DataMammoth.Models;

public record User(
    string Id,
    string? Name,
    string Email,
    string? Role,
    string? Status,
    bool TwoFactorEnabled,
    string? LastLoginAt,
    string CreatedAt,
    string UpdatedAt
);
