using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoTasks.DataAccess.Migrations
{
    public partial class AttachmentName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Attachments",
                newName: "SavedFileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SavedFileName",
                table: "Attachments",
                newName: "FilePath");
        }
    }
}
