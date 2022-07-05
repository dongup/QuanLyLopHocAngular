using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyLopHoc.Migrations
{
    public partial class addcoldiem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LopHocSinhViens");

            migrationBuilder.AddColumn<int>(
                name: "Diem",
                table: "SinhVienTraLois",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TongDiem",
                table: "SinhViens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiemSo",
                table: "BaiTaps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_IdLopHoc",
                table: "SinhViens",
                column: "IdLopHoc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SinhViens_IdLopHoc",
                table: "SinhViens");

            migrationBuilder.DropColumn(
                name: "Diem",
                table: "SinhVienTraLois");

            migrationBuilder.DropColumn(
                name: "TongDiem",
                table: "SinhViens");

            migrationBuilder.DropColumn(
                name: "DiemSo",
                table: "BaiTaps");

            migrationBuilder.CreateTable(
                name: "LopHocSinhViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LopHocsId = table.Column<int>(type: "int", nullable: false),
                    SinhViensId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocSinhViens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LopHocSinhViens_LopHocs_LopHocsId",
                        column: x => x.LopHocsId,
                        principalTable: "LopHocs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHocSinhViens_SinhViens_SinhViensId",
                        column: x => x.SinhViensId,
                        principalTable: "SinhViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LopHocSinhViens_LopHocsId",
                table: "LopHocSinhViens",
                column: "LopHocsId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocSinhViens_SinhViensId",
                table: "LopHocSinhViens",
                column: "SinhViensId");
        }
    }
}
