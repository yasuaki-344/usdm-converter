using System;
using System.IO;
using Microsoft.Extensions.Logging;
using UsdmConverter.ApplicationCore.Interfaces;

namespace UsdmConverter.Infrastructure
{
    public class MarkdownReader : IMarkdownReader
    {
        private readonly ILogger<MarkdownReader> _logger;

        /// <summary>
        /// Initializes a new instance of MarkdownReader class given logger object.
        /// </summary>
        /// <param name="logger">logger object</param>
        public MarkdownReader(ILogger<MarkdownReader> logger)
        {
            _logger = logger;
        }


        public string ReadToEnd(string filePath)
        {
            try
            {
                using (var sr = new StreamReader(filePath))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
