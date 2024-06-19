
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KGRC_Evaluacion.Models
{
    [Table("COCTEL_FAVORITO")]
    public partial class COCTEL_FAVORITO
    {
        public int   IdCoctelFav { get; set; }

        [Required]
        public int idUser { get; set; }
        [Required]
        public int idDrink { get; set; }
        public DateTime Fecha_Alta { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }


    }
}
