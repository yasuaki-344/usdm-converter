// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using UsdmConverter.ApplicationCore.Entities;

namespace UsdmConverter.ApplicationCore.Logic
{
    static public class ParseUtility
    {
        /// <summary>
        /// Extracts id and summary string from the given HeaderBlock
        /// </summary>
        /// <param name="header">markdown header block</param>
        /// <returns>Tuple of id and summary string</returns>
        static public (string id, string summary) DecomposeHeading(HeaderBlock header)
        {
            var text = header.ToString().Trim();
            var prefix = Regex.Match(text, "\\[[-A-Z0-9]{1,}\\]").Value;
            var id = prefix.Substring(1, prefix.Length - 2);
            var summay = text?.Replace(prefix, string.Empty);

            return string.IsNullOrEmpty(summay) ? (id, string.Empty) : (id, summay);
        }

        static public string ExtractGroupCategory(string rawString)
        {
            if (rawString.StartsWith("<") && rawString.EndsWith(">"))
            {
                return rawString.Substring(1, rawString.Length - 2);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Extracts USDM specification list from given ListBlock.
        /// </summary>
        /// <param name="listBlock">Markdown list block</param>
        /// <returns>The list of USDM specification information</returns>
        static public List<Specification> DecomposeSpecification(ListBlock listBlock) =>
            listBlock.Items
                .Select(x => DecomposeListItemBlock(x))
                .ToList();

        /// <summary>
        /// Extracts USDM specification from given ListItemBlock.
        /// </summary>
        /// <param name="listBlock">Markdown list item block</param>
        /// <returns>USDM specification information</returns>
        static private Specification DecomposeListItemBlock(ListItemBlock item)
        {
            foreach (var block in item.Blocks)
            {
                var paragraph = block as ParagraphBlock;
                if (paragraph != null)
                {
                    var link = paragraph.Inlines[0] as MarkdownLinkInline;
                    if (link != null)
                    {
                        var isImplemented = link.Inlines[0].ToString()?.Equals("x");
                        var id = link.ReferenceId;
                        var description = paragraph.Inlines[1].ToString();
                        if (isImplemented != null && id != null && description != null)
                        {
                            return new Specification
                            {
                                IsImplemented = isImplemented ?? false,
                                ID = id,
                                Description = description
                            };
                        }
                    }
                }
            }
            return new Specification();
        }

    }
}
