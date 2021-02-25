using System;
using System.IO;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using UsdmConverter.ApplicationCore.Interfaces;

namespace UsdmConverter.Infrastructure
{
    public class ExcelWriter : IExcelWriter
    {
        private readonly ILogger<ExcelWriter> _logger;

        /// <summary>
        /// Initializes a new instance of ExcelWriter class given logger object.
        /// </summary>
        /// <param name="logger">logger object</param>
        public ExcelWriter(ILogger<ExcelWriter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Output given workbook to the specified excel file path.
        /// </summary>
        /// <param name="book">workbook to output</param>
        /// <param name="filePath">Output excel file path</param>
        public void Write(IWorkbook book, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            var extension = Path.GetExtension(filePath);
            if (extension != ".xlsx")
            {
                throw new ArgumentException("extention is not xlsx");
            }

            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    book.Write(fs);
                }
            }
            catch (IOException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
