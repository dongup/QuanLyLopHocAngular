using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyLopHoc.Migrations
{
    public partial class adddiemcong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiemCong",
                table: "SinhViens",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiemSo",
                table: "BaiTaps",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiemCong",
                table: "SinhViens");

            migrationBuilder.AlterColumn<int>(
                name: "DiemSo",
                table: "BaiTaps",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
