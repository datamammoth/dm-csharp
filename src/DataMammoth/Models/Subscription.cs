namespace DataMammoth.Models;

public record Subscription(
    string Id,
    string Status,
    string? ProductId,
    string? PlanName,
    string? BillingCycle,
    decimal? Amount,
    string? Currency,
    string? NextDueDate,
    string? CancelledAt,
    string CreatedAt,
    string UpdatedAt
);
