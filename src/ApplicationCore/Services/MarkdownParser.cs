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
            var status = string.Empty;

            var data = new RequirementSpecification();
            foreach (var element in _document.Blocks)
            {
                switch (element.Type)
                {
                    case MarkdownBlockType.Header:
                        var header = element as HeaderBlock;
                        if (header != null)
                        {
                            var headerLevel = header.HeaderLevel;
                            var text = header.ToString().Trim();
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
                                        status = "UpperRequiremetReason";
                                    }
                                    else if (text.Equals("説明"))
                                    {
                                        status = "UpperRequiremetDescription";
                                    }
                                    else
                                    {
                                        data.Requirements.Last().Requirements.Add(
                                            // TODO: IDとの分離
                                            new LowerRequirement
                                            {
                                                ID = string.Empty,
                                                Summay = text,
                                            }
                                        );
                                    }
                                    break;
                                case 4:
                                    if (text.Equals("理由"))
                                    {
                                        status = "LowerRequiremetReason";
                                    }
                                    else if (text.Equals("説明"))
                                    {
                                        status = "LowerRequiremetDescription";
                                    }
                                    else if (text.Equals("仕様"))
                                    {
                                        status = "Specification";
                                    }
                                    break;
                                default:
                                    Console.WriteLine($"Text: {text}");
                                    Console.WriteLine($"Level: {headerLevel}");
                                    Console.WriteLine(element.ToString());
                                    status = string.Empty;
                                    break;
                            }
                        }
                        break;
                    case MarkdownBlockType.Paragraph:
                        switch (status)
                        {
                            case "UpperRequiremetReason":
                                data.Requirements.Last().Reason += element.ToString() ?? string.Empty;
                                break;
                            case "UpperRequiremetDescription":
                                data.Requirements.Last().Description += element.ToString() ?? string.Empty;
                                break;

                            case "LowerRequiremetReason":
                                data.Requirements.Last().Requirements.Last().Reason += element.ToString() ?? string.Empty;
                                break;
                            case "LowerRequiremetDescription":
                                data.Requirements.Last().Requirements.Last().Description += element.ToString() ?? string.Empty;
                                break;
                            case "Specification":
                                data.Requirements.Last().Requirements.Last().SpecificationGroups.Add(
                                    new SpecificationGroup
                                    {
                                        Category = element.ToString() ?? string.Empty
                                    }
                                );
                                break;
                            default:
                                Console.WriteLine($"Type: {element.Type}");
                                Console.WriteLine(element.ToString());
                                break;
                        }
                        break;
                    case MarkdownBlockType.List:
                        switch (status)
                        {
                            case "UpperRequiremetReason":
                                data.Requirements.Last().Reason += "\n" + element.ToString() ?? string.Empty;
                                break;
                            case "UpperRequiremetDescription":
                                data.Requirements.Last().Description += "\n" + element.ToString() ?? string.Empty;
                                break;
                            case "LowerRequiremetReason":
                                data.Requirements.Last().Requirements.Last().Reason += "\n" + element.ToString() ?? string.Empty;
                                break;
                            case "LowerRequiremetDescription":
                                data.Requirements.Last().Requirements.Last().Description += "\n" + element.ToString() ?? string.Empty;
                                break;
                            // case "Specification":
                            //     data.Requirements.Last().Requirements.Last().SpecificationGroups.Last().Specifications.Add(
                            //         new Specification
                            //         {
                            //             Description = element.ToString() ?? string.Empty
                            //         }
                            //     );
                            //     break;
                            default:
                                Console.WriteLine($"Type: {element.Type}");
                                Console.WriteLine(element.ToString());
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine($"Type: {element.Type}");
                        Console.WriteLine(element.ToString());
                        break;
                }
            }
            return data;
        }
    }
}
