using NPOI.SS.UserModel;

namespace UsdmConverter.ApplicationCore.Interfaces
{
    public interface IExcelWriter
    {
        void Write(IWorkbook book, string filePath);
    }
}
