using KGRC_Evaluacion.Models;
using KGRC_Evaluacion.Interface;
using KGRC_Evaluacion.Conexion;
using Microsoft.Win32;
using Microsoft.EntityFrameworkCore;

namespace KGRC_Evaluacion.Repositories
{
    public class BitacoraRepository : GenericRepository<BITACORA>, IBitacoraRepository
    {

        private readonly IConfiguration _Configuration;
        private readonly CoctelDbContext _CoctelContext;
        public IUnitOfWork _unitOfWork;
        public IUsuariosRepository _UsuariosRepository;

        public BitacoraRepository(CoctelDbContext CoctelContext, IConfiguration Configuration) : base(CoctelContext)
        {
            this._Configuration = Configuration;
            _CoctelContext = CoctelContext;

        }

        public void Save<T>(T Register) where T : class
        {
            _CoctelDbContext.Set<T>().Add(Register);
            _CoctelDbContext.SaveChanges();
        }
        public async Task AddAsync(BITACORA bitacora)
        {
            await _CoctelContext.BITACORA.AddAsync(bitacora);
        }

        public async Task<string> GetLatestBusqueda(int CurrentUser)
        {
            try
            {
                var latestBusqueda = await _CoctelContext.BITACORA
            .Where(b => b.idUser == CurrentUser)
            .OrderByDescending(b => b.idBitacora)
            .Select(b => b.Busqueda)
            .FirstOrDefaultAsync();

            return latestBusqueda != null ? latestBusqueda : "";
            }
            catch (Exception ex)
            {
                ConsolaMensaje("Error al obtener la última búsqueda en la bitácora:", ex);
                return string.Empty; 
            }

        }
        public async Task AgregarBitacora(string? CoctelFind, int ? idUser)
        {

            BITACORA Bitacora = new BITACORA();
            Bitacora.Fecha_Alta = DateTime.Now;
            Bitacora.Busqueda = CoctelFind;
            Bitacora.idUser = idUser;

            await _CoctelContext.AddAsync(Bitacora);
          
            await _CoctelContext.SaveChangesAsync();

        }

        public void ConsolaMensaje(string mensaje, Exception? ex = null)
        {
            if (ex != null)
            {
                Console.WriteLine($"{mensaje}: {ex.Message}");
            }
            else
            {
                Console.WriteLine(mensaje);
            }
        }
    }
}
