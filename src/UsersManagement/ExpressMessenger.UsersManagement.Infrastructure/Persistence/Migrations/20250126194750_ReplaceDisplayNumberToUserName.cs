using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressMessenger.UsersManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceDisplayNumberToUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "display_number",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "user_name",
                table: "users",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_name",
                table: "users");

            migrationBuilder.AddColumn<long>(
                name: "display_number",
                table: "users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
