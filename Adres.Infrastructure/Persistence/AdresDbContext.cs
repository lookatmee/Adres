using Adres.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adres.Infrastructure.Persistence;

public class AdresDbContext : DbContext
{
    public AdresDbContext(DbContextOptions<AdresDbContext> options) : base(options)
    {
    }

    public DbSet<Adquisicion> Adquisiciones { get; set; }
    public DbSet<UnidadAdministrativa> UnidadesAdministrativas { get; set; }
    public DbSet<TipoBienServicio> TiposBienesServicios { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Documentacion> Documentaciones { get; set; }
    public DbSet<HistorialCambios> HistorialCambios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Adquisicion>(entity =>
        {
            entity.Property(e => e.Presupuesto)
                .HasPrecision(18, 2);

            entity.Property(e => e.ValorTotal)
                .HasPrecision(18, 2);

            entity.Property(e => e.ValorUnitario)
                .HasPrecision(18, 2);

            entity.Property(e => e.Estado)
                .HasDefaultValue("activo");
        });

        modelBuilder.Entity<Documentacion>(entity =>
        {
            entity.HasOne(d => d.Adquisicion)
                .WithMany(a => a.Documentaciones)
                .HasForeignKey(d => d.AdquisicionId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.TipoDocumento)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.NumeroDocumento)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Observaciones)
                .HasMaxLength(500);

            entity.Property(e => e.CreadoPor)
                .HasMaxLength(100);

            entity.Property(e => e.ModificadoPor)
                .HasMaxLength(100);
        });

        modelBuilder.Entity<HistorialCambios>(entity =>
        {
            entity.HasOne(h => h.Adquisicion)
                .WithMany(a => a.HistorialCambios)
                .HasForeignKey(h => h.AdquisicionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
} 