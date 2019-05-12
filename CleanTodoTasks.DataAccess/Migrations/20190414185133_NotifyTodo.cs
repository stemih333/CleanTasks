using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanTodoTasks.DataAccess.Migrations
{
    public partial class NotifyTodo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedToName",
                table: "Todos");

            migrationBuilder.AddColumn<bool>(
                name: "Notify",
                table: "Todos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notify",
                table: "Todos");

            migrationBuilder.AddColumn<string>(
                name: "AssignedToName",
                table: "Todos",
                maxLength: 100,
                nullable: true);
        }
    }
}
