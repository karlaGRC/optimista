using KGRC_Evaluacion.Repositories;
using KGRC_Evaluacion.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using KGRC_Evaluacion.Conexion;
using KGRC_Evaluacion.Conexion.Interface;
using Dapper;
using KGRC_Evaluacion.Models;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Identity;

namespace KGRC_Evaluacion.Repositories
{

    public class CoctelRepository : GenericRepository<COCTEL_FAVORITO>, ICoctelRepository
    {
        private readonly IConfiguration _Configuration;
        private readonly CoctelDbContext _CoctelContext;
   


        public CoctelRepository(CoctelDbContext CoctelContext, IConfiguration Configuration) : base(CoctelContext)
        {
            this._Configuration = Configuration;
            _CoctelContext = CoctelContext;

        }
        public void Save<T>(T Register) where T : class
        {
            _CoctelDbContext.Set<T>().Add(Register);
            _CoctelDbContext.SaveChanges();
        }

        public async Task<List<COCTEL_FAVORITO>> GetInformationCoctel(int CurrentUser)
        {

            return await _CoctelContext.COCTEL_FAVORITO.Where(x => x.idUser == CurrentUser).ToListAsync();

        }

        public async Task AddAsync(COCTEL_FAVORITO coctel)
        {
            await _CoctelContext.COCTEL_FAVORITO.AddAsync(coctel);
        }

        public async Task RemoveAsync(COCTEL_FAVORITO coctel)
        {
            _CoctelContext.COCTEL_FAVORITO.Remove(coctel);
            await _CoctelContext.SaveChangesAsync();
        }

        public async Task<List<COCTEL_FAVORITO>> DeleteInformationCoctel(int idDrink, int idUser)
        {

            var result = await GetInformationCoctelUser(idUser, idDrink);

            foreach (var coctel in result)
            {
                await RemoveAsync(coctel);
            }

            return await GetInformationCoctel(idUser);
        }

        public async Task<List<COCTEL_FAVORITO>> GetInformationCoctelUser(int idUser, int Drink)
        {

            try
            {
                var result = await _CoctelContext.COCTEL_FAVORITO
                                                          .Where(x => x.idUser == idUser && x.idDrink == Drink)
                                                          .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error en GetInformationCoctel: {ex.Message}");
                throw;
            }
        }

        public async Task SaveCoctel(int idDrink, int idUser)
        {
            COCTEL_FAVORITO FavouriteCoctel = new COCTEL_FAVORITO();
            FavouriteCoctel.idDrink = idDrink;
            FavouriteCoctel.idUser = idUser;
            FavouriteCoctel.Fecha_Alta = DateTime.Now;
            FavouriteCoctel.Fecha_Modificacion = DateTime.Now;

            await _CoctelContext.AddAsync(FavouriteCoctel);

            await _CoctelContext.SaveChangesAsync();

        }

        public async Task<bool> SaveInformationCoctel(List<COCTEL_FAVORITO> cocteles)
        {
            bool flag = false;
            foreach (var coctel in cocteles)
            {
                flag= await SaveInformationCoctel(cocteles);
            }
            return true;
        }

    }
}

