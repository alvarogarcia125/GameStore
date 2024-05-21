using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameGenre_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenre_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamePlatform_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatform_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("0bfe4c2e-5e56-4b9f-a5ec-c9217f9fca41"), "Action", null },
                    { new Guid("2ab83c0d-1a9d-43d3-a960-99bc91c84d79"), "Races", null },
                    { new Guid("364c543d-60ad-446a-b091-c9050d8cc531"), "Off-road", new Guid("2ab83c0d-1a9d-43d3-a960-99bc91c84d79") },
                    { new Guid("5741f683-e872-4f6a-b353-3340cf50273d"), "Puzzle & Skill", null },
                    { new Guid("70a7985d-c5f7-422d-8e0a-60ccd5a2d5c5"), "Strategy", null },
                    { new Guid("73ec0b86-bd7e-4d19-b52d-f0a9ebe810d2"), "Rally", new Guid("2ab83c0d-1a9d-43d3-a960-99bc91c84d79") },
                    { new Guid("83fb2bcb-6f66-4f1a-ba38-2cf277bd8986"), "RPG", new Guid("70a7985d-c5f7-422d-8e0a-60ccd5a2d5c5") },
                    { new Guid("93e975b7-41ad-4aa4-a7f0-98939c47209d"), "FPS", new Guid("0bfe4c2e-5e56-4b9f-a5ec-c9217f9fca41") },
                    { new Guid("9b8b4c45-d2e2-4244-a0a2-915cd8e4c682"), "Sports", null },
                    { new Guid("a1e4a456-2a14-4cbf-9940-3a44992deea7"), "TPS", new Guid("0bfe4c2e-5e56-4b9f-a5ec-c9217f9fca41") },
                    { new Guid("a7df6eff-eb9b-4060-98ae-2ae992310636"), "TBS", new Guid("70a7985d-c5f7-422d-8e0a-60ccd5a2d5c5") },
                    { new Guid("b2118695-7fed-4f8d-96c6-16c43197893b"), "RTS", new Guid("70a7985d-c5f7-422d-8e0a-60ccd5a2d5c5") },
                    { new Guid("b2ecde0e-3a56-4aea-afdf-47655ac23f34"), "Arcade", new Guid("2ab83c0d-1a9d-43d3-a960-99bc91c84d79") },
                    { new Guid("b5f24fb1-960f-48fd-b4ba-03d82418b6b3"), "Adventure", null },
                    { new Guid("c35d7d00-a2fc-4c0f-baa5-2f005faeb214"), "Formula", new Guid("2ab83c0d-1a9d-43d3-a960-99bc91c84d79") }
                });

            migrationBuilder.InsertData(
                table: "Platform",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("2e0aea16-17c8-4e0e-bb91-c1cc503f4f5c"), "Console" },
                    { new Guid("319c88ae-6918-4b8a-a042-97477e099825"), "Browser" },
                    { new Guid("809f8c4c-7ff1-4999-8e5f-8741fccccf62"), "Desktop" },
                    { new Guid("de71aa97-e64e-4ec5-9bb4-d29cac55adf2"), "Mobile" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_Key",
                table: "Game",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GameId_GenreId",
                table: "GameGenre",
                columns: new[] { "GameId", "GenreId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenreId",
                table: "GameGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_GameId_PlatformId",
                table: "GamePlatform",
                columns: new[] { "GameId", "PlatformId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_PlatformId",
                table: "GamePlatform",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_Name",
                table: "Genre",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Platform_Type",
                table: "Platform",
                column: "Type",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "GamePlatform");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Platform");
        }
    }
}
