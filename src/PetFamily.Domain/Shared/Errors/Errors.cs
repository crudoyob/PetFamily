namespace PetFamily.Domain.Shared.Errors;

public static class Errors
{
    public static class General
    {
        public static Error Unexcepted(string message)
        {
            return Error.Failure("server.internal", message);
        }
        
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }
        
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return Error.Validation("record.not.found", $"record not fount{forId}");
        }
        
        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name + " ";
            return Error.Validation("length.is.invalid", $"invalid{label}length");
        }
        
        public static Error AlreadyExists(string? invalidField = null)
        {
            var message = invalidField == null
                ? "record already exists" 
                : "record with this " + invalidField + " already exists";
            return Error.Validation("record.already.exists", message, "Request." + invalidField);
        }
    }
}