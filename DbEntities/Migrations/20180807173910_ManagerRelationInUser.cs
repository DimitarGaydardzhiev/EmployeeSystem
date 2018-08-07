using Microsoft.EntityFrameworkCore.Migrations;

namespace DbEntities.Migrations
{
    public partial class ManagerRelationInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "EmployeeUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeUsers_ManagerId",
                table: "EmployeeUsers",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeUsers_EmployeeUsers_ManagerId",
                table: "EmployeeUsers",
                column: "ManagerId",
                principalTable: "EmployeeUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeUsers_EmployeeUsers_ManagerId",
                table: "EmployeeUsers");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeUsers_ManagerId",
                table: "EmployeeUsers");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "EmployeeUsers");
        }
    }
}
