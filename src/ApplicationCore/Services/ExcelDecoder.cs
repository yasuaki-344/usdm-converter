using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using NPOI.XSSF.Util;
using UsdmConverter.ApplicationCore.Entities;
using UsdmConverter.ApplicationCore.Interfaces;
namespace UsdmConverter.ApplicationCore.Services
{
    public class ExcelDecoder : IExcelDecoder
    {
        /// <summary>
        /// Initializes a new instance of ExcelDecoder class.
        /// </summary>
        public ExcelDecoder()
        {
        }

        public IWorkbook Decode(RequirementSpecification data)
        {
            var book = new XSSFWorkbook();
            book.CreateSheet(data.Title);
            var headingStyle = book.CreateCellStyle();
            headingStyle.BorderTop = BorderStyle.Thin;
            headingStyle.BorderRight = BorderStyle.Thin;
            headingStyle.BorderLeft = BorderStyle.Thin;
            headingStyle.BorderBottom = BorderStyle.Thin;
            headingStyle.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            headingStyle.FillPattern = FillPattern.SolidForeground;

            var upperHeadingStyle = book.CreateCellStyle();
            upperHeadingStyle.BorderTop = BorderStyle.Thin;
            upperHeadingStyle.BorderRight = BorderStyle.Thin;
            upperHeadingStyle.BorderLeft = BorderStyle.Thin;
            upperHeadingStyle.BorderBottom = BorderStyle.None;
            upperHeadingStyle.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            upperHeadingStyle.FillPattern = FillPattern.SolidForeground;

            var mediumHeadingStyle = book.CreateCellStyle();
            mediumHeadingStyle.BorderTop = BorderStyle.None;
            mediumHeadingStyle.BorderRight = BorderStyle.Thin;
            mediumHeadingStyle.BorderLeft = BorderStyle.Thin;
            mediumHeadingStyle.BorderBottom = BorderStyle.None;
            mediumHeadingStyle.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            mediumHeadingStyle.FillPattern = FillPattern.SolidForeground;

            var lowerHeadingStyle = book.CreateCellStyle();
            lowerHeadingStyle.BorderTop = BorderStyle.None;
            lowerHeadingStyle.BorderRight = BorderStyle.Thin;
            lowerHeadingStyle.BorderLeft = BorderStyle.Thin;
            lowerHeadingStyle.BorderBottom = BorderStyle.Thin;
            lowerHeadingStyle.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            lowerHeadingStyle.FillPattern = FillPattern.SolidForeground;

            var baseStyle = book.CreateCellStyle();
            baseStyle.BorderTop = BorderStyle.Thin;
            baseStyle.BorderRight = BorderStyle.Thin;
            baseStyle.BorderLeft = BorderStyle.Thin;
            baseStyle.BorderBottom = BorderStyle.Thin;

            var sheet = book.GetSheet(data.Title);
            var rowIndex = 0;
            foreach (var element in data.Requirements)
            {
                WriteCell(sheet, 0, rowIndex, "要求");
                WriteCell(sheet, 1, rowIndex, element.ID);
                WriteCell(sheet, 2, rowIndex, element.Summay);
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));

                WriteStyle(sheet, 0, rowIndex, upperHeadingStyle);
                WriteStyle(sheet, 1, rowIndex, headingStyle);
                WriteStyle(sheet, 2, rowIndex, headingStyle);
                WriteStyle(sheet, 3, rowIndex, headingStyle);
                rowIndex++;
                WriteCell(sheet, 1, rowIndex, "理由");
                WriteCell(sheet, 2, rowIndex, element.Reason);
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));
                WriteStyle(sheet, 0, rowIndex, mediumHeadingStyle);
                WriteStyle(sheet, 1, rowIndex, baseStyle);
                WriteStyle(sheet, 2, rowIndex, baseStyle);
                WriteStyle(sheet, 3, rowIndex, baseStyle);
                rowIndex++;
                WriteCell(sheet, 1, rowIndex, "説明");
                WriteCell(sheet, 2, rowIndex, element.Description);
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));
                WriteStyle(sheet, 0, rowIndex, lowerHeadingStyle);
                WriteStyle(sheet, 1, rowIndex, baseStyle);
                WriteStyle(sheet, 2, rowIndex, baseStyle);
                WriteStyle(sheet, 3, rowIndex, baseStyle);
                rowIndex++;
                foreach (var item in element.Requirements)
                {
                    WriteCell(sheet, 1, rowIndex, "要求");
                    WriteCell(sheet, 2, rowIndex, item.ID);
                    WriteCell(sheet, 3, rowIndex, item.Summay);
                    WriteStyle(sheet, 1, rowIndex, upperHeadingStyle);
                    WriteStyle(sheet, 2, rowIndex, headingStyle);
                    WriteStyle(sheet, 3, rowIndex, headingStyle);
                    rowIndex++;
                    WriteCell(sheet, 2, rowIndex, "理由");
                    WriteCell(sheet, 3, rowIndex, item.Reason);
                    WriteStyle(sheet, 1, rowIndex, mediumHeadingStyle);
                    WriteStyle(sheet, 2, rowIndex, baseStyle);
                    WriteStyle(sheet, 3, rowIndex, baseStyle);
                    rowIndex++;
                    WriteCell(sheet, 2, rowIndex, "説明");
                    WriteCell(sheet, 3, rowIndex, item.Description);
                    WriteStyle(sheet, 1, rowIndex, lowerHeadingStyle);
                    WriteStyle(sheet, 2, rowIndex, baseStyle);
                    WriteStyle(sheet, 3, rowIndex, baseStyle);
                    rowIndex++;
                    foreach (var group in item.SpecificationGroups)
                    {
                        WriteCell(sheet, 2, rowIndex, group.Category);
                        WriteStyle(sheet, 1, rowIndex, baseStyle);
                        WriteStyle(sheet, 2, rowIndex, baseStyle);
                        WriteStyle(sheet, 3, rowIndex, baseStyle);

                        sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));

                        rowIndex++;
                        foreach (var spec in group.Specifications)
                        {
                            WriteCell(sheet, 1, rowIndex, spec.IsImplemented.ToString());
                            WriteCell(sheet, 2, rowIndex, spec.ID);
                            WriteCell(sheet, 3, rowIndex, spec.Description);
                            WriteStyle(sheet, 1, rowIndex, baseStyle);
                            WriteStyle(sheet, 2, rowIndex, baseStyle);
                            WriteStyle(sheet, 3, rowIndex, baseStyle);

                            rowIndex++;
                        }
                    }
                }
            }
            sheet.SetColumnWidth(1, 256 * 12);
            sheet.SetColumnWidth(2, 256 * 15);
            return book;
        }

        private void WriteCell(ISheet sheet, int columnIndex, int rowIndex, string value)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

            cell.SetCellValue(value);
        }

        public static void WriteStyle(ISheet sheet, int columnIndex, int rowIndex, ICellStyle style)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

            cell.CellStyle = style;
        }
    }
}
