// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Services;
using Xunit;

namespace UsdmConverter.ApplicatonCore.Test
{
    public class ExcelEncoderTest
    {
        [Fact]
        public void EncodeCorrectly()
        {
            var book = new XSSFWorkbook();
            book.CreateSheet("title");
            var sheet = book.GetSheet("title");

            sheet.CreateRow(0).CreateCell(0).SetCellValue("要求");
            sheet.GetRow(0).CreateCell(1).SetCellValue("REQ01");
            sheet.GetRow(0).CreateCell(2).SetCellValue("requirement1");
            sheet.CreateRow(1).CreateCell(1).SetCellValue("理由");
            sheet.GetRow(1).CreateCell(2).SetCellValue("reason1");
            sheet.CreateRow(2).CreateCell(1).SetCellValue("説明");
            sheet.GetRow(2).CreateCell(2).SetCellValue("description1");

            var target = new ExcelEncoder();
            var actual = target.Encode(book);

            Assert.Equal("title", actual.Title);
            foreach (var item in actual.Requirements)
            {
                Assert.Equal("REQ01", item.ID);
                Assert.Equal("requirement1", item.Summary);
            }
        }
    }
}
