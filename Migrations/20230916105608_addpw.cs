using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addpw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "GiangViens",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "GiangViens");
        }
    }
}
