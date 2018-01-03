using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace transport.Migrations
{
    public partial class pracownikStanowisko : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stanowisko",
                table: "Pracownicy",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stanowisko",
                table: "Pracownicy");
        }
    }
}
