using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dərslər",
                columns: table => new
                {
                    DersKodu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DersAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sinifi = table.Column<int>(type: "int", nullable: false),
                    MuellimAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MuellimSoyadi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dərslər", x => x.DersKodu);
                });

            migrationBuilder.CreateTable(
                name: "Şagirdlər",
                columns: table => new
                {
                    Nomresi = table.Column<int>(type: "int", nullable: false),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyadi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sinifi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Şagirdlər", x => x.Nomresi);
                });

            migrationBuilder.CreateTable(
                name: "İmtahanlar",
                columns: table => new
                {
                    ImtahanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersKodu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SagirdNomresi = table.Column<int>(type: "int", nullable: false),
                    ImtahanTarixi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Qiymeti = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_İmtahanlar", x => x.ImtahanID);
                    table.ForeignKey(
                        name: "FK_İmtahanlar_Dərslər_DersKodu",
                        column: x => x.DersKodu,
                        principalTable: "Dərslər",
                        principalColumn: "DersKodu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_İmtahanlar_Şagirdlər_SagirdNomresi",
                        column: x => x.SagirdNomresi,
                        principalTable: "Şagirdlər",
                        principalColumn: "Nomresi",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_İmtahanlar_DersKodu",
                table: "İmtahanlar",
                column: "DersKodu");

            migrationBuilder.CreateIndex(
                name: "IX_İmtahanlar_SagirdNomresi",
                table: "İmtahanlar",
                column: "SagirdNomresi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "İmtahanlar");

            migrationBuilder.DropTable(
                name: "Dərslər");

            migrationBuilder.DropTable(
                name: "Şagirdlər");
        }
    }
}
