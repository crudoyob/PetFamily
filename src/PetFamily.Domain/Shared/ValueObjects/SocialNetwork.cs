using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects;

public sealed record SocialNetwork
{
    public string Name { get; }
    public string Url { get; }

    private SocialNetwork(string name, string url)
    {
        Name = name;
        Url = url;
    }

    public static Result<SocialNetwork, Error> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired("Name");

        if (name.Length > LengthConstants.LENGTH100)
            return Errors.General.ValueIsInvalid("Name");

        if (string.IsNullOrWhiteSpace(url))
            return Errors.General.ValueIsRequired("Url");

        if (url.Length > LengthConstants.LENGTH250)
            return Errors.General.ValueIsInvalid("Name");

        return new SocialNetwork(name, url);
    }
}