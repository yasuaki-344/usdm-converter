// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
using NPOI.SS.UserModel;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Interfaces;

namespace UsdmConverter.ApplicationCore.Services
{
    public class ExcelEncoder : IExcelEncoder
    {
        /// <summary>
        /// Initializes a new instance of ExcelEncoder class.
        /// </summary>
        public ExcelEncoder()
        {
        }

        public RequirementSpecification Encode(IWorkbook book)
        {
            var result = new RequirementSpecification();

            var sheet = book.GetSheetAt(0);
            result.Title = sheet.SheetName;

            var rowIndex = 0;
            var row = sheet.GetRow(rowIndex);
            while (row != null)
            {
                if (sheet.GetRow(rowIndex).GetCell(0).StringCellValue.Equals("要求"))
                {
                    var id = sheet.GetRow(rowIndex).GetCell(1).StringCellValue;
                    var summary = sheet.GetRow(rowIndex).GetCell(2).StringCellValue;
                    rowIndex++;
                    var reason = sheet.GetRow(rowIndex).GetCell(2).StringCellValue;
                    rowIndex++;
                    var description = sheet.GetRow(rowIndex).GetCell(2).StringCellValue;
                    result.Requirements.Add(new UpperRequirement
                    {
                        ID = id,
                        Summary = summary,
                        Reason = reason,
                        Description = description,
                    });
                }
                rowIndex++;
                row = sheet.GetRow(rowIndex);
            }

            return result;
        }
    }
}
