using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KGRC_Evaluacion.Models
{
    [Table("BITACORA")]
    public class BITACORA
    {
        
        public  int idBitacora { get; set; }
        [Required]
        public int ? idUser { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "La busqueda debe tener entre 3 y 20 caracteres.")]
        public string? Busqueda { get; set; }
        public DateTime? Fecha_Alta { get; set; }
    }


}
