using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TasksApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AttachmentId",
                table: "Tasks",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Attachments_AttachmentId",
                table: "Tasks",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Attachments_AttachmentId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AttachmentId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Tasks");
        }
    }
}
