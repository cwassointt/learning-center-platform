using System.Text.RegularExpressions;

namespace ACME.LearningCenterPlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static partial class StringExtensions
{
    public static string ToKebabCase(this string text)
    {
        return string.IsNullOrEmpty(text) 
            ? text 
            : KebabCaseRegex().Replace(text, "-$1").ToLower();
    }

    [GeneratedRegex("(?<!^)(?=[A-Z][a-z]|(?<=[a-z])[A-Z])", RegexOptions.Compiled)]
    private static partial Regex KebabCaseRegex();
}
