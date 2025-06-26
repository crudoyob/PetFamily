using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public class SocialNetwork
{
    public string Name { get; }
    public string Url{ get; }
    
    private SocialNetwork(string name, string url)
    {
        Name = name;
        Url = url;
    }
    
    public static Result<SocialNetwork> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<SocialNetwork>("Имя социальной сети не может быть пустым");
        
        if (string.IsNullOrWhiteSpace(url))
            return Result.Failure<SocialNetwork>("Url социальной сети не может быть пустым");
        
        return Result.Success(new SocialNetwork(name, url));
    }
}