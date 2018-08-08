using Microsoft.EntityFrameworkCore.Migrations;

namespace DbEntities.Migrations
{
    public partial class PositionPropertyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EmployeePositions",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "EmployeePositions",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
