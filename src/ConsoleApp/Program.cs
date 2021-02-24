using System;
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
using UsdmConverter.ApplicationCore.Interfaces;
using UsdmConverter.ApplicationCore.Services;
using UsdmConverter.Infrastructure;
using UsdmConverter.ApplicationCore.Entities;

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
        private readonly IMarkdownParser _markdownParser;
        private readonly IExcelDecoder _excelDecoder;

        /// <summary>
        /// Initializes a new instance of ApplicationLogic class.
        /// </summary>
        /// <param name="logger">logger object</param>
        /// <param name="excelWriter"></param>
        /// <param name="markdownParser"></param>
        /// <param name="excelDecoder"></param>
        public ApplicationLogic(
            ILogger<ApplicationLogic> logger,
            IExcelWriter excelWriter,
            IMarkdownParser markdownParser,
            IExcelDecoder excelDecoder
        )
        {
            _logger = logger;
            _excelWriter = excelWriter;
            _markdownParser = markdownParser;
            _excelDecoder = excelDecoder;
        }

        [Command("xlsx", "convert markdown to excel")]
        public void ConvertMarkdownToExcel(
            [Option("i", "input markdown file path.")] string markdownFilePath,
            [Option("o", "output Excel file path.")] string excelFilePath
        )
        {
            var data = new RequirementSpecification();
            var book = _excelDecoder.Decode(data);
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
