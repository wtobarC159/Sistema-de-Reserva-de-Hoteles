using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Reserva_de_Hoteles.Modelos;

namespace Sistema_de_Reserva_de_Hoteles.Data
{
    public class DataContext : IdentityDbContext<UsuarioApp>
    {
        public DataContext(DbContextOptions options) : base(options){}

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Reservacion> Reservaciones { get; set; }
        public DbSet<UsuarioApp> UsuarioApps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Reservacion>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservaciones)
                .HasForeignKey(r => r.ClienteId);

            builder.Entity<Reservacion>()
                .HasOne(r => r.UsuarioApp)
                .WithMany(u => u.Reservaciones)
                .HasForeignKey(r => r.UsuarioAppId);

            List<IdentityRole> Roles = new List<IdentityRole>() 
            {
                new IdentityRole()
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole()
                {
                    Id = "2",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            builder.Entity<IdentityRole>().HasData(Roles);
        }
    }
}
