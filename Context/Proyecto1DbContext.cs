using Microsoft.EntityFrameworkCore;
using Prueba_proyecto_1.Models;

namespace Prueba_proyecto_1.Context
{
    public class Proyecto1DbContext: DbContext
    {
        public Proyecto1DbContext(DbContextOptions<Proyecto1DbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<GeneroClass> Genero { get; set; }
        public DbSet<AutorClass> Autor { get; set; }
        public DbSet<LibroClass> Libro { get; set; }
    }
}
