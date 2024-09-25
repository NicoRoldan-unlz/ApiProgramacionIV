using ApiProgramacionIV.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProgramacionIV.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<RolModel> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().HasKey(usuario => usuario.Id_Usuario);
            modelBuilder.Entity<RolModel>().HasKey(rol => rol.Id_Rol);

            base.OnModelCreating(modelBuilder);
        }
    }
}
