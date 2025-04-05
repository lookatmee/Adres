using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adres.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposBienesServicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposBienesServicios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesAdministrativas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesAdministrativas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adquisiciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Presupuesto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UnidadAdministrativaId = table.Column<int>(type: "int", nullable: false),
                    TipoBienServicioId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FechaAdquisicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "activo")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adquisiciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adquisiciones_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adquisiciones_TiposBienesServicios_TipoBienServicioId",
                        column: x => x.TipoBienServicioId,
                        principalTable: "TiposBienesServicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adquisiciones_UnidadesAdministrativas_UnidadAdministrativaId",
                        column: x => x.UnidadAdministrativaId,
                        principalTable: "UnidadesAdministrativas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documentaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdquisicionId = table.Column<int>(type: "int", nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detalle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentaciones_Adquisiciones_AdquisicionId",
                        column: x => x.AdquisicionId,
                        principalTable: "Adquisiciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorialCambios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdquisicionId = table.Column<int>(type: "int", nullable: false),
                    FechaCambio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CampoModificado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorAnterior = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorNuevo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialCambios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistorialCambios_Adquisiciones_AdquisicionId",
                        column: x => x.AdquisicionId,
                        principalTable: "Adquisiciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adquisiciones_ProveedorId",
                table: "Adquisiciones",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Adquisiciones_TipoBienServicioId",
                table: "Adquisiciones",
                column: "TipoBienServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Adquisiciones_UnidadAdministrativaId",
                table: "Adquisiciones",
                column: "UnidadAdministrativaId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentaciones_AdquisicionId",
                table: "Documentaciones",
                column: "AdquisicionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialCambios_AdquisicionId",
                table: "HistorialCambios",
                column: "AdquisicionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentaciones");

            migrationBuilder.DropTable(
                name: "HistorialCambios");

            migrationBuilder.DropTable(
                name: "Adquisiciones");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "TiposBienesServicios");

            migrationBuilder.DropTable(
                name: "UnidadesAdministrativas");
        }
    }
}
