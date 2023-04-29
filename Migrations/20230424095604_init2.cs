using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ManageUserId",
                table: "Attendances",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ManageUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Mobile = table.Column<int>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    BrithDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NameFatherChurch = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false),
                    CountChildren = table.Column<int>(type: "INTEGER", nullable: true),
                    NameJop = table.Column<string>(type: "TEXT", nullable: true),
                    IdFaceBook = table.Column<string>(type: "TEXT", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManageUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ManageUserId",
                table: "Attendances",
                column: "ManageUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_ManageUsers_ManageUserId",
                table: "Attendances",
                column: "ManageUserId",
                principalTable: "ManageUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_ManageUsers_ManageUserId",
                table: "Attendances");

            migrationBuilder.DropTable(
                name: "ManageUsers");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_ManageUserId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "ManageUserId",
                table: "Attendances");
        }
    }
}
