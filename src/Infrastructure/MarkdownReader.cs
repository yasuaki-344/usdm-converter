﻿// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

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
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{filePath} does not exist");
            }

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
