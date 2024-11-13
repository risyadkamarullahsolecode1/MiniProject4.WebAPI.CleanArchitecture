using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiniProject4.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    deptno = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    deptname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    mgrempno = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("departments_pkey", x => x.deptno);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    empno = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    lname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    dob = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    sex = table.Column<string>(type: "character varying", nullable: true),
                    position = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    deptno = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employees_pkey", x => x.empno);
                    table.ForeignKey(
                        name: "fk_deptno",
                        column: x => x.deptno,
                        principalTable: "departments",
                        principalColumn: "deptno");
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    projno = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    projname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    deptno = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("projects_pkey", x => x.projno);
                    table.ForeignKey(
                        name: "projects_deptno_fkey",
                        column: x => x.deptno,
                        principalTable: "departments",
                        principalColumn: "deptno");
                });

            migrationBuilder.CreateTable(
                name: "workson",
                columns: table => new
                {
                    empno = table.Column<int>(type: "integer", nullable: false),
                    projno = table.Column<int>(type: "integer", nullable: false),
                    dateworked = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    hoursworked = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("workson_pkey", x => new { x.empno, x.projno });
                    table.ForeignKey(
                        name: "workson_empno_fkey",
                        column: x => x.empno,
                        principalTable: "employees",
                        principalColumn: "empno");
                    table.ForeignKey(
                        name: "workson_projno_fkey",
                        column: x => x.projno,
                        principalTable: "projects",
                        principalColumn: "projno");
                });

            migrationBuilder.CreateIndex(
                name: "IX_departments_mgrempno",
                table: "departments",
                column: "mgrempno");

            migrationBuilder.CreateIndex(
                name: "IX_employees_deptno",
                table: "employees",
                column: "deptno");

            migrationBuilder.CreateIndex(
                name: "IX_projects_deptno",
                table: "projects",
                column: "deptno");

            migrationBuilder.CreateIndex(
                name: "IX_workson_projno",
                table: "workson",
                column: "projno");

            migrationBuilder.AddForeignKey(
                name: "departments_mgrempno_fkey",
                table: "departments",
                column: "mgrempno",
                principalTable: "employees",
                principalColumn: "empno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "departments_mgrempno_fkey",
                table: "departments");

            migrationBuilder.DropTable(
                name: "workson");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "departments");
        }
    }
}
