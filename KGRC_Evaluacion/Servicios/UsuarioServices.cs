using KGRC_Evaluacion.Interface;
using KGRC_Evaluacion.Models;
using static KGRC_Evaluacion.Models.Usuarios;

namespace KGRC_Evaluacion.Servicios
{
    public class UsuarioServices
    {
        private readonly IUnitOfWork _unitOfWork;
      
        public UsuarioServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<USUARIOS> GetInformationUsuario(int Userid)
        {

            return await _unitOfWork.Usuario.GetById(Userid);

        }

        public async Task<ResponseUser> GetUserLoggin(ViewModelLogin ModelLogin)
        {
            var result = await _unitOfWork.Usuario.GetUserLoggin(ModelLogin);
            return result;
        }
        


    }
}
