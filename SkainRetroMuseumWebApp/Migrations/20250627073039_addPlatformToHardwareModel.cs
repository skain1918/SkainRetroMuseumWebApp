using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkainRetroMuseumWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addPlatformToHardwareModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Hardwares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Hardwares_PlatformId",
                table: "Hardwares",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hardwares_Platforms_PlatformId",
                table: "Hardwares",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hardwares_Platforms_PlatformId",
                table: "Hardwares");

            migrationBuilder.DropIndex(
                name: "IX_Hardwares_PlatformId",
                table: "Hardwares");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Hardwares");
        }
    }
}
