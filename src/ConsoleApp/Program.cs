using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ConsoleAppFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Interfaces;
using UsdmConverter.ApplicationCore.Services;
using UsdmConverter.Infrastructure;
namespace UsdmConverter.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {

                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IMarkdownReader, MarkdownReader>();
                    services.AddSingleton<IExcelWriter, ExcelWriter>();
                    services.AddScoped<IMarkdownParser, MarkdownParser>();
                    services.AddScoped<IExcelDecoder, ExcelDecoder>();
                })
                .RunConsoleAppFrameworkAsync<ApplicationLogic>(args);
        }
    }

    public class ApplicationLogic : ConsoleAppBase
    {
        private readonly ILogger<ApplicationLogic> _logger;
        private readonly IExcelWriter _excelWriter;
        private readonly IMarkdownReader _markdownReader;
        private readonly IMarkdownParser _markdownParser;
        private readonly IExcelDecoder _excelDecoder;

        /// <summary>
        /// Initializes a new instance of ApplicationLogic class.
        /// </summary>
        /// <param name="logger">logger object</param>
        /// <param name="excelWriter"></param>
        /// <param name="markdownReader"></param>
        /// <param name="markdownParser"></param>
        /// <param name="excelDecoder"></param>
        /// <param name="excelWriter"></param>
        public ApplicationLogic(
            ILogger<ApplicationLogic> logger,
            IExcelWriter excelWriter,
            IMarkdownReader markdownReader,
            IMarkdownParser markdownParser,
            IExcelDecoder excelDecoder
        )
        {
            _logger = logger;
            _excelWriter = excelWriter;
            _markdownReader = markdownReader;
            _markdownParser = markdownParser;
            _excelDecoder = excelDecoder;
        }

        [Command("xlsx", "convert markdown to excel")]
        public void ConvertMarkdownToExcel(
            [Option("i", "input markdown file path.")] string markdownFilePath,
            [Option("o", "output Excel file path.")] string excelFilePath
        )
        {
            var data = new RequirementSpecification
            {
                Title = "要求仕様書",
                Requirements = new System.Collections.Generic.List<UpperRequirement>()
                {
                    new UpperRequirement
                    {
                        ID = "REQ1",
                        Summay = "要求事項１",
                        Reason = "要求事項１の理由",
                        Description = "要求事項１の説明",
                        Requirements = new List<LowerRequirement>()
                        {
                            new LowerRequirement
                            {
                                ID = "REQ1-1",
                                Summay = "要求事項１-1",
                                Reason = "要求事項１-1の理由",
                                Description = "要求事項１-1の説明",
                                SpecificationGroups = new List<SpecificationGroup>()
                                {
                                    new SpecificationGroup
                                    {
                                        Category = "仕様グループ1",
                                        Specifications = new List<Specification>()
                                        {
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-1-1",
                                                Description = "仕様の詳細1-1-1",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = true,
                                                ID = "SPEC1-1-2",
                                                Description = "仕様の詳細1-1-2",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-1-3",
                                                Description = "仕様の詳細1-1-3",
                                            },
                                        }
                                    }
                                },
                            },
                            new LowerRequirement
                            {
                                ID = "REQ1-2",
                                Summay = "要求事項１-2",
                                Reason = "要求事項１-2の理由",
                                Description = "要求事項１-2の説明",
                                SpecificationGroups = new List<SpecificationGroup>()
                                {
                                    new SpecificationGroup
                                    {
                                        Category = "仕様グループ1",
                                        Specifications = new List<Specification>()
                                        {
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-2-1",
                                                Description = "仕様の詳細1-2-1",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = true,
                                                ID = "SPEC1-2-2",
                                                Description = "仕様の詳細1-2-2",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-2-3",
                                                Description = "仕様の詳細1-2-3",
                                            },
                                        }
                                    }
                                },
                            }
                        }
                    },
                    new UpperRequirement
                    {
                        ID = "REQ2",
                        Summay = "要求事項2",
                        Reason = "要求事項2の理由",
                        Description = "要求事項2の説明",
                        Requirements = new List<LowerRequirement>()
                        {
                            new LowerRequirement
                            {
                                ID = "REQ1-1",
                                Summay = "要求事項１-1",
                                Reason = "要求事項１-1の理由",
                                Description = "要求事項１-1の説明",
                                SpecificationGroups = new List<SpecificationGroup>()
                                {
                                    new SpecificationGroup
                                    {
                                        Category = "仕様グループ1",
                                        Specifications = new List<Specification>()
                                        {
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-1-1",
                                                Description = "仕様の詳細1-1-1",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = true,
                                                ID = "SPEC1-1-2",
                                                Description = "仕様の詳細1-1-2",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-1-3",
                                                Description = "仕様の詳細1-1-3",
                                            },
                                        }
                                    }
                                },
                            },
                            new LowerRequirement
                            {
                                ID = "REQ1-2",
                                Summay = "要求事項１-2",
                                Reason = "要求事項１-2の理由",
                                Description = "要求事項１-2の説明",
                                SpecificationGroups = new List<SpecificationGroup>()
                                {
                                    new SpecificationGroup
                                    {
                                        Category = "仕様グループ1",
                                        Specifications = new List<Specification>()
                                        {
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-2-1",
                                                Description = "仕様の詳細1-2-1",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = true,
                                                ID = "SPEC1-2-2",
                                                Description = "仕様の詳細1-2-2",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-2-3",
                                                Description = "仕様の詳細1-2-3",
                                            },
                                        }
                                    }
                                },
                            }
                        }
                    },
                    new UpperRequirement
                    {
                        ID = "REQ3",
                        Summay = "要求事項3",
                        Reason = "要求事項3の理由",
                        Description = "要求事項3の説明",
                        Requirements = new List<LowerRequirement>()
                        {
                            new LowerRequirement
                            {
                                ID = "REQ1-1",
                                Summay = "要求事項１-1",
                                Reason = "要求事項１-1の理由",
                                Description = "要求事項１-1の説明",
                                SpecificationGroups = new List<SpecificationGroup>()
                                {
                                    new SpecificationGroup
                                    {
                                        Category = "仕様グループ1",
                                        Specifications = new List<Specification>()
                                        {
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-1-1",
                                                Description = "仕様の詳細1-1-1",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = true,
                                                ID = "SPEC1-1-2",
                                                Description = "仕様の詳細1-1-2",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-1-3",
                                                Description = "仕様の詳細1-1-3",
                                            },
                                        }
                                    }
                                },
                            },
                            new LowerRequirement
                            {
                                ID = "REQ1-2",
                                Summay = "要求事項１-2",
                                Reason = "要求事項１-2の理由",
                                Description = "要求事項１-2の説明",
                                SpecificationGroups = new List<SpecificationGroup>()
                                {
                                    new SpecificationGroup
                                    {
                                        Category = "仕様グループ1",
                                        Specifications = new List<Specification>()
                                        {
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-2-1",
                                                Description = "仕様の詳細1-2-1",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = true,
                                                ID = "SPEC1-2-2",
                                                Description = "仕様の詳細1-2-2",
                                            },
                                            new Specification
                                            {
                                                IsImplemented = false,
                                                ID = "SPEC1-2-3",
                                                Description = "仕様の詳細1-2-3",
                                            },
                                        }
                                    }
                                },
                            }
                        }
                    }
                }
            };

            var markdown = _markdownReader.ReadToEnd(markdownFilePath);
            var usdm = _markdownParser.Parse(markdown);
            var book = _excelDecoder.Decode(usdm);
            _excelWriter.Write(book, excelFilePath);
        }

        [Command("md", "convert excel to markdown")]
        public void ConvertExcelToMarkdown(
            [Option("i", "input Excel file path.")] string excelFilePath,
            [Option("o", "output markdown file path.")] string markdownFilePath
        )
        {
            throw new NotImplementedException();
        }
    }
}
