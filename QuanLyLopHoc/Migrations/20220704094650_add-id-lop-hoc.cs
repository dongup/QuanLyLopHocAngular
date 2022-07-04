using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyLopHoc.Migrations
{
    public partial class addidlophoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdLopHoc",
                table: "SinhVienTraLois",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SinhVienTraLois_IdBaiTap",
                table: "SinhVienTraLois",
                column: "IdBaiTap");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVienTraLois_IdSinhVien",
                table: "SinhVienTraLois",
                column: "IdSinhVien");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhVienTraLois_BaiTaps_IdBaiTap",
                table: "SinhVienTraLois",
                column: "IdBaiTap",
                principalTable: "BaiTaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SinhVienTraLois_SinhViens_IdSinhVien",
                table: "SinhVienTraLois",
                column: "IdSinhVien",
                principalTable: "SinhViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SinhVienTraLois_BaiTaps_IdBaiTap",
                table: "SinhVienTraLois");

            migrationBuilder.DropForeignKey(
                name: "FK_SinhVienTraLois_SinhViens_IdSinhVien",
                table: "SinhVienTraLois");

            migrationBuilder.DropIndex(
                name: "IX_SinhVienTraLois_IdBaiTap",
                table: "SinhVienTraLois");

            migrationBuilder.DropIndex(
                name: "IX_SinhVienTraLois_IdSinhVien",
                table: "SinhVienTraLois");

            migrationBuilder.DropColumn(
                name: "IdLopHoc",
                table: "SinhVienTraLois");
        }
    }
}
