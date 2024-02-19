using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TitleUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Titles_Images_ImageId",
                table: "Titles");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Titles",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "RequiredSubscriptionId",
                table: "Titles",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrialSubscriptionUsed",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Titles_RequiredSubscriptionId",
                table: "Titles",
                column: "RequiredSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_Images_ImageId",
                table: "Titles",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_Subscriptions_RequiredSubscriptionId",
                table: "Titles",
                column: "RequiredSubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Titles_Images_ImageId",
                table: "Titles");

            migrationBuilder.DropForeignKey(
                name: "FK_Titles_Subscriptions_RequiredSubscriptionId",
                table: "Titles");

            migrationBuilder.DropIndex(
                name: "IX_Titles_RequiredSubscriptionId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "RequiredSubscriptionId",
                table: "Titles");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsTrialSubscriptionUsed",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "Titles",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Titles_Images_ImageId",
                table: "Titles",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
