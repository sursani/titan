using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace data.Migrations
{
    public partial class EditUserImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "UserImage",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "UserImage",
                type: "timestamp",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "UserImage");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "UserImage");
        }
    }
}
