using Microsoft.Extensions.Configuration;
using POS.Infraestructure.FileStorage;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly POSContext _context;
        public ICategoryRepository Category { get; private set; }

        public IUserRepository User { get; private set; }

        public IAzureStorage Storage { get; private set; }

        public UnitOfWork(POSContext context, IConfiguration configuration)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            User = new UserRepository(_context);
            Storage = new AzureStorage(configuration);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChages()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
