using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanTasks.Persistence.Migrations
{
    public partial class PermissionUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TodoAreaPermissions",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TodoAreaPermissions",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
