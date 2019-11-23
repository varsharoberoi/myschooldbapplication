using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace myschooldbapplication.Migrations
{
    public partial class studentimg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "student",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "student",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "student",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "student",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "student",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "student");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "student");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "student");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "student");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "student");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "student");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "student");
        }
    }
}
