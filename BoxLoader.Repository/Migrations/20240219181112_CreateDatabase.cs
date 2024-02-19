using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoxLoader.Repository.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SupplierIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    PoNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BoxIdentifier = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.PoNumber);
                    table.ForeignKey(
                        name: "FK_Contents_Boxes_BoxIdentifier",
                        column: x => x.BoxIdentifier,
                        principalTable: "Boxes",
                        principalColumn: "Identifier");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contents_BoxIdentifier",
                table: "Contents",
                column: "BoxIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "Boxes");
        }
    }
}
