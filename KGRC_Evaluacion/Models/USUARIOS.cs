using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KGRC_Evaluacion.Models
{
    public class Usuarios
    {
        [Table("USUARIOS")]
        public partial class USUARIOS
        {
          public int  idUser { get; set; }

         [Required]
         [StringLength(50)]
         public string  ?Usuario  { get; set; }
         
         [Required]
         [StringLength(20)]
         public string  ?Contrasenia  { get; set; }
         public Boolean Estatus  { get; set; }
         public DateTime Fecha_Alta  { get; set; }
         public DateTime Fecha_Modificacion { get; set; }

        }
    }
}
