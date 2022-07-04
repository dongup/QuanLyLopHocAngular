using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyLopHoc.Migrations
{
    public partial class addcreateddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SinhVienTraLois",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SinhViens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "LopHocSinhViens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "LopHocs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BaiTaps",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_BaiTaps_IdLopHoc",
                table: "BaiTaps",
                column: "IdLopHoc");

            migrationBuilder.AddForeignKey(
                name: "FK_BaiTaps_LopHocs_IdLopHoc",
                table: "BaiTaps",
                column: "IdLopHoc",
                principalTable: "LopHocs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaiTaps_LopHocs_IdLopHoc",
                table: "BaiTaps");

            migrationBuilder.DropIndex(
                name: "IX_BaiTaps_IdLopHoc",
                table: "BaiTaps");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SinhVienTraLois");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SinhViens");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "LopHocSinhViens");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "LopHocs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BaiTaps");
        }
    }
}
