
using KGRC_Evaluacion.Models;

namespace KGRC_Evaluacion.Interface
{
    public interface ICoctelRepository : IGenericRepository<COCTEL_FAVORITO>
    {
        void Save<T>(T Register) where T : class;

        Task AddAsync(COCTEL_FAVORITO coctel);

        Task RemoveAsync(COCTEL_FAVORITO coctel);

        Task<List<COCTEL_FAVORITO>> GetInformationCoctel(int CurrentUser);

        Task<List<COCTEL_FAVORITO>> GetInformationCoctelUser(int idDrink, int idUser);

        Task<List<COCTEL_FAVORITO>> DeleteInformationCoctel(int idDrink, int idUser);

        Task<bool> SaveInformationCoctel(List<COCTEL_FAVORITO> cocteles);

        Task SaveCoctel(int idDrink, int idUser);
    }


}
