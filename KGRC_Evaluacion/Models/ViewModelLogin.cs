using System.ComponentModel.DataAnnotations;

namespace KGRC_Evaluacion.Models
{
    public class ViewModelLogin
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string ?Usuario { get; set; }

        
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 20 caracteres.")]
        [DataType(DataType.Password)]
        public string ?Contrasenia { get; set; }

       
    }
}
