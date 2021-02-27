// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System;
using System.IO;
using System.Threading.Tasks;
using ConsoleAppFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                    services.AddSingleton<IExcelReader, ExcelReader>();
                    services.AddSingleton<IExcelWriter, ExcelWriter>();
                    services.AddSingleton<IMarkdownReader, MarkdownReader>();
                    services.AddScoped<IMarkdownParser, MarkdownParser>();
                    services.AddScoped<IMarkdownComposer, MarkdownComposer>();
                    services.AddScoped<IExcelDecoder, ExcelDecoder>();
                    services.AddScoped<IExcelEncoder, ExcelEncoder>();
                })
                .RunConsoleAppFrameworkAsync<ApplicationLogic>(args);
        }
    }

    public class ApplicationLogic : ConsoleAppBase
    {
        private readonly ILogger<ApplicationLogic> _logger;
        private readonly IExcelReader _excelReader;
        private readonly IExcelWriter _excelWriter;
        private readonly IMarkdownReader _markdownReader;
        private readonly IMarkdownParser _markdownParser;
        private readonly IMarkdownComposer _markdownComposer;
        private readonly IExcelDecoder _excelDecoder;
        private readonly IExcelEncoder _excelEncoder;

        /// <summary>
        /// Initializes a new instance of ApplicationLogic class.
        /// </summary>
        /// <param name="logger">logger object</param>
        /// <param name="excelWriter"></param>
        /// <param name="markdownReader"></param>
        /// <param name="markdownParser"></param>
        /// <param name="excelDecoder"></param>
        public ApplicationLogic(
            ILogger<ApplicationLogic> logger,
            IExcelReader excelReader,
            IExcelWriter excelWriter,
            IMarkdownReader markdownReader,
            IMarkdownParser markdownParser,
            IMarkdownComposer markdownComposer,
            IExcelDecoder excelDecoder,
            IExcelEncoder excelEncoder
        )
        {
            _logger = logger;
            _excelReader = excelReader;
            _excelWriter = excelWriter;
            _markdownReader = markdownReader;
            _markdownParser = markdownParser;
            _markdownComposer = markdownComposer;
            _excelDecoder = excelDecoder;
            _excelEncoder = excelEncoder;
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
            var book = _excelReader.Read(excelFilePath);
            var entity = _excelEncoder.Encode(book);
            var markdown = _markdownComposer.Compose(entity);
            using (var sw = new StreamWriter(markdownFilePath))
            {
                sw.Write(markdown);
            }
        }
    }
}
