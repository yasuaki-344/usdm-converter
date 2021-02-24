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
                    services.AddScoped<IMarkdownParser, MarkdownParser>();
                })
                .RunConsoleAppFrameworkAsync<ApplicationLogic>(args);
        }
    }

    public class ApplicationLogic : ConsoleAppBase
    {
        private readonly ILogger<ApplicationLogic> _logger;


        /// <summary>
        /// Initializes a new instance of ApplicationLogic class.
        /// </summary>
        /// <param name="logger">logger object</param>
        public ApplicationLogic(
            ILogger<ApplicationLogic> logger
        )
        {
            _logger = logger;
        }

        [Command("xlsx", "convert markdown to excel")]
        public void ConvertMarkdownToExcel(
            [Option("i", "input markdown file path.")] string markdownFilePath,
            [Option("o", "output Excel file path.")] string excelFilePath
        )
        {
            throw new NotImplementedException();
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
