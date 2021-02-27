// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UsdmConverter.Infrastructure.Test
{
    public class MarkdownReaderTest
    {
        [Fact]
        public void ThrowArgumentNullExceptionGivenEmtpyFilePath()
        {
            var logger = new Mock<ILogger<MarkdownReader>>();
            var target = new MarkdownReader(logger.Object);
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                target.ReadToEnd(string.Empty);
            });
        }

        [Fact]
        public void ThrowExceptionGivenNonExistFilePath()
        {
            var logger = new Mock<ILogger<MarkdownReader>>();
            var target = new MarkdownReader(logger.Object);
            var ex = Assert.Throws<FileNotFoundException>(() =>
            {
                target.ReadToEnd("xxxxxxxx/xxxxxx/xxxx");
            });
        }
    }
}
