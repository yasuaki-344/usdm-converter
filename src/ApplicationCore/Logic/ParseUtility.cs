using System.Text.RegularExpressions;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

namespace UsdmConverter.ApplicationCore.Logic
{
    static public class ParseUtility
    {
        static public (string id, string summary) DecomposeHeading(HeaderBlock header)
        {
            var text = header.ToString().Trim();
            var prefix = Regex.Match(text, "\\[[-A-Z0-9]{1,}\\]").Value;
            var id = prefix.Substring(1, prefix.Length - 2);
            var summay = text?.Replace(prefix, string.Empty);

            return string.IsNullOrEmpty(summay) ? (id, string.Empty) : (id, summay);
        }

    }
}
