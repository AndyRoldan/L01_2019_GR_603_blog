using Microsoft.EntityFrameworkCore;
namespace L01_2019_603.Models
{
    public class labContext : DbContext
    {
        public labContext(DbContextOptions<labContext> options): base(options)
        {

        }

         public DbSet<Usuarios> usuarios { get; set;}
        public DbSet<Calificaciones> Calificaciones { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
    }
}
