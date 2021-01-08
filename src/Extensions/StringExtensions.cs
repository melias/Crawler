using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Crawler.Extensions
{
    public static class StringExtensions
    {
        public static string DecodeFromUtf8(this string value)
        {
            byte[] bytes = Encoding.Default.GetBytes(value);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string RemoveAllTagsHtml(this string html)
        {
            var justText = Regex.Replace(html, @"<[^>]*>", String.Empty);
            return justText.Trim().Replace("   ", "").Replace("\t", "");
        }
    }
}
