using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class cambiosUsoServicio2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteId1",
                table: "Servicios",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_ClienteId1",
                table: "Servicios",
                column: "ClienteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Clientes_ClienteId1",
                table: "Servicios",
                column: "ClienteId1",
                principalTable: "Clientes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Clientes_ClienteId1",
                table: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_ClienteId1",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "Servicios");
        }
    }
}
