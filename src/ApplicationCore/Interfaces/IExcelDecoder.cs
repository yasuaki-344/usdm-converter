using NPOI.SS.UserModel;
using UsdmConverter.ApplicationCore.Entities;

namespace UsdmConverter.ApplicationCore.Interfaces
{
    public interface IExcelDecoder
    {
        IWorkbook Decode(RequirementSpecification data);
    }
}
