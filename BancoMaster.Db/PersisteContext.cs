using BancoMaster.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BancoMaster.Db
{
    public class PersisteContext : DbContext
    {
        public DbSet<rotas> Rotas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Bruno Rangel\OneDrive\Documentos\Projetos\BancoMaster\dbRotas.mdf"";Integrated Security=True;Connect Timeout=30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<rotas>(entity =>
           { 
            entity.HasKey(e => e.IdRota);
               entity.Property(e => e.Origem).IsRequired();
               entity.Property(e => e.Destino).IsRequired();
               entity.Property(e => e.Valor).IsRequired();

           });
        }

    }
}

