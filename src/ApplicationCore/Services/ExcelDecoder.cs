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
            var style1 = book.CreateCellStyle();
            style1.BorderTop = BorderStyle.Thin;
            style1.BorderRight = BorderStyle.Thin;
            style1.BorderLeft = BorderStyle.Thin;
            style1.BorderBottom = BorderStyle.Thin;
            style1.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            style1.FillPattern = FillPattern.SolidForeground;

            var style3 = book.CreateCellStyle();
            style3.BorderTop = BorderStyle.Thin;
            style3.BorderRight = BorderStyle.Thin;
            style3.BorderLeft = BorderStyle.Thin;
            style3.BorderBottom = BorderStyle.None;
            style3.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            style3.FillPattern = FillPattern.SolidForeground;

            var style4 = book.CreateCellStyle();
            style4.BorderTop = BorderStyle.None;
            style4.BorderRight = BorderStyle.Thin;
            style4.BorderLeft = BorderStyle.Thin;
            style4.BorderBottom = BorderStyle.None;
            style4.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            style4.FillPattern = FillPattern.SolidForeground;

            var style5 = book.CreateCellStyle();
            style5.BorderTop = BorderStyle.None;
            style5.BorderRight = BorderStyle.Thin;
            style5.BorderLeft = BorderStyle.Thin;
            style5.BorderBottom = BorderStyle.Thin;
            style5.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            style5.FillPattern = FillPattern.SolidForeground;

            var style2 = book.CreateCellStyle();
            style2.BorderTop = BorderStyle.Thin;
            style2.BorderRight = BorderStyle.Thin;
            style2.BorderLeft = BorderStyle.Thin;
            style2.BorderBottom = BorderStyle.Thin;

            var sheet = book.GetSheet(data.Title);
            var rowIndex = 0;
            foreach (var element in data.Requirements)
            {
                WriteCell(sheet, 0, rowIndex, "要求");
                WriteCell(sheet, 1, rowIndex, element.ID);
                WriteCell(sheet, 2, rowIndex, element.Summay);
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));

                WriteStyle(sheet, 0, rowIndex, style3);
                WriteStyle(sheet, 1, rowIndex, style1);
                WriteStyle(sheet, 2, rowIndex, style1);
                WriteStyle(sheet, 3, rowIndex, style1);
                rowIndex++;
                WriteCell(sheet, 1, rowIndex, "理由");
                WriteCell(sheet, 2, rowIndex, element.Reason);
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));
                WriteStyle(sheet, 0, rowIndex, style4);
                WriteStyle(sheet, 1, rowIndex, style2);
                WriteStyle(sheet, 2, rowIndex, style2);
                WriteStyle(sheet, 3, rowIndex, style2);
                rowIndex++;
                WriteCell(sheet, 1, rowIndex, "説明");
                WriteCell(sheet, 2, rowIndex, element.Description);
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));
                WriteStyle(sheet, 0, rowIndex, style5);
                WriteStyle(sheet, 1, rowIndex, style2);
                WriteStyle(sheet, 2, rowIndex, style2);
                WriteStyle(sheet, 3, rowIndex, style2);
                rowIndex++;
                foreach (var item in element.Requirements)
                {
                    WriteCell(sheet, 1, rowIndex, "要求");
                    WriteCell(sheet, 2, rowIndex, item.ID);
                    WriteCell(sheet, 3, rowIndex, item.Summay);
                    WriteStyle(sheet, 1, rowIndex, style3);
                    WriteStyle(sheet, 2, rowIndex, style1);
                    WriteStyle(sheet, 3, rowIndex, style1);
                    rowIndex++;
                    WriteCell(sheet, 2, rowIndex, "理由");
                    WriteCell(sheet, 3, rowIndex, item.Reason);
                    WriteStyle(sheet, 1, rowIndex, style4);
                    WriteStyle(sheet, 2, rowIndex, style2);
                    WriteStyle(sheet, 3, rowIndex, style2);
                    rowIndex++;
                    WriteCell(sheet, 2, rowIndex, "説明");
                    WriteCell(sheet, 3, rowIndex, item.Description);
                    WriteStyle(sheet, 1, rowIndex, style5);
                    WriteStyle(sheet, 2, rowIndex, style2);
                    WriteStyle(sheet, 3, rowIndex, style2);
                    rowIndex++;
                    foreach (var group in item.SpecificationGroups)
                    {
                        WriteCell(sheet, 2, rowIndex, group.Category);
                        WriteStyle(sheet, 1, rowIndex, style2);
                        WriteStyle(sheet, 2, rowIndex, style2);
                        WriteStyle(sheet, 3, rowIndex, style2);

                        sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));

                        rowIndex++;
                        foreach (var spec in group.Specifications)
                        {
                            WriteCell(sheet, 1, rowIndex, spec.IsImplemented.ToString());
                            WriteCell(sheet, 2, rowIndex, spec.ID);
                            WriteCell(sheet, 3, rowIndex, spec.Description);
                            WriteStyle(sheet, 1, rowIndex, style2);
                            WriteStyle(sheet, 2, rowIndex, style2);
                            WriteStyle(sheet, 3, rowIndex, style2);

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
