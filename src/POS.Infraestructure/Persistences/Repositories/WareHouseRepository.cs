using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class WareHouseRepository: GenericRepository<Warehouse>, IWareHouseRepository
    {
        public readonly POSContext _context;

        public WareHouseRepository(POSContext context): base(context) 
        {
            _context = context;
        }



    }
}
