using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpicorBTWRest.DataAccess.Migrations
{
    public partial class ActualizarCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "total",
                table: "EpicorUD05",
                newName: "Ensamble");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "EpicorLabor",
                newName: "Ensamble");

            migrationBuilder.AddColumn<int>(
                name: "Consecutivo",
                table: "EpicorUD05",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GrupoRecurso",
                table: "EpicorUD05",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Maquina",
                table: "EpicorUD05",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TiempoPerdido",
                table: "EpicorUD05",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoLabor",
                table: "EpicorUD05",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodigoEjecucion",
                table: "EpicorLabor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodigoParo",
                table: "EpicorLabor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Consecutivo",
                table: "EpicorLabor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GrupoRecurso",
                table: "EpicorLabor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Labor_QtyFinal",
                table: "EpicorLabor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Maquina",
                table: "EpicorLabor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TiempoPerdido",
                table: "EpicorLabor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consecutivo",
                table: "EpicorUD05");

            migrationBuilder.DropColumn(
                name: "GrupoRecurso",
                table: "EpicorUD05");

            migrationBuilder.DropColumn(
                name: "Maquina",
                table: "EpicorUD05");

            migrationBuilder.DropColumn(
                name: "TiempoPerdido",
                table: "EpicorUD05");

            migrationBuilder.DropColumn(
                name: "TipoLabor",
                table: "EpicorUD05");

            migrationBuilder.DropColumn(
                name: "CodigoEjecucion",
                table: "EpicorLabor");

            migrationBuilder.DropColumn(
                name: "CodigoParo",
                table: "EpicorLabor");

            migrationBuilder.DropColumn(
                name: "Consecutivo",
                table: "EpicorLabor");

            migrationBuilder.DropColumn(
                name: "GrupoRecurso",
                table: "EpicorLabor");

            migrationBuilder.DropColumn(
                name: "Labor_QtyFinal",
                table: "EpicorLabor");

            migrationBuilder.DropColumn(
                name: "Maquina",
                table: "EpicorLabor");

            migrationBuilder.DropColumn(
                name: "TiempoPerdido",
                table: "EpicorLabor");

            migrationBuilder.RenameColumn(
                name: "Ensamble",
                table: "EpicorUD05",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "Ensamble",
                table: "EpicorLabor",
                newName: "total");
        }
    }
}
