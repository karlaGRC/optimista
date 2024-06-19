using KGRC_Evaluacion.Conexion;
using KGRC_Evaluacion.Interface;
using KGRC_Evaluacion.Models;
using KGRC_Evaluacion.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading;


namespace KGRC_Evaluacion.Servicios
{
    public class CoctelServices
    {
        private readonly IUnitOfWork _unitOfWork;
       
        public CoctelServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
         }

        public async Task<List<COCTEL_FAVORITO>> GetInformationCoctel(int idUser)
        {

         
            try
            {
                var result = await _unitOfWork.Coctel.GetInformationCoctel(idUser);

                return result;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error en GetInformationCoctel: {ex.Message}");
                throw; 
            }
        }

        public async Task<List<COCTEL_FAVORITO>> DeleteInformationCoctel(int idUser, string idDrink)
        {
            var result = await _unitOfWork.Coctel.GetInformationCoctelUser(int.Parse(idDrink), idUser);

            foreach (var coctel in result)
            {
                await _unitOfWork.Coctel.RemoveAsync(coctel);
            }

            await _unitOfWork.SaveAsync(); 

            return await _unitOfWork.Coctel.GetInformationCoctel(idUser);
        }

    }
}
