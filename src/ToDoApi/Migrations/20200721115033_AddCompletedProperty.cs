using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApi.Migrations
{
    public partial class AddCompletedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "ToDoItems",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "ToDoItems");
        }
    }
}