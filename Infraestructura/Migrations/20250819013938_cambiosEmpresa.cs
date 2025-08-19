using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class cambiosEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suscripciones_Empresas_EmpresaId",
                table: "Suscripciones");

            migrationBuilder.DropForeignKey(
                name: "FK_TiposPlanes_Empresas_EmpresaId",
                table: "TiposPlanes");

            migrationBuilder.DropIndex(
                name: "IX_TiposPlanes_EmpresaId",
                table: "TiposPlanes");

            migrationBuilder.DropIndex(
                name: "IX_Suscripciones_EmpresaId",
                table: "Suscripciones");

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId1",
                table: "Suscripciones",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Empresas",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Empresas",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<int>(
                name: "SuscripcionId",
                table: "Empresas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TelefonoContacto",
                table: "Empresas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TipoPlanId",
                table: "Empresas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_EmpresaId1",
                table: "Suscripciones",
                column: "EmpresaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_SuscripcionId",
                table: "Empresas",
                column: "SuscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_TipoPlanId",
                table: "Empresas",
                column: "TipoPlanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_Suscripciones_SuscripcionId",
                table: "Empresas",
                column: "SuscripcionId",
                principalTable: "Suscripciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Empresas_TiposPlanes_TipoPlanId",
                table: "Empresas",
                column: "TipoPlanId",
                principalTable: "TiposPlanes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suscripciones_Empresas_EmpresaId1",
                table: "Suscripciones",
                column: "EmpresaId1",
                principalTable: "Empresas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_Suscripciones_SuscripcionId",
                table: "Empresas");

            migrationBuilder.DropForeignKey(
                name: "FK_Empresas_TiposPlanes_TipoPlanId",
                table: "Empresas");

            migrationBuilder.DropForeignKey(
                name: "FK_Suscripciones_Empresas_EmpresaId1",
                table: "Suscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Suscripciones_EmpresaId1",
                table: "Suscripciones");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_SuscripcionId",
                table: "Empresas");

            migrationBuilder.DropIndex(
                name: "IX_Empresas_TipoPlanId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "EmpresaId1",
                table: "Suscripciones");

            migrationBuilder.DropColumn(
                name: "SuscripcionId",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "TelefonoContacto",
                table: "Empresas");

            migrationBuilder.DropColumn(
                name: "TipoPlanId",
                table: "Empresas");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Empresas",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "Eliminado",
                table: "Empresas",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TiposPlanes_EmpresaId",
                table: "TiposPlanes",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Suscripciones_EmpresaId",
                table: "Suscripciones",
                column: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suscripciones_Empresas_EmpresaId",
                table: "Suscripciones",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TiposPlanes_Empresas_EmpresaId",
                table: "TiposPlanes",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id");
        }
    }
}
