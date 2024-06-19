

namespace KGRC_Evaluacion.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICoctelRepository Coctel { get; }

        IUsuariosRepository Usuario { get; }

        IBitacoraRepository Bitacora   { get; }

        int Save();

        Task<int> SaveAsync();
        

    }
}
