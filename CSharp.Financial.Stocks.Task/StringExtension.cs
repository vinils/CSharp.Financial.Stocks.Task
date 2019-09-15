namespace CSharp.Financial.Stocks.Task
{
    using System;
    using System.Globalization;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtension
    {
        public static string RemoveDiacritics(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static T? GetValueOrNull<T>(this string valueAsString, IFormatProvider provider = null)
            where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
                return null;

            if (provider == null)
                provider = CultureInfo.CurrentCulture;

            try
            {
                return (T)Convert.ChangeType(valueAsString, typeof(T), provider);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static string NextWord(this string value, int startIdx = 0)
        {
            var endIdx = value.IndexOf(' ', startIdx);
            if(endIdx <= 0)
            {
                endIdx = value.Length;
            }
            return value.Substring(startIdx, endIdx - startIdx);
        }

        public static string LastWord(this string value, int? startIdx = null)
        {
            if(!startIdx.HasValue)
            {
                startIdx = value.Length;
            }

            var endIdx = value.LastIndexOf(' ', startIdx.Value - 1);
            if (endIdx <= 0)
            {
                endIdx = value.Length;
            }

            return value.Substring(endIdx + 1, startIdx.Value - endIdx -1);
        }

        public static string RemoveDuplicateSpaces(this string str)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            return regex.Replace(str, " ");
        }
    }
}