// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using NPOI.XSSF.UserModel;
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
            var row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("要求");
            row.CreateCell(1).SetCellValue("REQ01");
            row.CreateCell(2).SetCellValue("requirement1");

            row = sheet.CreateRow(1);
            row.CreateCell(1).SetCellValue("理由");
            row.CreateCell(2).SetCellValue("reason1");

            row = sheet.CreateRow(2);
            row.CreateCell(1).SetCellValue("説明");
            row.CreateCell(2).SetCellValue("description1");

            row = sheet.CreateRow(3);
            row.CreateCell(1).SetCellValue("要求");
            row.CreateCell(2).SetCellValue("REQ01-01");
            row.CreateCell(3).SetCellValue("requirement1-1");

            row = sheet.CreateRow(4);
            row.CreateCell(2).SetCellValue("理由");
            row.CreateCell(3).SetCellValue("reason1-1");

            row = sheet.CreateRow(5);
            row.CreateCell(2).SetCellValue("説明");
            row.CreateCell(3).SetCellValue("description1-1");

            row = sheet.CreateRow(6);
            row.CreateCell(1).SetCellValue(string.Empty);
            row.CreateCell(2).SetCellValue("<仕様グループ>");

            row = sheet.CreateRow(7);
            row.CreateCell(1).SetCellValue("□");
            row.CreateCell(2).SetCellValue("SPC01-01-01");
            row.CreateCell(3).SetCellValue("description1");

            row = sheet.CreateRow(8);
            row.CreateCell(1).SetCellValue("■");
            row.CreateCell(2).SetCellValue("SPC01-01-02");
            row.CreateCell(3).SetCellValue("description2");

            var target = new ExcelEncoder();
            var actual = target.Encode(book);

            Assert.Equal("title", actual.Title);
            foreach (var item in actual.Requirements)
            {
                Assert.Equal("REQ01", item.ID);
                Assert.Equal("requirement1", item.Summary);
                Assert.Equal("reason1", item.Reason);
                Assert.Equal("description1", item.Description);
                foreach (var requirement in item.Requirements)
                {
                    Assert.Equal("REQ01-01", requirement.ID);
                    Assert.Equal("requirement1-1", requirement.Summary);
                    Assert.Equal("reason1-1", requirement.Reason);
                    Assert.Equal("description1-1", requirement.Description);

                    foreach (var group in requirement.SpecificationGroups)
                    {
                        Assert.Equal("仕様グループ", group.Category);

                        Assert.False(group.Specifications[0].IsImplemented);
                        Assert.Equal("SPC01-01-01", group.Specifications[0].ID);
                        Assert.Equal("description1", group.Specifications[0].Description);

                        Assert.True(group.Specifications[1].IsImplemented);
                        Assert.Equal("SPC01-01-02", group.Specifications[1].ID);
                        Assert.Equal("description2", group.Specifications[1].Description);
                    }
                }
            }
        }
    }
}
