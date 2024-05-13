using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EurofinsEvents.Migrations
{
    public partial class addvoting2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserEvent1");

            migrationBuilder.CreateTable(
                name: "EventVote",
                columns: table => new
                {
                    VotesEvent_ID = table.Column<int>(type: "int", nullable: false),
                    VotesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Event_ID = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventVote", x => new { x.VotesEvent_ID, x.VotesId });
                    table.ForeignKey(
                        name: "FK_EventVote_AspNetUsers_VotesId",
                        column: x => x.VotesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventVote_Event_VotesEvent_ID",
                        column: x => x.VotesEvent_ID,
                        principalTable: "Event",
                        principalColumn: "Event_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventVote_VotesId",
                table: "EventVote",
                column: "VotesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventVote");

            migrationBuilder.CreateTable(
                name: "ApplicationUserEvent1",
                columns: table => new
                {
                    VotesEvent_ID = table.Column<int>(type: "int", nullable: false),
                    VotesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserEvent1", x => new { x.VotesEvent_ID, x.VotesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserEvent1_AspNetUsers_VotesId",
                        column: x => x.VotesId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserEvent1_Event_VotesEvent_ID",
                        column: x => x.VotesEvent_ID,
                        principalTable: "Event",
                        principalColumn: "Event_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEvent1_VotesId",
                table: "ApplicationUserEvent1",
                column: "VotesId");
        }
    }
}
