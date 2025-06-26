using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public class FullName
{
    public string FirstName { get; }
    public string LastName { get; }
    public string? Patronymic { get; }
    
    private FullName(string firstName, string lastName, string? patronymic = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
    }
    
    public static Result<FullName> Create(string firstName, string lastName, string? patronymic = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<FullName>("Имя человека не может быть пустым");
        
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<FullName>("Фамилия человека не может быть пустой");
        
        return Result.Success(new FullName(firstName, lastName, patronymic));
    }
}