namespace POS.Infraestructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Declaracion o martricula de nuestras interfaces a nivel de repositorio.
        ICategoryRepository Category { get; }

        void SaveChages();
        Task SaveChangeAsync();
    }
}
