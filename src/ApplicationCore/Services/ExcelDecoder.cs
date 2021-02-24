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
            return book;
        }
    }
}
