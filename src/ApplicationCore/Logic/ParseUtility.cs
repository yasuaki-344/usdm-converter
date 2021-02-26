using System;
using System.Text.RegularExpressions;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using UsdmConverter.ApplicationCore.Entities;

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

        static public Specification DecomposeSpecification(ListBlock listBlock)
        {
            foreach (var item in listBlock.Items)
            {
                foreach (var item2 in item.Blocks)
                {
                    var paragraph = item2 as ParagraphBlock;
                    if (paragraph != null)
                    {
                        var item3 = (MarkdownLinkInline)paragraph.Inlines[0];
                        var spec = new Specification
                        {
                            IsImplemented = item3.Inlines[0].ToString().Equals("x"),
                            ID = item3.ReferenceId,
                            Description = paragraph.Inlines[1].ToString()
                        };
                        return spec;
                    }
                }
            }

            return new Specification();
        }

        static private Specification DecomposeSpecification(ListItemBlock item)
        {
            throw new NotImplementedException();
        }
    }
}
