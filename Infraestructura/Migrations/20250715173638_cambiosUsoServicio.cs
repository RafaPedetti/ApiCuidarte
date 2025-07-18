using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class cambiosUsoServicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioHora",
                table: "TipoServicios",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Servicios",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TareaId",
                table: "Servicios",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TareaId1",
                table: "Servicios",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_ClienteId",
                table: "Servicios",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_TareaId",
                table: "Servicios",
                column: "TareaId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_TareaId1",
                table: "Servicios",
                column: "TareaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Clientes_ClienteId",
                table: "Servicios",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Tareas_TareaId",
                table: "Servicios",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Tareas_TareaId1",
                table: "Servicios",
                column: "TareaId1",
                principalTable: "Tareas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Clientes_ClienteId",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Tareas_TareaId",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Tareas_TareaId1",
                table: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_ClienteId",
                table: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_TareaId",
                table: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_TareaId1",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "TareaId",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "TareaId1",
                table: "Servicios");

            migrationBuilder.AlterColumn<float>(
                name: "PrecioHora",
                table: "TipoServicios",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
