using Microsoft.EntityFrameworkCore;
using Microsis.Names.Models;

namespace Microsis.Web.Services.Data
{
    /// <summary>
    /// Context per l'accesso al database
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProgettoUE> ProgettiUE { get; set; }
        public DbSet<Servizio> Servizi { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Foto> Foto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurazione per ProgettoUE
            modelBuilder.Entity<ProgettoUE>()
                .HasKey(p => p.ID);

            modelBuilder.Entity<ProgettoUE>()
                .Property(p => p.Titolo)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<ProgettoUE>()
                .Property(p => p.Tab_Name)
                .IsRequired()
                .HasMaxLength(50);

            // Configurazione per EntiCoinvolti (lista di stringhe)
            modelBuilder.Entity<ProgettoUE>()
                .Property(p => p.EntiCoinvolti)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            // Configurazione per Servizio
            modelBuilder.Entity<Servizio>()
                .HasKey(s => s.ID);

            modelBuilder.Entity<Servizio>()
                .Property(s => s.Titolo)
                .IsRequired()
                .HasMaxLength(200);

            // Configurazione per GalleriaFoto (lista di GUID)
            modelBuilder.Entity<Servizio>()
                .Property(s => s.GalleriaFoto)
                .HasConversion(
                    v => string.Join(',', v ?? new List<Guid>()),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(g => Guid.Parse(g))
                        .ToList()
                );

            // Configurazione per News
            modelBuilder.Entity<News>()
                .HasKey(n => n.ID);

            modelBuilder.Entity<News>()
                .Property(n => n.Titolo)
                .IsRequired()
                .HasMaxLength(200);

            // Configurazione per GalleriaFoto (lista di GUID)
            modelBuilder.Entity<News>()
                .Property(n => n.GalleriaFoto)
                .HasConversion(
                    v => string.Join(',', v ?? new List<Guid>()),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(g => Guid.Parse(g))
                        .ToList()
                );

            // Configurazione per User
            modelBuilder.Entity<User>()
                .HasKey(u => u.ID);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.NomeEsteso)
                .IsRequired()
                .HasMaxLength(100);

            // Configurazione per Foto
            modelBuilder.Entity<Foto>()
                .HasKey(f => f.ID);

            modelBuilder.Entity<Foto>()
                .Property(f => f.LocalPath)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Foto>()
                .Property(f => f.Filename)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Foto>()
                .Property(f => f.Title)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
