using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Shared.ValueObjects;

public record SocialNetwork
{
    public string Name { get; }
    public string Url { get; }

    private SocialNetwork(string name, string url)
    {
        Name = name;
        Url = url;
    }

    public static Result<SocialNetwork> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<SocialNetwork>("Имя социальной сети не может быть пустым");

        if (name.Length > LengthConstants.Length100)
            return Result.Failure<SocialNetwork>
                ($"Имя социальной сети не может превышать {LengthConstants.Length100} символов");

        if (string.IsNullOrWhiteSpace(url))
            return Result.Failure<SocialNetwork>("Url социальной сети не может быть пустым");

        if (url.Length > LengthConstants.Length250)
            return Result.Failure<SocialNetwork>
                ($"Url социальной сети не может превышать {LengthConstants.Length250} символов");

        return Result.Success(new SocialNetwork(name, url));
    }
}