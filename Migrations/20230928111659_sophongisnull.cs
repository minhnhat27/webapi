using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class sophongisnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichThucHanhs_Phongs_PhongSoPhong",
                table: "LichThucHanhs");

            migrationBuilder.AlterColumn<int>(
                name: "PhongSoPhong",
                table: "LichThucHanhs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LichThucHanhs_Phongs_PhongSoPhong",
                table: "LichThucHanhs",
                column: "PhongSoPhong",
                principalTable: "Phongs",
                principalColumn: "SoPhong");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichThucHanhs_Phongs_PhongSoPhong",
                table: "LichThucHanhs");

            migrationBuilder.AlterColumn<int>(
                name: "PhongSoPhong",
                table: "LichThucHanhs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LichThucHanhs_Phongs_PhongSoPhong",
                table: "LichThucHanhs",
                column: "PhongSoPhong",
                principalTable: "Phongs",
                principalColumn: "SoPhong",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
