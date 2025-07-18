using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class cambioCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TiposPlanes_PlanId",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "PlanId",
                table: "Clientes",
                newName: "TipoPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Clientes_PlanId",
                table: "Clientes",
                newName: "IX_Clientes_TipoPlanId");

            migrationBuilder.AddColumn<bool>(
                name: "Eliminado",
                table: "Clientes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_TiposPlanes_TipoPlanId",
                table: "Clientes",
                column: "TipoPlanId",
                principalTable: "TiposPlanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_TiposPlanes_TipoPlanId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Eliminado",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "TipoPlanId",
                table: "Clientes",
                newName: "PlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Clientes_TipoPlanId",
                table: "Clientes",
                newName: "IX_Clientes_PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_TiposPlanes_PlanId",
                table: "Clientes",
                column: "PlanId",
                principalTable: "TiposPlanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
