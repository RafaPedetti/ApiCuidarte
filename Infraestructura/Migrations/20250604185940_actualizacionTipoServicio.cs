using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class actualizacionTipoServicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_tipoServicios_TipoServicioId",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tipoServicios",
                table: "tipoServicios");

            migrationBuilder.RenameTable(
                name: "tipoServicios",
                newName: "TipoServicios");

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "TipoServicios",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoServicios",
                table: "TipoServicios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_TipoServicios_TipoServicioId",
                table: "Servicios",
                column: "TipoServicioId",
                principalTable: "TipoServicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_TipoServicios_TipoServicioId",
                table: "Servicios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoServicios",
                table: "TipoServicios");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "TipoServicios");

            migrationBuilder.RenameTable(
                name: "TipoServicios",
                newName: "tipoServicios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tipoServicios",
                table: "tipoServicios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_tipoServicios_TipoServicioId",
                table: "Servicios",
                column: "TipoServicioId",
                principalTable: "tipoServicios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
