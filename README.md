# DataMammoth C# / .NET SDK

Official C# and .NET client for the [DataMammoth API v2](https://data-mammoth.com/api-docs/reference).

> **Status**: Under development. Not yet published to NuGet.

## Installation

```bash
dotnet add package DataMammoth.Sdk
```

Or via the NuGet Package Manager:

```
Install-Package DataMammoth.Sdk
```

## Quick Start

```csharp
using DataMammoth;

var dm = new DataMammothClient("dm_your_key_here");

// List active servers
var servers = await dm.Servers.ListAsync(new ServerListParams
{
    Status = "active"
});
foreach (var server in servers.Data)
{
    Console.WriteLine($"{server.Hostname} — {server.IpAddress}");
}

// Create a server
var task = await dm.Servers.CreateAsync(new ServerCreateParams
{
    ProductId = "prod_abc",
    ImageId = "img_ubuntu2204",
    Hostname = "web-01"
});
var newServer = await task.WaitAsync();
```

## Features

- All 105 API v2 endpoints
- .NET 8+ with nullable reference types
- Async/await throughout
- Strong typing with record types
- Automatic pagination
- Rate limit handling with retry
- API key authentication

## Documentation

- [API Reference](https://data-mammoth.com/api-docs/reference)
- [Getting Started Guide](https://data-mammoth.com/api-docs/guides)
- [Authentication](https://data-mammoth.com/api-docs/guides/authentication)

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md).

## License

MIT — see [LICENSE](LICENSE).
