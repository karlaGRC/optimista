using KGRC_Evaluacion.Models;
using KGRC_Evaluacion.Repositories;
using static KGRC_Evaluacion.Models.Usuarios;

namespace KGRC_Evaluacion.Interface
{
    public interface IUsuariosRepository : IGenericRepository<USUARIOS>
    {
        void Save<T>(T Register) where T : class;

        Task AddAsync(USUARIOS usuario);
        Task AddAsync(ViewModelLogin usuario);

        Task<USUARIOS> GetInformationUsuario(int CurrentUser);

        Task<ResponseUser> GetUserLoggin(ViewModelLogin ModelLogin);

        Task<bool> SaveInformationUsuario(ViewModelLogin usuario);

        Task<bool> UpdateInformationUsuario( int userId);


    }
}
