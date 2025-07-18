using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class updateTipoPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_TipoServicios_TipoServicioId",
                table: "Servicios");

            migrationBuilder.RenameColumn(
                name: "TipoServicioId",
                table: "Servicios",
                newName: "tipoServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_Servicios_TipoServicioId",
                table: "Servicios",
                newName: "IX_Servicios_tipoServicioId");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Tareas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Clientes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_PlanId",
                table: "Clientes",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_TiposPlanes_PlanId",
                table: "Clientes",
                column: "PlanId",
                principalTable: "TiposPlanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_TipoServicios_tipoServicioId",
                table: "Servicios",
                column: "tipoServicioId",
                principalTable: "TipoServicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TiposPlanes_PlanId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_TipoServicios_tipoServicioId",
                table: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_PlanId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "tipoServicioId",
                table: "Servicios",
                newName: "TipoServicioId");

            migrationBuilder.RenameIndex(
                name: "IX_Servicios_tipoServicioId",
                table: "Servicios",
                newName: "IX_Servicios_TipoServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_TipoServicios_TipoServicioId",
                table: "Servicios",
                column: "TipoServicioId",
                principalTable: "TipoServicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
