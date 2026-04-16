using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SP26ConcertMVCMartino.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "ArtistGenre",
                columns: table => new
                {
                    ArtistsArtistID = table.Column<int>(type: "int", nullable: false),
                    GenresGenreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistGenre", x => new { x.ArtistsArtistID, x.GenresGenreID });
                    table.ForeignKey(
                        name: "FK_ArtistGenre_Artists_ArtistsArtistID",
                        column: x => x.ArtistsArtistID,
                        principalTable: "Artists",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistGenre_Genres_GenresGenreID",
                        column: x => x.GenresGenreID,
                        principalTable: "Genres",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistGenre_GenresGenreID",
                table: "ArtistGenre",
                column: "GenresGenreID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistGenre");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
