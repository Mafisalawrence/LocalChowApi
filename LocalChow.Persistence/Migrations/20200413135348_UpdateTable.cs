using Microsoft.EntityFrameworkCore.Migrations;

namespace LocalChow.Persistence.Migrations
{
    public partial class UpdateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_User_UserID",
                table: "Store");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Store",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Longitude",
                table: "Store",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Latitude",
                table: "Store",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Store_User_UserID",
                table: "Store",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Store_User_UserID",
                table: "Store");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Store",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "Store",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AddForeignKey(
                name: "FK_Store_User_UserID",
                table: "Store",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
