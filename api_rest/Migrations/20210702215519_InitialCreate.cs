using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace api_rest.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Username = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    TypeUser = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    QuantityInPackge = table.Column<short>(type: "smallint", nullable: false),
                    UnitOfMeasurement = table.Column<byte>(type: "smallint", nullable: false),
                    IdCategory = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_categories_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 100, "Fruits and Vegetables" },
                    { 101, "Dairy" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Name", "Password", "TypeUser", "Username" },
                values: new object[] { 1, "Administrador", "Admim", 1, "Admim" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "IdCategory", "Name", "QuantityInPackge", "UnitOfMeasurement" },
                values: new object[] { 100, 100, "Banana", (short)10, (byte)4 });

            migrationBuilder.CreateIndex(
                name: "IX_products_IdCategory",
                table: "products",
                column: "IdCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
