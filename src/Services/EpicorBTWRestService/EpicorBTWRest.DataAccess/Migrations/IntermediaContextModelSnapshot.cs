// <auto-generated />
using System;
using EpicorBTWRest.DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EpicorBTWRest.DataAccess.Migrations
{
    [DbContext(typeof(IntermediaContext))]
    partial class IntermediaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EpicorBTWRest.DataAccess.Models.TimeReport.ControlPerdidos", b =>
                {
                    b.Property<int>("ContolPerdidosid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContolPerdidosid"), 1L, 1);

                    b.Property<string>("Codi_Ejecu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Codi_Paro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Entrada")
                        .HasColumnType("datetime2");

                    b.Property<int>("Oper")
                        .HasColumnType("int");

                    b.Property<string>("QtlyFinal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("jobnum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("posicion")
                        .HasColumnType("int");

                    b.Property<int>("total")
                        .HasColumnType("int");

                    b.HasKey("ContolPerdidosid");

                    b.ToTable("ControlPerdidos");
                });

            modelBuilder.Entity("EpicorBTWRest.DataAccess.Models.TimeReport.Epicor_Labor", b =>
                {
                    b.Property<int>("epicorlaborid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("epicorlaborid"), 1L, 1);

                    b.Property<DateTime>("ClockInDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ClockOutTime")
                        .HasColumnType("decimal(18,2)")
                        .HasConversion<decimal>();

                    b.Property<decimal>("ClockinTime")
                        .HasColumnType("decimal(18,2)")
                        .HasConversion<decimal>();

                  b.Property<bool>("Closed")
                        .HasColumnType("bit");

                    b.Property<string>("CodigoEjecucion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodigoParo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Consecutivo")
                        .HasColumnType("int");

                    b.Property<string>("DspClockInTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DspClockOutTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ensamble")
                        .HasColumnType("int");

                    b.Property<string>("GrupoRecurso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LaborNote")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LaborType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Labor_QtyFinal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maquina")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnlyCreated")
                        .HasColumnType("bit");

                    b.Property<bool>("OnlyUpdate")
                        .HasColumnType("bit");

                    b.Property<int>("OprSeq")
                        .HasColumnType("int");

                    b.Property<int>("Posicion")
                        .HasColumnType("int");

                    b.Property<string>("ResourceID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sincronizado")
                        .HasColumnType("bit");

                    b.Property<string>("TiempoPerdido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("jobnum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("laborhrs")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("epicorlaborid");

                    b.ToTable("EpicorLabor");
                });

            modelBuilder.Entity("EpicorBTWRest.DataAccess.Models.TimeReport.Epicor_UD05", b =>
                {
                    b.Property<int>("epicorud05id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("epicorud05id"), 1L, 1);

                    b.Property<bool>("Closed")
                        .HasColumnType("bit");

                    b.Property<string>("Codi_Ejecu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Codi_Paro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Consecutivo")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ensamble")
                        .HasColumnType("int");

                    b.Property<DateTime>("Entrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Final")
                        .HasColumnType("datetime2");

                    b.Property<string>("GrupoRecurso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maquina")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnlyCreated")
                        .HasColumnType("bit");

                    b.Property<bool>("OnlyUpdate")
                        .HasColumnType("bit");

                    b.Property<int>("Oper")
                        .HasColumnType("int");

                    b.Property<int>("Posicion1")
                        .HasColumnType("int");

                    b.Property<string>("QtlyFinal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sincronizado")
                        .HasColumnType("bit");

                    b.Property<string>("TiempoPerdido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoLabor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("jobnum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("epicorud05id");

                    b.ToTable("EpicorUD05");
                });
#pragma warning restore 612, 618
        }
    }
}
