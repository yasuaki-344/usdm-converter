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
