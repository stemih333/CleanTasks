using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanTasks.Persistence.Migrations
{
    public partial class UniqueTodo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_TodoAreas_TodoAreaId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_Title_TodoAreaId",
                table: "Todos");

            migrationBuilder.AlterColumn<int>(
                name: "TodoAreaId",
                table: "Todos",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Title_TodoAreaId",
                table: "Todos",
                columns: new[] { "Title", "TodoAreaId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_TodoAreas_TodoAreaId",
                table: "Todos",
                column: "TodoAreaId",
                principalTable: "TodoAreas",
                principalColumn: "TodoAreaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_TodoAreas_TodoAreaId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_Title_TodoAreaId",
                table: "Todos");

            migrationBuilder.AlterColumn<int>(
                name: "TodoAreaId",
                table: "Todos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Title_TodoAreaId",
                table: "Todos",
                columns: new[] { "Title", "TodoAreaId" },
                unique: true,
                filter: "[TodoAreaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_TodoAreas_TodoAreaId",
                table: "Todos",
                column: "TodoAreaId",
                principalTable: "TodoAreas",
                principalColumn: "TodoAreaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
