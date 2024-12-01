using ElectroPromesa.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectroPromesa.Data
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<CandidatoPartido> CandidatoPartidos { get; set; }
        public DbSet<Promesa> Promesas { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<CandidatoPartido>()
        //        .HasKey(sc => new { sc.CandidatoId, sc.PartidoId });

        //    modelBuilder.Entity<CandidatoPartido>()
        //        .HasOne(sc => sc.Candidato)
        //        .WithMany(s => s.Partidos)
        //        .HasForeignKey(sc => sc.CandidatoId);

        //    modelBuilder.Entity<CandidatoPartido>()
        //        .HasOne(sc => sc.Partido)
        //        .WithMany(c => c.Candidatos)
        //        .HasForeignKey(sc => sc.PartidoId);
        //}
    }
}
