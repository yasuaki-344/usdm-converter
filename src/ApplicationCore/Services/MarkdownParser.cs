using System;
using System.Linq;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Exceptions;
using UsdmConverter.ApplicationCore.Interfaces;

namespace UsdmConverter.ApplicationCore.Services
{
    public class MarkdownParser : IMarkdownParser
    {
        private readonly MarkdownDocument _document = new MarkdownDocument();

        /// <summary>
        /// Initializes a new instance of MarkdownParser class.
        /// </summary>
        public MarkdownParser()
        {
        }

        /// <summary>
        /// Parse a markdown document
        /// </summary>
        /// <param name="content">The markdown text</param>
        /// <returns></returns>
        public RequirementSpecification Parse(string content)
        {
            _document.Parse(content);

            var data = new RequirementSpecification();
            foreach (var element in _document.Blocks)
            {
                switch (element.Type)
                {
                    case MarkdownBlockType.Code:
                        Console.WriteLine($"Type: {element.Type}");
                        break;
                    case MarkdownBlockType.Header:
                        var header = element as HeaderBlock;
                        if (header != null)
                        {
                            var headerLevel = header.HeaderLevel;
                            var text = header.ToString().Trim();
                            // var count = header.Inlines.Count;
                            switch (headerLevel)
                            {
                                case 1:
                                    data.Title = text;
                                    break;
                                case 2:
                                    // TODO: IDとの分離
                                    data.Requirements.Add(
                                        new UpperRequirement
                                        {
                                            Summay = text
                                        }
                                    );
                                    break;
                                case 3:
                                    if (text.Equals("理由"))
                                    {
                                        data.Requirements.Last().Reason = text;
                                    }
                                    else if (text.Equals("説明"))
                                    {
                                        data.Requirements.Last().Description = text;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Text: {text}");
                                    }
                                    break;
                                default:
                                    Console.WriteLine($"Text: {text}");
                                    break;
                            }
                        }
                        break;
                    case MarkdownBlockType.HorizontalRule:
                        Console.WriteLine($"Type: {element.Type}");
                        break;
                    case MarkdownBlockType.LinkReference:
                        Console.WriteLine($"Type: {element.Type}");
                        break;
                    case MarkdownBlockType.List:
                        Console.WriteLine($"Type: {element.Type}");
                        break;
                    case MarkdownBlockType.ListItemBuilder:
                        Console.WriteLine($"Type: {element.Type}");
                        break;
                    case MarkdownBlockType.Paragraph:
                        Console.WriteLine($"Type: {element.Type}");
                        break;
                    case MarkdownBlockType.Quote:
                        Console.WriteLine($"Type: {element.Type}");
                        break;
                    case MarkdownBlockType.Root:
                        Console.WriteLine($"Type: {element.Type}");
                        break;
                    case MarkdownBlockType.Table:
                        Console.WriteLine($"Type: {element.Type}");
                        break;
                    default:
                        Console.WriteLine($"Type: {element.Type}");
                        break;
                }
            }
            return data;
        }
    }
}
