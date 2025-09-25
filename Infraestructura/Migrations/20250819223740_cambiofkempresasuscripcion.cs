using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class cambiofkempresasuscripcion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Suscripciones_SuscripcionId",
                table: "Empresas");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Suscripciones_SuscripcionId",
                table: "Empresas",
                column: "SuscripcionId",
                principalTable: "Suscripciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Suscripciones_SuscripcionId",
                table: "Empresas");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Suscripciones_SuscripcionId",
                table: "Empresas",
                column: "SuscripcionId",
                principalTable: "Suscripciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
