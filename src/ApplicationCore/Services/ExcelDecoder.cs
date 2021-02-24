using NPOI.SS.UserModel;
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

            var sheet = book.GetSheet(data.Title);
            var rowIndex = 0;
            foreach (var element in data.Requirements)
            {
                WriteCell(sheet, 0, rowIndex, "要求");
                WriteCell(sheet, 1, rowIndex, element.ID);
                WriteCell(sheet, 2, rowIndex, element.Summay);
                rowIndex++;
                WriteCell(sheet, 1, rowIndex, "理由");
                WriteCell(sheet, 2, rowIndex, element.Reason);
                rowIndex++;
                WriteCell(sheet, 1, rowIndex, "説明");
                WriteCell(sheet, 2, rowIndex, element.Description);
                rowIndex++;
                foreach (var item in element.Requirements)
                {
                    WriteCell(sheet, 1, rowIndex, "要求");
                    WriteCell(sheet, 2, rowIndex, item.ID);
                    WriteCell(sheet, 3, rowIndex, item.Summay);
                    rowIndex++;
                    WriteCell(sheet, 2, rowIndex, "理由");
                    WriteCell(sheet, 3, rowIndex, item.Reason);
                    rowIndex++;
                    WriteCell(sheet, 2, rowIndex, "説明");
                    WriteCell(sheet, 3, rowIndex, item.Description);
                    rowIndex++;
                    foreach (var group in item.SpecificationGroups)
                    {
                        WriteCell(sheet, 2, rowIndex, group.Category);
                        rowIndex++;
                        foreach (var spec in group.Specifications)
                        {
                            WriteCell(sheet, 1, rowIndex, spec.IsImplemented.ToString());
                            WriteCell(sheet, 2, rowIndex, spec.ID);
                            WriteCell(sheet, 3, rowIndex, spec.Description);
                            rowIndex++;
                        }
                    }
                }
            }
            return book;
        }

        private void WriteCell(ISheet sheet, int columnIndex, int rowIndex, string value)
        {
            var row = sheet.GetRow(rowIndex) ?? sheet.CreateRow(rowIndex);
            var cell = row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);

            cell.SetCellValue(value);
        }
    }
}
