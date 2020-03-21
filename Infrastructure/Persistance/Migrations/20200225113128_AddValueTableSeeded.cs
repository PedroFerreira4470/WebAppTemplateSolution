using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistance.Migrations
{
    public partial class AddValueTableSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    ValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValueNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.ValueId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Values");
        }
    }
}
