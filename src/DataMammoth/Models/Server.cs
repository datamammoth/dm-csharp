namespace DataMammoth.Models;

public record Server(
    string Id,
    string? Hostname,
    string? Label,
    string Status,
    string? IpAddress,
    string? Ipv6Address,
    string? Region,
    string? OsImage,
    string? Plan,
    ServerSpecs? Specs,
    string? ProvisionedAt,
    string CreatedAt,
    string UpdatedAt
);

public record ServerSpecs(
    int? CpuCores,
    int? RamMb,
    int? DiskGb,
    string? DiskType,
    int? BandwidthTb
);
