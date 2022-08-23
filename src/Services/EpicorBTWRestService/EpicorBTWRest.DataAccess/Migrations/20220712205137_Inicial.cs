using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpicorBTWRest.DataAccess.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControlPerdidos",
                columns: table => new
                {
                    ContolPerdidosid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<int>(type: "int", nullable: false),
                    jobnum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Oper = table.Column<int>(type: "int", nullable: false),
                    Codi_Ejecu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codi_Paro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtlyFinal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    posicion = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlPerdidos", x => x.ContolPerdidosid);
                });

            migrationBuilder.CreateTable(
                name: "EpicorLabor",
                columns: table => new
                {
                    epicorlaborid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<int>(type: "int", nullable: false),
                    jobnum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaborType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    laborhrs = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResourceID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClockOutTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClockinTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DspClockInTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DspClockOutTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OprSeq = table.Column<int>(type: "int", nullable: false),
                    ClockInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LaborNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sincronizado = table.Column<bool>(type: "bit", nullable: false),
                    OnlyCreated = table.Column<bool>(type: "bit", nullable: false),
                    OnlyUpdate = table.Column<bool>(type: "bit", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false),
                    Posicion = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpicorLabor", x => x.epicorlaborid);
                });

            migrationBuilder.CreateTable(
                name: "EpicorUD05",
                columns: table => new
                {
                    epicorud05id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<int>(type: "int", nullable: false),
                    jobnum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResourceID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Final = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Oper = table.Column<int>(type: "int", nullable: false),
                    Codi_Ejecu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codi_Paro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtlyFinal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sincronizado = table.Column<bool>(type: "bit", nullable: false),
                    Posicion1 = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<int>(type: "int", nullable: false),
                    OnlyCreated = table.Column<bool>(type: "bit", nullable: false),
                    OnlyUpdate = table.Column<bool>(type: "bit", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpicorUD05", x => x.epicorud05id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlPerdidos");

            migrationBuilder.DropTable(
                name: "EpicorLabor");

            migrationBuilder.DropTable(
                name: "EpicorUD05");
        }
    }
}
