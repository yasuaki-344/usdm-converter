using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Exceptions;
using UsdmConverter.ApplicationCore.Interfaces;
using UsdmConverter.ApplicationCore.Logic;

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
            var status = UsdmScope.None;

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
                                    {
                                        var info = ParseUtility.DecomposeHeading(text);
                                        data.Requirements.Add(
                                            new UpperRequirement
                                            {
                                                ID = info.id,
                                                Summary = info.summary
                                            }
                                        );
                                    }
                                    break;
                                case 3:
                                    if (text.Equals("理由"))
                                    {
                                        status = UsdmScope.UpperRequiremetReason;
                                    }
                                    else if (text.Equals("説明"))
                                    {
                                        status = UsdmScope.UpperRequiremetDescription;
                                    }
                                    else
                                    {
                                        var info = ParseUtility.DecomposeHeading(text);
                                        data.Requirements.Last().Requirements.Add(
                                            new LowerRequirement
                                            {
                                                ID = info.id,
                                                Summary = info.summary
                                            }
                                        );
                                    }
                                    break;
                                case 4:
                                    if (text.Equals("理由"))
                                    {
                                        status = UsdmScope.LowerRequiremetReason;
                                    }
                                    else if (text.Equals("説明"))
                                    {
                                        status = UsdmScope.LowerRequiremetDescription;
                                    }
                                    else if (text.Equals("仕様"))
                                    {
                                        status = UsdmScope.Specification;
                                    }
                                    break;
                                default:
                                    Console.WriteLine($"Text: {text}");
                                    Console.WriteLine($"Level: {headerLevel}");
                                    Console.WriteLine(element.ToString());
                                    status = UsdmScope.None;
                                    break;
                            }
                        }
                        break;
                    case MarkdownBlockType.Paragraph:
                        switch (status)
                        {
                            case UsdmScope.UpperRequiremetReason:
                                data.Requirements.Last().Reason += element.ToString() ?? string.Empty;
                                break;
                            case UsdmScope.UpperRequiremetDescription:
                                data.Requirements.Last().Description += element.ToString() ?? string.Empty;
                                break;

                            case UsdmScope.LowerRequiremetReason:
                                data.Requirements.Last().Requirements.Last().Reason += element.ToString() ?? string.Empty;
                                break;
                            case UsdmScope.LowerRequiremetDescription:
                                data.Requirements.Last().Requirements.Last().Description += element.ToString() ?? string.Empty;
                                break;
                            case UsdmScope.Specification:
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
                            case UsdmScope.UpperRequiremetReason:
                                data.Requirements.Last().Reason += "\n" + element.ToString() ?? string.Empty;
                                break;
                            case UsdmScope.UpperRequiremetDescription:
                                data.Requirements.Last().Description += "\n" + element.ToString() ?? string.Empty;
                                break;
                            case UsdmScope.LowerRequiremetReason:
                                data.Requirements.Last().Requirements.Last().Reason += "\n" + element.ToString() ?? string.Empty;
                                break;
                            case UsdmScope.LowerRequiremetDescription:
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
