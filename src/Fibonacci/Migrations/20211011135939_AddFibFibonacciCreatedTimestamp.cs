using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fibonacci.Migrations
{
    public partial class AddFibFibonacciCreatedTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FIB_CreatedTimestamp",
                schema: "sch_fib",
                table: "T_Fibonacci",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FIB_CreatedTimestamp",
                schema: "sch_fib",
                table: "T_Fibonacci");
        }
    }
}
