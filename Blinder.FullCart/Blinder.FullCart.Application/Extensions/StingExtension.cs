
using System.Globalization;

namespace Blinder.FullCart.Application.Extensions;

static class StingExtension
{
    public static string ToCamelCase(this string str)
    {
        if (!string.IsNullOrEmpty(str) && str.Length > 1)
        {
            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
        return str.ToLowerInvariant();
    }
    public static bool IsArabic(this string input)
    {
        foreach (char c in input)
            if (char.GetUnicodeCategory(c) == UnicodeCategory.OtherLetter)
                return true;
        return false;
    }

    public static bool IsEnglish(this string input)
    {
        foreach (char c in input)
            if (char.IsLetter(c) && char.GetUnicodeCategory(c) != UnicodeCategory.OtherLetter)
                return true;
        return false;
    }
}
