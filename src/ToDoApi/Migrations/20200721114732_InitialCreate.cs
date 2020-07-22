using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Summary = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "Id", "Description", "Summary" },
                values: new object[] { 1, "(Seeded Data)", "Example ToDo Item #1" });

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "Id", "Description", "Summary" },
                values: new object[] { 2, "(Seeded Data)", "Example ToDo Item #2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDoItems");
        }
    }
}