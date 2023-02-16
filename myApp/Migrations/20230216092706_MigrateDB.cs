using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myApp.Migrations
{
    /// <inheritdoc />
    public partial class MigrateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "user_seq");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "user_pk",
                table: "User",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "user_pk",
                table: "User");

            migrationBuilder.DropColumn(
                name: "id",
                table: "User");

            migrationBuilder.DropSequence(
                name: "user_seq");
        }
    }
}
