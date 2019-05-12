using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanTodoTasks.DataAccess.Migrations
{
    public partial class TodoReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CloseReason",
                table: "Todos",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CloseReason",
                table: "Todos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
