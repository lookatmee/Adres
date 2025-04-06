﻿// <auto-generated />
using System;
using Adres.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Adres.Infrastructure.Migrations
{
    [DbContext(typeof(AdresDbContext))]
    [Migration("20250406022639_AddDocumentacionAuditFields")]
    partial class AddDocumentacionAuditFields
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Adres.Domain.Entities.Adquisicion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("activo");

                    b.Property<DateTime>("FechaAdquisicion")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Presupuesto")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<int>("TipoBienServicioId")
                        .HasColumnType("int");

                    b.Property<int>("UnidadAdministrativaId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ValorUnitario")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.HasIndex("TipoBienServicioId");

                    b.HasIndex("UnidadAdministrativaId");

                    b.ToTable("Adquisiciones");
                });

            modelBuilder.Entity("Adres.Domain.Entities.Documentacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdquisicionId")
                        .HasColumnType("int");

                    b.Property<string>("CreadoPor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaDocumento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModificadoPor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NumeroDocumento")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("TipoDocumento")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AdquisicionId");

                    b.ToTable("Documentaciones");
                });

            modelBuilder.Entity("Adres.Domain.Entities.HistorialCambios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdquisicionId")
                        .HasColumnType("int");

                    b.Property<string>("CampoModificado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCambio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValorAnterior")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValorNuevo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdquisicionId");

                    b.ToTable("HistorialCambios");
                });

            modelBuilder.Entity("Adres.Domain.Entities.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Adres.Domain.Entities.TipoBienServicio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TiposBienesServicios");
                });

            modelBuilder.Entity("Adres.Domain.Entities.UnidadAdministrativa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UnidadesAdministrativas");
                });

            modelBuilder.Entity("Adres.Domain.Entities.Adquisicion", b =>
                {
                    b.HasOne("Adres.Domain.Entities.Proveedor", "Proveedor")
                        .WithMany("Adquisiciones")
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Adres.Domain.Entities.TipoBienServicio", "TipoBienServicio")
                        .WithMany("Adquisiciones")
                        .HasForeignKey("TipoBienServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Adres.Domain.Entities.UnidadAdministrativa", "UnidadAdministrativa")
                        .WithMany("Adquisiciones")
                        .HasForeignKey("UnidadAdministrativaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");

                    b.Navigation("TipoBienServicio");

                    b.Navigation("UnidadAdministrativa");
                });

            modelBuilder.Entity("Adres.Domain.Entities.Documentacion", b =>
                {
                    b.HasOne("Adres.Domain.Entities.Adquisicion", "Adquisicion")
                        .WithMany("Documentaciones")
                        .HasForeignKey("AdquisicionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adquisicion");
                });

            modelBuilder.Entity("Adres.Domain.Entities.HistorialCambios", b =>
                {
                    b.HasOne("Adres.Domain.Entities.Adquisicion", "Adquisicion")
                        .WithMany("HistorialCambios")
                        .HasForeignKey("AdquisicionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adquisicion");
                });

            modelBuilder.Entity("Adres.Domain.Entities.Adquisicion", b =>
                {
                    b.Navigation("Documentaciones");

                    b.Navigation("HistorialCambios");
                });

            modelBuilder.Entity("Adres.Domain.Entities.Proveedor", b =>
                {
                    b.Navigation("Adquisiciones");
                });

            modelBuilder.Entity("Adres.Domain.Entities.TipoBienServicio", b =>
                {
                    b.Navigation("Adquisiciones");
                });

            modelBuilder.Entity("Adres.Domain.Entities.UnidadAdministrativa", b =>
                {
                    b.Navigation("Adquisiciones");
                });
#pragma warning restore 612, 618
        }
    }
}
