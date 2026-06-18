using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLANETS_HOME.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    light = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    water = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    humidity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    caretips = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "id", "caretips", "category", "description", "humidity", "light", "name", "water" },
                values: new object[,]
                {
                    { 1, "Wipe leaves monthly. Avoid direct sunlight which burns leaves.", "Tropical", "A tropical plant loved for its large, glossy, split leaves. A statement piece for any room.", "High (60%+)", "Bright indirect", "Monstera", "Once a week" },
                    { 2, "Let soil dry completely between waterings. Overwatering is the main risk.", "Succulent", "One of the most tolerant houseplants. Perfect for beginners. Purifies indoor air.", "Low to medium", "Any light", "Snake Plant", "Every 2-3 weeks" },
                    { 3, "Drooping leaves signal it needs water. It recovers quickly after watering.", "Flowering", "A graceful flowering plant that thrives in low light. White blooms appear in spring.", "High", "Low to medium", "Peace Lily", "Once a week" },
                    { 4, "Use cactus soil. Never let water pool in the saucer.", "Succulent", "Drought-tolerant succulents that store water in their stems. Great for sunny windowsills.", "Low", "Full sun", "Cactus", "Every 2-4 weeks" },
                    { 5, "Can grow in water alone. Trim long vines to encourage bushiness.", "Tropical", "A fast-growing trailing vine with heart-shaped leaves. Almost impossible to kill.", "Medium", "Low to medium", "Pothos", "Every 1-2 weeks" },
                    { 6, "Soak roots 15 minutes then drain. Never leave in standing water.", "Flowering", "Elegant flowering plants with blooms that last for months. A popular gift plant.", "Medium to high", "Bright indirect", "Orchid", "Every 7-10 days" },
                    { 7, "Wipe leaves with a damp cloth. Repot every 2 years.", "Tropical", "A bold plant with large, glossy, dark-green leaves. Grows tall indoors.", "Medium", "Bright indirect", "Rubber Plant", "Every 1-2 weeks" },
                    { 8, "Use a terracotta pot with drainage holes. Direct sun is ideal.", "Succulent", "A practical succulent. The gel inside its leaves soothes burns and skin irritation.", "Low", "Full sun", "Aloe Vera", "Every 3 weeks" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created", "email", "password", "role", "username" },
                values: new object[] { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@planetshome.com", "admin123", "admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_username",
                table: "Users",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
