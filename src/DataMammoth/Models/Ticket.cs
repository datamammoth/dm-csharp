namespace DataMammoth.Models;

public record Ticket(
    string Id,
    string Subject,
    string Status,
    string? Priority,
    string? DepartmentId,
    string? LastReplyAt,
    string CreatedAt,
    string UpdatedAt
);
