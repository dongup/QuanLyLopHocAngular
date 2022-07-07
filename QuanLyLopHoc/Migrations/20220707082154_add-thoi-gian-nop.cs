using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyLopHoc.Migrations
{
    public partial class addthoigiannop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ThoiGianNopBai",
                table: "SinhViens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThoiGianNopBai",
                table: "SinhViens");
        }
    }
}
