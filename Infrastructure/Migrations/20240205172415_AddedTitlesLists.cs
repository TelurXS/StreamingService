using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTitlesLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TitlesList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Availability = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitlesList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitlesList_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitleUser",
                columns: table => new
                {
                    FavouriteInUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FavouriteTitlesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleUser", x => new { x.FavouriteInUsersId, x.FavouriteTitlesId });
                    table.ForeignKey(
                        name: "FK_TitleUser_AspNetUsers_FavouriteInUsersId",
                        column: x => x.FavouriteInUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TitleUser_Titles_FavouriteTitlesId",
                        column: x => x.FavouriteTitlesId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    FollowersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.FollowersId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserUser_AspNetUsers_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ViewRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Progress = table.Column<float>(type: "real", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewRecord_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ViewRecord_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ViewRecord_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TitleTitlesList",
                columns: table => new
                {
                    ListsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitlesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleTitlesList", x => new { x.ListsId, x.TitlesId });
                    table.ForeignKey(
                        name: "FK_TitleTitlesList_TitlesList_ListsId",
                        column: x => x.ListsId,
                        principalTable: "TitlesList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TitleTitlesList_Titles_TitlesId",
                        column: x => x.TitlesId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TitlesList_AuthorId",
                table: "TitlesList",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleTitlesList_TitlesId",
                table: "TitleTitlesList",
                column: "TitlesId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleUser_FavouriteTitlesId",
                table: "TitleUser",
                column: "FavouriteTitlesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_UserId",
                table: "UserUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewRecord_AuthorId",
                table: "ViewRecord",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewRecord_SeriesId",
                table: "ViewRecord",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewRecord_TitleId",
                table: "ViewRecord",
                column: "TitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TitleTitlesList");

            migrationBuilder.DropTable(
                name: "TitleUser");

            migrationBuilder.DropTable(
                name: "UserUser");

            migrationBuilder.DropTable(
                name: "ViewRecord");

            migrationBuilder.DropTable(
                name: "TitlesList");
        }
    }
}
