using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Core.Extensions
{
    public static class StringExtensions
    {
        public static string OnlyNumbers(this string source) => string.IsNullOrEmpty(source) ? source : new(source.Where(c => char.IsDigit(c)).ToArray());
        public static string OnlyLetters(this string source) => string.IsNullOrEmpty(source) ? source : new(source.Where(c => char.IsLetter(c)).ToArray());
        public static string OnlyLetterOrNumbers(this string source) => string.IsNullOrEmpty(source) ? source : new(source.Where(c => char.IsLetterOrDigit(c)).ToArray());
        public static string WithoutSpaces(this string source) => string.IsNullOrEmpty(source) ? source : new(source.Where(s => !char.IsWhiteSpace(s)).ToArray());
        public static string WithTrimmedSpaces(this string source) => string.IsNullOrEmpty(source) ? source : string.Join(" ", source.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));


        public static List<string> WithTrimmedSpaces(this List<string> source)
        {
            for (var i = 0; i < source.Count; i++)
                source[i] = source[i].WithTrimmedSpaces();

            return source;
        }

        public static string WithStrippedDiacritics(this string source)
        {
            var sbReturn = new StringBuilder();
            var normalizedSource = source.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in normalizedSource)
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);

            return sbReturn.ToString();
        }

        public static string ToSnakeCase(this string source) =>
            Regex.Replace(
                Regex.Replace(
                    Regex.Replace(source, @"([\p{Lu}]+)([\p{Lu}][\p{Ll}])", "$1_$2"), @"([\p{Ll}\d])([\p{Lu}])", "$1_$2"), @"[-\s]", "_").ToLower();

        public static string ToJson(this object source) => System.Text.Json.JsonSerializer.Serialize(source);
    }
}