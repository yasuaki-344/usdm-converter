using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Moq;
using NPOI.SS.UserModel;
using Xunit;

namespace UsdmConverter.Infrastructure.Test
{
    public class UnitTest1
    {
        [Fact]
        public void ThrowArgumentNullExceptionGivenEmtpyFilePath()
        {
            var logger = new Mock<ILogger<ExcelWriter>>();
            var target = new ExcelWriter(logger.Object);
            var book = new Mock<IWorkbook>();
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                target.Write(book.Object, string.Empty);
            });
        }

        [Fact]
        public void ThrowExceptionGivenWrongFilePath()
        {
            var logger = new Mock<ILogger<ExcelWriter>>();
            var target = new ExcelWriter(logger.Object);
            var book = new Mock<IWorkbook>();
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                target.Write(book.Object, "path.txt");

            });
            Assert.Contains("extention is not xlsx", ex.Message);
        }

        [Fact]
        public void ThrowIOExceptionIfIOErrorOccurred()
        {
            var logger = new Mock<ILogger<ExcelWriter>>();
            var target = new ExcelWriter(logger.Object);
            var book = new Mock<IWorkbook>();
            book.Setup(c => c.Write(It.IsAny<FileStream>()))
                .Throws(new IOException());
            var ex = Assert.Throws<IOException>(() =>
            {
                target.Write(book.Object, "path.xlsx");
            });
        }

        [Fact]
        public void WriteCorrectly()
        {
            var logger = new Mock<ILogger<ExcelWriter>>();
            var target = new ExcelWriter(logger.Object);
            var book = new Mock<IWorkbook>();
            target.Write(book.Object, "path.xlsx");
        }
    }
}
