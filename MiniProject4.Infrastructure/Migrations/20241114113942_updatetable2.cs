using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProject4.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatetable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dob",
                table: "employees",
                newName: "Dob");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Dob",
                table: "employees",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dob",
                table: "employees",
                newName: "dob");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dob",
                table: "employees",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
