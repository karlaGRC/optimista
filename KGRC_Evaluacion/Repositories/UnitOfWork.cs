using KGRC_Evaluacion.Interface;
using KGRC_Evaluacion.Conexion.Interface;
using KGRC_Evaluacion.Conexion;
using KGRC_Evaluacion.Models;
using Azure;
using Microsoft.EntityFrameworkCore;
using static KGRC_Evaluacion.Models.Usuarios;


namespace KGRC_Evaluacion.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoctelDbContext _dbContext;

        public ICoctelRepository Coctel { get; }
        public IUsuariosRepository Usuario { get; }

        public IRestClient RestClient { get; }

        public IBitacoraRepository Bitacora { get; }

        public UnitOfWork(CoctelDbContext dbContext, ICoctelRepository coctelRepository, IUsuariosRepository usuariosRepository, IBitacoraRepository bitacora )
        {
            _dbContext = dbContext;
            Coctel = coctelRepository;
            Usuario = usuariosRepository;
            Bitacora = bitacora;
        }


        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }


    }

}
