using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Request;
using POS.Infraestructure.Commons.Bases.Response;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters);
        //Task<IEnumerable<Category>> ListSelectCategories();
        //Task<Category> CategoryById(int categoryId);
        //Task<bool> RegisterCategory(Category category);
        //Task<bool> EditCategory(Category category);
        //Task<bool> RemoveCategory(int categoryId);
    }
}
