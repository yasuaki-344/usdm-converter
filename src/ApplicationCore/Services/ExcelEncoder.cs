// Copyright (c) 2021 Yasuaki Miyoshi
//
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php
using System.Linq;
using NPOI.SS.UserModel;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Exceptions;
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
                var cell = row.GetCell(0);
                if (cell != null)
                {
                    var label = cell.StringCellValue;
                    if (label.Equals("要求"))
                    {
                        var id = row.GetCell(1).StringCellValue;
                        var summary = row.GetCell(2).StringCellValue;

                        row = sheet.GetRow(++rowIndex);
                        var reason = row.GetCell(2).StringCellValue;

                        row = sheet.GetRow(++rowIndex);
                        var description = row.GetCell(2).StringCellValue;
                        result.Requirements.Add(new UpperRequirement
                        {
                            ID = id,
                            Summary = summary,
                            Reason = reason,
                            Description = description,
                        });
                    }
                    else
                    {
                        throw new UsdmFormatException($"Unknown label: {label}");
                    }
                }
                else
                {
                    cell = row.GetCell(1);
                    if (cell != null)
                    {
                        var label = cell.StringCellValue;
                        if (label.Equals("要求"))
                        {
                            var id = row.GetCell(2).StringCellValue;
                            var summary = row.GetCell(3).StringCellValue;

                            row = sheet.GetRow(++rowIndex);
                            var reason = row.GetCell(3).StringCellValue;

                            row = sheet.GetRow(++rowIndex);
                            var description = row.GetCell(3).StringCellValue;

                            result.Requirements.Last()
                                .Requirements.Add(new LowerRequirement
                                {
                                    ID = id,
                                    Summary = summary,
                                    Reason = reason,
                                    Description = description,
                                });
                        }
                        else if (label.Equals(string.Empty))
                        {
                            var category = row.GetCell(2).StringCellValue;
                            result.Requirements.Last()
                                .Requirements.Last()
                                .SpecificationGroups.Add(
                                    new SpecificationGroup
                                    {
                                        Category = category
                                    }
                                );
                        }
                        else if (label.Equals("■") || label.Equals("□"))
                        {
                            var isImplemented = label.Equals("■");
                            var id = row.GetCell(2).StringCellValue;
                            var description = row.GetCell(3).StringCellValue;
                            result.Requirements.Last()
                                .Requirements.Last()
                                .SpecificationGroups.Last()
                                .Specifications.Add(
                                    new Specification
                                    {
                                        IsImplemented = isImplemented,
                                        ID = id,
                                        Description = description
                                    }
                                );
                        }
                        else
                        {
                            throw new UsdmFormatException($"Unknown sublabel: {label}");
                        }
                    }
                }
                row = sheet.GetRow(++rowIndex);
            }

            return result;
        }
    }
}
