using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.Users.Migrations
{
    /// <inheritdoc />
    public partial class v5_fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "User",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_BranchId",
                table: "User",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Branches_BranchId",
                table: "User",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Branches_BranchId",
                table: "User");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_User_BranchId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "User");
        }
    }
}
