
using KGRC_Evaluacion.Interface;
using Microsoft.EntityFrameworkCore;
using KGRC_Evaluacion.Conexion;
using KGRC_Evaluacion.Conexion.Interface;
using KGRC_Evaluacion.Models;
using System.Data;
using Microsoft.AspNetCore.Identity;
using static KGRC_Evaluacion.Models.Usuarios;
using System.Net.NetworkInformation;

namespace KGRC_Evaluacion.Repositories
{
    public class UsuariosRepository : GenericRepository<USUARIOS>, IUsuariosRepository
    {
        private readonly IConfiguration _Configuration;
        private readonly CoctelDbContext _CoctelContext;
  
        public IUnitOfWork _unitOfWork;
        public ICoctelRepository _CoctelRepository;
        public UsuariosRepository(CoctelDbContext CoctelContext, IConfiguration Configuration) : base(CoctelContext)
        {
            this._Configuration = Configuration;
            _CoctelContext = CoctelContext;
            
        }
        public void Save<T>(T Register) where T : class
        {
            _CoctelDbContext.Set<T>().Add(Register);
            _CoctelDbContext.SaveChanges();
        }


        public async Task AddAsync(USUARIOS Usuario)
        {
            try
            {
                await _CoctelContext.USUARIOS.AddAsync(Usuario);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in SaveInformationUsuario: {ex.Message}");
                throw;
            }
        }

        public async Task AddAsync(ViewModelLogin Usuariologin)
        {

            try
            {
                if (Usuariologin == null)
                {
                    throw new ArgumentNullException(nameof(Usuariologin));
                }

                USUARIOS newUser = new USUARIOS();

                newUser.Usuario = Usuariologin.Usuario;
                newUser.Contrasenia = Usuariologin.Contrasenia;
                newUser.Estatus = true;
                newUser.Fecha_Alta = DateTime.Now;
                
                await _CoctelContext.USUARIOS.AddAsync(newUser);
                await _CoctelContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                Console.WriteLine($" Error in SaveInformationUsuario: {ex.Message}");
                throw;
            }

        }

        public async Task<USUARIOS> GetInformationUsuario(int Userid)
        {

            return await GetById(Userid);
            
        }
        public async Task<ResponseUser> GetUserLoggin(ViewModelLogin ModelLogin)
        {
            var result = await _CoctelContext.USUARIOS.Where(x => x.Usuario == ModelLogin.Usuario && x.Contrasenia == ModelLogin.Contrasenia)
                .Select(x => new ResponseUser
                {
                    Id = x.idUser
                }).FirstOrDefaultAsync();

            return result ?? new ResponseUser();

        }
        public async Task<bool> SaveInformationUsuario(ViewModelLogin usuario)
        {
            
            try
            {
                if (usuario == null)
                {
                    throw new ArgumentNullException(nameof(usuario));
                }

                USUARIOS newUser = new USUARIOS();

                newUser.Usuario = usuario.Usuario;
                newUser.Contrasenia = usuario.Contrasenia;
                newUser.Estatus = true;
                newUser.Fecha_Alta = DateTime.Now;
                newUser.Fecha_Modificacion = DateTime.Now;

                await _CoctelContext.USUARIOS.AddAsync(newUser);
                await _CoctelContext.SaveChangesAsync();


                return  true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al SaveInformationUsuario: {ex.Message}");
                return false;

            }

        }

        public async Task<bool> UpdateInformationUsuario(int userId)
        {
            try
            {

                var usuario = await _CoctelContext.USUARIOS.FindAsync(userId);

                if (usuario == null)
                {

                    return false;
                }

                usuario.Estatus = false;
                usuario.Fecha_Modificacion = DateTime.Now;
                _CoctelContext.USUARIOS.Update(usuario);
                await _CoctelContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el usuario: {ex.Message}");
                return false;
            }
        }
    }
}
