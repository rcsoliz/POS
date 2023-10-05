using POS.Infraestructure.FileStorage;

namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Declaracion o martricula de nuestras interfaces a nivel de repositorio.
        ICategoryRepository Category { get; }
        IUserRepository User { get; }
        IAzureStorage Storage { get; }  

        void SaveChages();
        Task SaveChangeAsync();
    }
}
