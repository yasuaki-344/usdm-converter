using NPOI.SS.UserModel;
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
            throw new System.NotImplementedException();
        }
    }
}
