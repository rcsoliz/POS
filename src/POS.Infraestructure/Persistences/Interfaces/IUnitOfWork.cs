using POS.Domain.Entities;

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

        void SaveChages();
        Task SaveChangeAsync();
    }
}
