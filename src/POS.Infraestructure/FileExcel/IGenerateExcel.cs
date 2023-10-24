using POS.Infraestructure.Commons.Bases.Response;
using POS.Utilities.Static;

namespace POS.Infraestructure.FileExcel
{
    public interface IGenerateExcel
    {
        MemoryStream GenerateToExcel<T>(BaseEntityResponse<T> data,
            List<TableColumn> columns);
    
    }
}
