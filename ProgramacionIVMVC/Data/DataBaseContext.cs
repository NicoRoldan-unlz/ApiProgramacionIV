using Microsoft.EntityFrameworkCore;
using ProgramacionIVMVC.Models;

namespace ProgramacionIVMVC.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<RolModel> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().HasKey(u => u.Id_Usuario);
            modelBuilder.Entity<RolModel>().HasKey(r => r.Id_Rol);
            base.OnModelCreating(modelBuilder);
        }
    }
}
