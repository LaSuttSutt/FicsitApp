using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Migrations
{
    /// <inheritdoc />
    public partial class Factories_Productions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemProductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FactoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RecipeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MachineCount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Workload = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemProductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceProductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FactoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Purity = table.Column<int>(type: "INTEGER", nullable: false),
                    Extractor = table.Column<int>(type: "INTEGER", nullable: false),
                    ExtractorCount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Workload = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceProductions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factories");

            migrationBuilder.DropTable(
                name: "ItemProductions");

            migrationBuilder.DropTable(
                name: "ResourceProductions");
        }
    }
}
