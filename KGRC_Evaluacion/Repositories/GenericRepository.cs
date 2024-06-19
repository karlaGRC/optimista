using KGRC_Evaluacion.Interface;
using KGRC_Evaluacion.Repositories;
using KGRC_Evaluacion.Conexion.Interface;
using KGRC_Evaluacion.Conexion;
using Microsoft.EntityFrameworkCore;

namespace KGRC_Evaluacion.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CoctelDbContext _CoctelDbContext;

        //Base de datos
        protected GenericRepository(CoctelDbContext  CoctelDbContex)
        {
            _CoctelDbContext = CoctelDbContex;
        }
        public async Task<T> GetById(int id)
        {
            var entity = await _CoctelDbContext.Set<T>().FindAsync(id);
            return entity ?? throw new Exception($"No se encontró una entidad de tipo {typeof(T).Name} con ID {id}");
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            return await _CoctelDbContext.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _CoctelDbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _CoctelDbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _CoctelDbContext.Set<T>().Update(entity);
        }


    }
}
