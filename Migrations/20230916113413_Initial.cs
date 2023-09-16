using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buois",
                columns: table => new
                {
                    TenBuoi = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buois", x => x.TenBuoi);
                });

            migrationBuilder.CreateTable(
                name: "BuoiThucHanhs",
                columns: table => new
                {
                    STT = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuoiThucHanhs", x => x.STT);
                });

            migrationBuilder.CreateTable(
                name: "CPUs",
                columns: table => new
                {
                    Ten = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUs", x => x.Ten);
                });

            migrationBuilder.CreateTable(
                name: "GiangViens",
                columns: table => new
                {
                    MSCB = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangViens", x => x.MSCB);
                });

            migrationBuilder.CreateTable(
                name: "HocKyNamHocs",
                columns: table => new
                {
                    HK_NH = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NgayBatDau = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HocKyHienTai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocKyNamHocs", x => x.HK_NH);
                });

            migrationBuilder.CreateTable(
                name: "HocPhans",
                columns: table => new
                {
                    MaHP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenHocPhan = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: false),
                    SoTietThucHanh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhans", x => x.MaHP);
                });

            migrationBuilder.CreateTable(
                name: "OCungs",
                columns: table => new
                {
                    DungLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCungs", x => x.DungLuong);
                });

            migrationBuilder.CreateTable(
                name: "PhanMems",
                columns: table => new
                {
                    IdPhanMem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhanMem = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanMems", x => x.IdPhanMem);
                });

            migrationBuilder.CreateTable(
                name: "RAMs",
                columns: table => new
                {
                    DungLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMs", x => x.DungLuong);
                });

            migrationBuilder.CreateTable(
                name: "Tuans",
                columns: table => new
                {
                    SoTuan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tuans", x => x.SoTuan);
                });

            migrationBuilder.CreateTable(
                name: "NhomHocPhans",
                columns: table => new
                {
                    MaNhomHP = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SoLuongSV = table.Column<int>(type: "int", nullable: false),
                    HocPhanMaHP = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomHocPhans", x => x.MaNhomHP);
                    table.ForeignKey(
                        name: "FK_NhomHocPhans_HocPhans_HocPhanMaHP",
                        column: x => x.HocPhanMaHP,
                        principalTable: "HocPhans",
                        principalColumn: "MaHP");
                });

            migrationBuilder.CreateTable(
                name: "CauHinhs",
                columns: table => new
                {
                    IdCauHinh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPUTen = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    RAMDungLuong = table.Column<int>(type: "int", nullable: true),
                    OCungDungLuong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHinhs", x => x.IdCauHinh);
                    table.ForeignKey(
                        name: "FK_CauHinhs_CPUs_CPUTen",
                        column: x => x.CPUTen,
                        principalTable: "CPUs",
                        principalColumn: "Ten");
                    table.ForeignKey(
                        name: "FK_CauHinhs_OCungs_OCungDungLuong",
                        column: x => x.OCungDungLuong,
                        principalTable: "OCungs",
                        principalColumn: "DungLuong");
                    table.ForeignKey(
                        name: "FK_CauHinhs_RAMs_RAMDungLuong",
                        column: x => x.RAMDungLuong,
                        principalTable: "RAMs",
                        principalColumn: "DungLuong");
                });

            migrationBuilder.CreateTable(
                name: "GiangDays",
                columns: table => new
                {
                    HK_NH = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    MSCB = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    STT = table.Column<int>(type: "int", nullable: false),
                    MaNhomHP = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangDays", x => new { x.HK_NH, x.MSCB, x.STT, x.MaNhomHP });
                    table.ForeignKey(
                        name: "FK_GiangDays_BuoiThucHanhs_STT",
                        column: x => x.STT,
                        principalTable: "BuoiThucHanhs",
                        principalColumn: "STT",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiangDays_GiangViens_MSCB",
                        column: x => x.MSCB,
                        principalTable: "GiangViens",
                        principalColumn: "MSCB",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiangDays_HocKyNamHocs_HK_NH",
                        column: x => x.HK_NH,
                        principalTable: "HocKyNamHocs",
                        principalColumn: "HK_NH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiangDays_NhomHocPhans_MaNhomHP",
                        column: x => x.MaNhomHP,
                        principalTable: "NhomHocPhans",
                        principalColumn: "MaNhomHP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaiDatPhanMems",
                columns: table => new
                {
                    IdCauHinh = table.Column<int>(type: "int", nullable: false),
                    IdPhanMem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaiDatPhanMems", x => new { x.IdCauHinh, x.IdPhanMem });
                    table.ForeignKey(
                        name: "FK_CaiDatPhanMems_CauHinhs_IdCauHinh",
                        column: x => x.IdCauHinh,
                        principalTable: "CauHinhs",
                        principalColumn: "IdCauHinh",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaiDatPhanMems_PhanMems_IdPhanMem",
                        column: x => x.IdPhanMem,
                        principalTable: "PhanMems",
                        principalColumn: "IdPhanMem",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phongs",
                columns: table => new
                {
                    SoPhong = table.Column<int>(type: "int", nullable: false),
                    SoLuongMayTinh = table.Column<int>(type: "int", nullable: false),
                    CauHinhIdCauHinh = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phongs", x => x.SoPhong);
                    table.ForeignKey(
                        name: "FK_Phongs_CauHinhs_CauHinhIdCauHinh",
                        column: x => x.CauHinhIdCauHinh,
                        principalTable: "CauHinhs",
                        principalColumn: "IdCauHinh");
                });

            migrationBuilder.CreateTable(
                name: "HocPhanPhuHops",
                columns: table => new
                {
                    SoPhong = table.Column<int>(type: "int", nullable: false),
                    MaHP = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhanPhuHops", x => new { x.SoPhong, x.MaHP });
                    table.ForeignKey(
                        name: "FK_HocPhanPhuHops_HocPhans_MaHP",
                        column: x => x.MaHP,
                        principalTable: "HocPhans",
                        principalColumn: "MaHP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HocPhanPhuHops_Phongs_SoPhong",
                        column: x => x.SoPhong,
                        principalTable: "Phongs",
                        principalColumn: "SoPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichThucHanhs",
                columns: table => new
                {
                    NgayThucHanh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SoPhong = table.Column<int>(type: "int", nullable: false),
                    TenBuoi = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    TuanSoTuan = table.Column<int>(type: "int", nullable: true),
                    GiangDayHK_NH = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    GiangDayMSCB = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    GiangDaySTT = table.Column<int>(type: "int", nullable: true),
                    GiangDayMaNhomHP = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichThucHanhs", x => new { x.NgayThucHanh, x.SoPhong, x.TenBuoi });
                    table.ForeignKey(
                        name: "FK_LichThucHanhs_Buois_TenBuoi",
                        column: x => x.TenBuoi,
                        principalTable: "Buois",
                        principalColumn: "TenBuoi",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichThucHanhs_GiangDays_GiangDayHK_NH_GiangDayMSCB_GiangDaySTT_GiangDayMaNhomHP",
                        columns: x => new { x.GiangDayHK_NH, x.GiangDayMSCB, x.GiangDaySTT, x.GiangDayMaNhomHP },
                        principalTable: "GiangDays",
                        principalColumns: new[] { "HK_NH", "MSCB", "STT", "MaNhomHP" });
                    table.ForeignKey(
                        name: "FK_LichThucHanhs_Phongs_SoPhong",
                        column: x => x.SoPhong,
                        principalTable: "Phongs",
                        principalColumn: "SoPhong",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichThucHanhs_Tuans_TuanSoTuan",
                        column: x => x.TuanSoTuan,
                        principalTable: "Tuans",
                        principalColumn: "SoTuan");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaiDatPhanMems_IdPhanMem",
                table: "CaiDatPhanMems",
                column: "IdPhanMem");

            migrationBuilder.CreateIndex(
                name: "IX_CauHinhs_CPUTen",
                table: "CauHinhs",
                column: "CPUTen");

            migrationBuilder.CreateIndex(
                name: "IX_CauHinhs_OCungDungLuong",
                table: "CauHinhs",
                column: "OCungDungLuong");

            migrationBuilder.CreateIndex(
                name: "IX_CauHinhs_RAMDungLuong",
                table: "CauHinhs",
                column: "RAMDungLuong");

            migrationBuilder.CreateIndex(
                name: "IX_GiangDays_MaNhomHP",
                table: "GiangDays",
                column: "MaNhomHP");

            migrationBuilder.CreateIndex(
                name: "IX_GiangDays_MSCB",
                table: "GiangDays",
                column: "MSCB");

            migrationBuilder.CreateIndex(
                name: "IX_GiangDays_STT",
                table: "GiangDays",
                column: "STT");

            migrationBuilder.CreateIndex(
                name: "IX_HocPhanPhuHops_MaHP",
                table: "HocPhanPhuHops",
                column: "MaHP");

            migrationBuilder.CreateIndex(
                name: "IX_LichThucHanhs_GiangDayHK_NH_GiangDayMSCB_GiangDaySTT_GiangDayMaNhomHP",
                table: "LichThucHanhs",
                columns: new[] { "GiangDayHK_NH", "GiangDayMSCB", "GiangDaySTT", "GiangDayMaNhomHP" });

            migrationBuilder.CreateIndex(
                name: "IX_LichThucHanhs_SoPhong",
                table: "LichThucHanhs",
                column: "SoPhong");

            migrationBuilder.CreateIndex(
                name: "IX_LichThucHanhs_TenBuoi",
                table: "LichThucHanhs",
                column: "TenBuoi");

            migrationBuilder.CreateIndex(
                name: "IX_LichThucHanhs_TuanSoTuan",
                table: "LichThucHanhs",
                column: "TuanSoTuan");

            migrationBuilder.CreateIndex(
                name: "IX_NhomHocPhans_HocPhanMaHP",
                table: "NhomHocPhans",
                column: "HocPhanMaHP");

            migrationBuilder.CreateIndex(
                name: "IX_Phongs_CauHinhIdCauHinh",
                table: "Phongs",
                column: "CauHinhIdCauHinh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaiDatPhanMems");

            migrationBuilder.DropTable(
                name: "HocPhanPhuHops");

            migrationBuilder.DropTable(
                name: "LichThucHanhs");

            migrationBuilder.DropTable(
                name: "PhanMems");

            migrationBuilder.DropTable(
                name: "Buois");

            migrationBuilder.DropTable(
                name: "GiangDays");

            migrationBuilder.DropTable(
                name: "Phongs");

            migrationBuilder.DropTable(
                name: "Tuans");

            migrationBuilder.DropTable(
                name: "BuoiThucHanhs");

            migrationBuilder.DropTable(
                name: "GiangViens");

            migrationBuilder.DropTable(
                name: "HocKyNamHocs");

            migrationBuilder.DropTable(
                name: "NhomHocPhans");

            migrationBuilder.DropTable(
                name: "CauHinhs");

            migrationBuilder.DropTable(
                name: "HocPhans");

            migrationBuilder.DropTable(
                name: "CPUs");

            migrationBuilder.DropTable(
                name: "OCungs");

            migrationBuilder.DropTable(
                name: "RAMs");
        }
    }
}
