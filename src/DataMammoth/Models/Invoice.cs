namespace DataMammoth.Models;

public record Invoice(
    string Id,
    string Status,
    decimal Total,
    decimal Subtotal,
    decimal Tax,
    string? Currency,
    string? DueDate,
    string? PaidAt,
    string CreatedAt,
    string UpdatedAt
);
