using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkainRetroMuseumWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addHardwareWithBranch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Hardwares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Hardwares_BranchId",
                table: "Hardwares",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hardwares_Branches_BranchId",
                table: "Hardwares",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hardwares_Branches_BranchId",
                table: "Hardwares");

            migrationBuilder.DropIndex(
                name: "IX_Hardwares_BranchId",
                table: "Hardwares");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Hardwares");
        }
    }
}
