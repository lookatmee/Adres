using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adres.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentacionAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detalle",
                table: "Documentaciones");

            migrationBuilder.AlterColumn<string>(
                name: "TipoDocumento",
                table: "Documentaciones",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroDocumento",
                table: "Documentaciones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CreadoPor",
                table: "Documentaciones",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Documentaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDocumento",
                table: "Documentaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Documentaciones",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModificadoPor",
                table: "Documentaciones",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Documentaciones",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreadoPor",
                table: "Documentaciones");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Documentaciones");

            migrationBuilder.DropColumn(
                name: "FechaDocumento",
                table: "Documentaciones");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Documentaciones");

            migrationBuilder.DropColumn(
                name: "ModificadoPor",
                table: "Documentaciones");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Documentaciones");

            migrationBuilder.AlterColumn<string>(
                name: "TipoDocumento",
                table: "Documentaciones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroDocumento",
                table: "Documentaciones",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Detalle",
                table: "Documentaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
