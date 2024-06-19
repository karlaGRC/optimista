using KGRC_Evaluacion.Models;

namespace KGRC_Evaluacion.Interface
{
    public interface IBitacoraRepository :IGenericRepository<BITACORA>
    {
        void Save<T>(T Register) where T : class;

        Task AddAsync(BITACORA bITACORA);

        Task<string> GetLatestBusqueda(int CurrentUser);

        void ConsolaMensaje(string mensaje, Exception? ex = null);

        Task AgregarBitacora(string? CoctelFind, int? idUser);
    }
}
