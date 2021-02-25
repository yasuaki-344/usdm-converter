using System;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Moq;
using NPOI.SS.UserModel;
using UsdmConverter.Infrastructure;
using Xunit;

namespace UsdmConverter.Infrastructure.Test
{
    public class UnitTest1
    {
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
    }
}
