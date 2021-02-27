// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System;
using System.IO;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UsdmConverter.ApplicationCore.Interfaces;

namespace UsdmConverter.Infrastructure
{
    public class ExcelReader : IExcelReader
    {
        private readonly ILogger<ExcelReader> _logger;

        /// <summary>
        /// Initializes a new instance of ExcelReader class given logger object.
        /// </summary>
        /// <param name="logger">logger object</param>
        public ExcelReader(ILogger<ExcelReader> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Output given workbook to the specified excel file path.
        /// </summary>
        /// <param name="filePath">excel file path</param>
        /// <returns>excel workbook</returns>
        public IWorkbook Read(string filePath)
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
                return new XSSFWorkbook(filePath);
            }
            catch (IOException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
