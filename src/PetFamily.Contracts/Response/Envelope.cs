using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Contracts.Response;

public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);

public record Envelope
{
    public object? Result { get; }
    public ErrorList? Errors { get; }
    public DateTime TimeGenerated { get; }

    private Envelope(object? result, ErrorList? errors)
    {
        Result = result; 
        Errors = errors;
        TimeGenerated = DateTime.UtcNow;
    }

    public static Envelope Ok(object? result = null) =>
        new(result, null);

    public static Envelope Error(ErrorList? errors) =>
        new(null, errors);
}