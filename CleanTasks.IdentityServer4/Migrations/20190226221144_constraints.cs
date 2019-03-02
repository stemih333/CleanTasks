using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanTasks.IdentityServer4.Migrations
{
    public partial class constraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AspNetUsers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
