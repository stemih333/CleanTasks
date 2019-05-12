using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanTodoTasks.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoAreas",
                columns: table => new
                {
                    TodoAreaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoAreas", x => x.TodoAreaId);
                });

            migrationBuilder.CreateTable(
                name: "TodoAreaPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    UserId = table.Column<string>(maxLength: 25, nullable: false),
                    TodoAreaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoAreaPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoAreaPermissions_TodoAreas_TodoAreaId",
                        column: x => x.TodoAreaId,
                        principalTable: "TodoAreas",
                        principalColumn: "TodoAreaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    TodoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 10000, nullable: false),
                    AssignedTo = table.Column<string>(maxLength: 25, nullable: true),
                    AssignedToName = table.Column<string>(maxLength: 100, nullable: true),
                    Type = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    CloseReason = table.Column<string>(nullable: false),
                    LinkedTodoId = table.Column<int>(nullable: true),
                    Rowversion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TodoAreaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.TodoId);
                    table.ForeignKey(
                        name: "FK_Todos_Todos_LinkedTodoId",
                        column: x => x.LinkedTodoId,
                        principalTable: "Todos",
                        principalColumn: "TodoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Todos_TodoAreas_TodoAreaId",
                        column: x => x.TodoAreaId,
                        principalTable: "TodoAreas",
                        principalColumn: "TodoAreaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 300, nullable: false),
                    Size = table.Column<int>(nullable: false),
                    TodoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_Attachments_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "TodoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    Value = table.Column<string>(maxLength: 300, nullable: false),
                    TodoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "TodoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    Value = table.Column<string>(maxLength: 25, nullable: false),
                    TodoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tags_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "TodoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_TodoId",
                table: "Attachments",
                column: "TodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_Name_TodoId",
                table: "Attachments",
                columns: new[] { "Name", "TodoId" },
                unique: true,
                filter: "[TodoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TodoId",
                table: "Comments",
                column: "TodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Value",
                table: "Comments",
                column: "Value");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TodoId",
                table: "Tags",
                column: "TodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Value_TodoId",
                table: "Tags",
                columns: new[] { "Value", "TodoId" },
                unique: true,
                filter: "[TodoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TodoAreaPermissions_TodoAreaId",
                table: "TodoAreaPermissions",
                column: "TodoAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoAreas_Name",
                table: "TodoAreas",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_LinkedTodoId",
                table: "Todos",
                column: "LinkedTodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_TodoAreaId",
                table: "Todos",
                column: "TodoAreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "TodoAreaPermissions");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "TodoAreas");
        }
    }
}
