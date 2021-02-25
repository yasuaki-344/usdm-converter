using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
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

            var font = book.CreateFont();
            font.FontName = "Yu Gothic Medium";

            var headingStyle = CreateHeadingCellStyle(book, font);
            var upperHeadingStyle = CreateUpperHeadingCellStyle(book, font);
            var mediumHeadingStyle = CreateMediumHeadingCellStyle(book, font);
            var lowerHeadingStyle = CreateLowerHeadingCellStyle(book, font);
            var baseStyle = CreateBasicCellStyle(book, font);
            var itemStyle = CreateItemCellStyle(book, font);

            var sheet = book.GetSheet(data.Title);

            var rowIndex = 0;
            foreach (var element in data.Requirements)
            {
                // 上位要求の項目の作成
                WriteCell(sheet, 0, rowIndex, "要求");
                WriteCell(sheet, 1, rowIndex, element.ID);
                WriteCell(sheet, 2, rowIndex, element.Summary);
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));
                WriteStyle(sheet, 0, rowIndex, upperHeadingStyle);
                WriteStyle(sheet, 1, rowIndex, upperHeadingStyle);
                WriteStyle(sheet, 2, rowIndex, headingStyle);
                WriteStyle(sheet, 3, rowIndex, headingStyle);
                rowIndex++;

                // 上位要求の理由の作成
                WriteCell(sheet, 1, rowIndex, "理由");
                WriteCell(sheet, 2, rowIndex, element.Reason);
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));
                WriteStyle(sheet, 0, rowIndex, mediumHeadingStyle);
                WriteStyle(sheet, 1, rowIndex, itemStyle);
                WriteStyle(sheet, 2, rowIndex, baseStyle);
                WriteStyle(sheet, 3, rowIndex, baseStyle);
                rowIndex++;

                // 上位要求の説明の作成
                WriteCell(sheet, 1, rowIndex, "説明");
                WriteCell(sheet, 2, rowIndex, element.Description);
                sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));
                WriteStyle(sheet, 0, rowIndex, lowerHeadingStyle);
                WriteStyle(sheet, 1, rowIndex, itemStyle);
                WriteStyle(sheet, 2, rowIndex, baseStyle);
                WriteStyle(sheet, 3, rowIndex, baseStyle);
                rowIndex++;

                foreach (var item in element.Requirements)
                {
                    // 下位要求の項目の作成
                    WriteCell(sheet, 1, rowIndex, "要求");
                    WriteCell(sheet, 2, rowIndex, item.ID);
                    WriteCell(sheet, 3, rowIndex, item.Summary);
                    WriteStyle(sheet, 1, rowIndex, upperHeadingStyle);
                    WriteStyle(sheet, 2, rowIndex, upperHeadingStyle);
                    WriteStyle(sheet, 3, rowIndex, headingStyle);
                    rowIndex++;

                    // 下位要求の理由の作成
                    WriteCell(sheet, 2, rowIndex, "理由");
                    WriteCell(sheet, 3, rowIndex, item.Reason);
                    WriteStyle(sheet, 1, rowIndex, mediumHeadingStyle);
                    WriteStyle(sheet, 2, rowIndex, itemStyle);
                    WriteStyle(sheet, 3, rowIndex, baseStyle);
                    rowIndex++;

                    // 下位要求の説明の作成
                    WriteCell(sheet, 2, rowIndex, "説明");
                    WriteCell(sheet, 3, rowIndex, item.Description);
                    WriteStyle(sheet, 1, rowIndex, lowerHeadingStyle);
                    WriteStyle(sheet, 2, rowIndex, itemStyle);
                    WriteStyle(sheet, 3, rowIndex, baseStyle);
                    rowIndex++;

                    foreach (var group in item.SpecificationGroups)
                    {
                        // 仕様グループの作成
                        WriteCell(sheet, 2, rowIndex, group.Category);
                        WriteStyle(sheet, 1, rowIndex, baseStyle);
                        WriteStyle(sheet, 2, rowIndex, baseStyle);
                        WriteStyle(sheet, 3, rowIndex, baseStyle);
                        sheet.AddMergedRegion(new CellRangeAddress(rowIndex, rowIndex, 2, 3));

                        rowIndex++;
                        foreach (var spec in group.Specifications)
                        {
                            // 仕様の記述
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
            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
            sheet.SetColumnWidth(3, 256 * 87 - (
                sheet.GetColumnWidth(0) +
                sheet.GetColumnWidth(1) +
                sheet.GetColumnWidth(2)
            ));
            sheet.PrintSetup.PaperSize = 9;
            return book;
        }
        private ICellStyle CreateBasicCellStyle(IWorkbook book, IFont font)
        {
            var cellStyle = book.CreateCellStyle();
            // font
            cellStyle.SetFont(font);
            // boreder style
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            // text alignment
            cellStyle.WrapText = true;
            return cellStyle;
        }

        private ICellStyle CreateItemCellStyle(IWorkbook book, IFont font)
        {
            var cellStyle = book.CreateCellStyle();
            // font
            cellStyle.SetFont(font);
            // boreder style
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            // text alignment
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Top;
            cellStyle.WrapText = true;
            return cellStyle;
        }

        private ICellStyle CreateHeadingCellStyle(IWorkbook book, IFont font)
        {
            var cellStyle = book.CreateCellStyle();
            // font
            cellStyle.SetFont(font);
            // boreder style
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            // background color
            cellStyle.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;

            // text alignment
            cellStyle.Alignment = HorizontalAlignment.Left;
            cellStyle.VerticalAlignment = VerticalAlignment.Top;
            cellStyle.WrapText = true;
            return cellStyle;
        }

        private ICellStyle CreateUpperHeadingCellStyle(IWorkbook book, IFont font)
        {
            var cellStyle = book.CreateCellStyle();
            // font
            cellStyle.SetFont(font);
            // boreder style
            cellStyle.BorderTop = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.None;
            // background color
            cellStyle.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            // text alignment
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Top;
            cellStyle.WrapText = true;
            // font
            cellStyle.SetFont(font);
            return cellStyle;
        }
        private ICellStyle CreateMediumHeadingCellStyle(IWorkbook book, IFont font)
        {
            var cellStyle = book.CreateCellStyle();
            // font
            cellStyle.SetFont(font);
            // boreder style
            cellStyle.BorderTop = BorderStyle.None;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.None;
            // background color
            cellStyle.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            // text alignment
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Top;
            cellStyle.WrapText = true;
            // font
            cellStyle.SetFont(font);
            return cellStyle;
        }

        private ICellStyle CreateLowerHeadingCellStyle(IWorkbook book, IFont font)
        {
            var cellStyle = book.CreateCellStyle();
            // font
            cellStyle.SetFont(font);
            // boreder style
            cellStyle.BorderTop = BorderStyle.None;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderBottom = BorderStyle.Thin;
            // background color
            cellStyle.FillForegroundColor = IndexedColors.LightTurquoise.Index;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            // text alignment
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Top;
            cellStyle.WrapText = true;
            // font
            cellStyle.SetFont(font);
            return cellStyle;
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
