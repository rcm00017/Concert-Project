using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SP26ConcertMVCMartino.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeatPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatID",
                table: "Tickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SeatPrices",
                columns: table => new
                {
                    SeatPriceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: false),
                    SeatID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatPrices", x => x.SeatPriceID);
                    table.ForeignKey(
                        name: "FK_SeatPrices_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatPrices_Seats_SeatID",
                        column: x => x.SeatID,
                        principalTable: "Seats",
                        principalColumn: "SeatID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatID",
                table: "Tickets",
                column: "SeatID");

            migrationBuilder.CreateIndex(
                name: "IX_SeatPrices_EventID",
                table: "SeatPrices",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_SeatPrices_SeatID",
                table: "SeatPrices",
                column: "SeatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Seats_SeatID",
                table: "Tickets",
                column: "SeatID",
                principalTable: "Seats",
                principalColumn: "SeatID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Seats_SeatID",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "SeatPrices");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_SeatID",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "SeatID",
                table: "Tickets");
        }
    }
}
