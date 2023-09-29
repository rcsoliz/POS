using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class CategoryRepository: GenericRepository<Category>,
        ICategoryRepository
    {
        private readonly POSContext _context;

        public CategoryRepository(POSContext context)
        {
            _context = context;
        }
    }
}
