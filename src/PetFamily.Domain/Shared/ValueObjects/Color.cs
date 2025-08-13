using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared.ValueObjects
{
    public sealed record Color
    {
        private const string HexRegexPattern = @"^#([0-9A-Fa-f]{6}|[0-9A-Fa-f]{3})$";
        private static readonly Regex HexRegex = new(
            HexRegexPattern,
            RegexOptions.Compiled,
            TimeSpan.FromMilliseconds(100)
        );

        public string ColorName { get; }
        public string ColorDescription { get; }
        public string HexCode { get; }

        private Color(string colorName, string colorDescription, string hexCode)
        {
            ColorName = colorName;
            ColorDescription = colorDescription;
            HexCode = hexCode;
        }

        public static Result<Color, Error> Create(string colorName, string colorDescription, string hexCode)
        {
            if (string.IsNullOrWhiteSpace(colorName))
                return Errors.General.ValueIsRequired("ColorName");

            if (colorName.Length > LengthConstants.LENGTH100)
                return Errors.General.ValueIsInvalid("ColorName");

            if (string.IsNullOrWhiteSpace(colorDescription))
                return Errors.General.ValueIsRequired("ColorDescription");

            if (colorDescription.Length > LengthConstants.LENGTH250)
                return Errors.General.ValueIsInvalid("ColorDescription");

            if (!string.IsNullOrWhiteSpace(hexCode) && !HexRegex.IsMatch(hexCode))
                return Errors.General.ValueIsInvalid("HexCode");

            return new Color(colorName, colorDescription, hexCode);
        }
    }
}