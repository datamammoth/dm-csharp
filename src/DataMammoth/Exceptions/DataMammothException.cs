namespace DataMammoth.Exceptions;

/// <summary>
/// Base exception for all DataMammoth SDK errors.
/// </summary>
public class DataMammothException : Exception
{
    public int StatusCode { get; }
    public string? RequestId { get; }
    public string? ErrorCode { get; }

    public DataMammothException(string message, int statusCode = 0, string? requestId = null, string? errorCode = null, Exception? inner = null)
        : base(message, inner)
    {
        StatusCode = statusCode;
        RequestId = requestId;
        ErrorCode = errorCode;
    }
}

/// <summary>Thrown when authentication or authorization fails (401/403).</summary>
public class AuthException : DataMammothException
{
    public AuthException(string message, int statusCode, string? requestId = null, string? errorCode = null)
        : base(message, statusCode, requestId, errorCode) { }
}

/// <summary>Thrown when a resource is not found (404).</summary>
public class NotFoundException : DataMammothException
{
    public NotFoundException(string message, string? requestId = null, string? errorCode = null)
        : base(message, 404, requestId, errorCode) { }
}

/// <summary>Thrown when request validation fails (400/422).</summary>
public class ValidationException : DataMammothException
{
    public IReadOnlyList<Dictionary<string, string>> FieldErrors { get; }

    public ValidationException(string message, int statusCode, string? requestId = null, IReadOnlyList<Dictionary<string, string>>? fieldErrors = null)
        : base(message, statusCode, requestId, "VALIDATION_FAILED")
    {
        FieldErrors = fieldErrors ?? new List<Dictionary<string, string>>();
    }
}

/// <summary>Thrown when rate limit is exceeded (429).</summary>
public class RateLimitException : DataMammothException
{
    public int RetryAfterSeconds { get; }

    public RateLimitException(string message = "Rate limit exceeded", int retryAfterSeconds = 60, string? requestId = null)
        : base(message, 429, requestId, "RATE_LIMITED")
    {
        RetryAfterSeconds = retryAfterSeconds;
    }
}
