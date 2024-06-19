using KGRC_Evaluacion.Models;
using Microsoft.EntityFrameworkCore;
using static KGRC_Evaluacion.Models.Usuarios;

namespace KGRC_Evaluacion.Conexion
{
    public class CoctelDbContext : DbContext
    {
        public CoctelDbContext(DbContextOptions<CoctelDbContext> contextOptions) : base(contextOptions)
        {

        }
        public DbSet<USUARIOS> USUARIOS { get; set; }
        public DbSet<COCTEL_FAVORITO> COCTEL_FAVORITO { get; set; }
        public DbSet<BITACORA> BITACORA     { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<COCTEL_FAVORITO>().HasKey(m => new { m.IdCoctelFav });

            modelBuilder.Entity<USUARIOS>().HasKey(y => new { y.idUser });

            modelBuilder.Entity<BITACORA>().HasKey(x => new { x.idBitacora });
        }

    }
}
