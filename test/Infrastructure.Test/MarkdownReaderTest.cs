﻿using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UsdmConverter.Infrastructure.Test
{
    public class UnitTest1
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
