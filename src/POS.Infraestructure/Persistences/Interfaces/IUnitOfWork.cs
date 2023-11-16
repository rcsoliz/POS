using POS.Domain.Entities;
using System.Data;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Declaracion o martricula de nuestras interfaces a nivel de repositorio.
        
        IGenericRepository<Category> Category { get; }
        IGenericRepository<Provider> Provider { get; }
        IGenericRepository<DocumentType> DocumentType { get; }
        IGenericRepository<Client> Client { get; }
        IUserRepository User { get; }
        IWareHouseRepository WareHouse { get; }
        IGenericRepository<Product> Product { get; }

        IProductStockRepository ProductStock { get; }

        void SaveChages();
        Task SaveChangeAsync();

        IDbTransaction BeginTransaction();
    }
}
