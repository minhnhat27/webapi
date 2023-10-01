﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWebAPI.Data;

#nullable disable

namespace MyWebAPI.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20231001092830_addngaynghi1")]
    partial class addngaynghi1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MyWebAPI.Models.Buoi", b =>
                {
                    b.Property<string>("TenBuoi")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("TenBuoi");

                    b.ToTable("Buois");
                });

            modelBuilder.Entity("MyWebAPI.Models.BuoiThucHanh", b =>
                {
                    b.Property<int>("STT")
                        .HasColumnType("int");

                    b.HasKey("STT");

                    b.ToTable("BuoiThucHanhs");
                });

            modelBuilder.Entity("MyWebAPI.Models.CPU", b =>
                {
                    b.Property<string>("Ten")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Ten");

                    b.ToTable("CPUs");
                });

            modelBuilder.Entity("MyWebAPI.Models.CaiDatPhanMem", b =>
                {
                    b.Property<int>("SoPhong")
                        .HasColumnType("int");

                    b.Property<int>("IdPhanMem")
                        .HasColumnType("int");

                    b.HasKey("SoPhong", "IdPhanMem");

                    b.HasIndex("IdPhanMem");

                    b.ToTable("CaiDatPhanMems");
                });

            modelBuilder.Entity("MyWebAPI.Models.CauHinh", b =>
                {
                    b.Property<int>("IdCauHinh")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCauHinh"));

                    b.Property<string>("CPUTen")
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("OCungDungLuong")
                        .HasColumnType("int");

                    b.Property<int?>("RAMDungLuong")
                        .HasColumnType("int");

                    b.HasKey("IdCauHinh");

                    b.HasIndex("CPUTen");

                    b.HasIndex("OCungDungLuong");

                    b.HasIndex("RAMDungLuong");

                    b.ToTable("CauHinhs");
                });

            modelBuilder.Entity("MyWebAPI.Models.GiangDay", b =>
                {
                    b.Property<string>("HK_NH")
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("GiangVienId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BuoiThucHanhSTT")
                        .HasColumnType("int");

                    b.Property<string>("MaNhomHP")
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("caNgay")
                        .HasColumnType("bit");

                    b.Property<bool>("onSchedule")
                        .HasColumnType("bit");

                    b.HasKey("HK_NH", "GiangVienId", "BuoiThucHanhSTT", "MaNhomHP");

                    b.HasIndex("BuoiThucHanhSTT");

                    b.HasIndex("GiangVienId");

                    b.HasIndex("MaNhomHP");

                    b.ToTable("GiangDays");
                });

            modelBuilder.Entity("MyWebAPI.Models.GiangVien", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("HoTen")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SoDienThoai")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("MyWebAPI.Models.HocKyNamHoc", b =>
                {
                    b.Property<string>("HK_NH")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("HocKyHienTai")
                        .HasColumnType("bit");

                    b.Property<DateTime>("NgayBatDau")
                        .HasMaxLength(20)
                        .HasColumnType("datetime2");

                    b.HasKey("HK_NH");

                    b.ToTable("HocKyNamHocs");
                });

            modelBuilder.Entity("MyWebAPI.Models.HocPhan", b =>
                {
                    b.Property<string>("MaHP")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("SoTietThucHanh")
                        .HasColumnType("int");

                    b.Property<int>("SoTinChi")
                        .HasColumnType("int");

                    b.Property<string>("TenHocPhan")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("MaHP");

                    b.ToTable("HocPhans");
                });

            modelBuilder.Entity("MyWebAPI.Models.HocPhanPhuHop", b =>
                {
                    b.Property<int>("SoPhong")
                        .HasColumnType("int");

                    b.Property<string>("MaHP")
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("SoPhong", "MaHP");

                    b.HasIndex("MaHP");

                    b.ToTable("HocPhanPhuHops");
                });

            modelBuilder.Entity("MyWebAPI.Models.LichThucHanh", b =>
                {
                    b.Property<string>("TenBuoi")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("HK_NH")
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("GiangVienId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MaNhomHP")
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("BuoiThucHanhSTT")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("NgayThucHanh")
                        .HasMaxLength(20)
                        .HasColumnType("datetime2");

                    b.Property<int?>("PhongSoPhong")
                        .HasColumnType("int");

                    b.Property<int>("TuanSoTuan")
                        .HasColumnType("int");

                    b.HasKey("TenBuoi", "HK_NH", "GiangVienId", "MaNhomHP", "BuoiThucHanhSTT");

                    b.HasIndex("PhongSoPhong");

                    b.HasIndex("TuanSoTuan");

                    b.HasIndex("HK_NH", "GiangVienId", "BuoiThucHanhSTT", "MaNhomHP");

                    b.ToTable("LichThucHanhs");
                });

            modelBuilder.Entity("MyWebAPI.Models.NhomHocPhan", b =>
                {
                    b.Property<string>("MaNhomHP")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("HocPhanMaHP")
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("SoLuongSV")
                        .HasColumnType("int");

                    b.HasKey("MaNhomHP");

                    b.HasIndex("HocPhanMaHP");

                    b.ToTable("NhomHocPhans");
                });

            modelBuilder.Entity("MyWebAPI.Models.OCung", b =>
                {
                    b.Property<int>("DungLuong")
                        .HasColumnType("int");

                    b.HasKey("DungLuong");

                    b.ToTable("OCungs");
                });

            modelBuilder.Entity("MyWebAPI.Models.PhanMem", b =>
                {
                    b.Property<int>("IdPhanMem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPhanMem"));

                    b.Property<string>("TenPhanMem")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("IdPhanMem");

                    b.ToTable("PhanMems");
                });

            modelBuilder.Entity("MyWebAPI.Models.Phong", b =>
                {
                    b.Property<int>("SoPhong")
                        .HasColumnType("int");

                    b.Property<int?>("CauHinhIdCauHinh")
                        .HasColumnType("int");

                    b.Property<int>("SoLuongMayTinh")
                        .HasColumnType("int");

                    b.HasKey("SoPhong");

                    b.HasIndex("CauHinhIdCauHinh");

                    b.ToTable("Phongs");
                });

            modelBuilder.Entity("MyWebAPI.Models.RAM", b =>
                {
                    b.Property<int>("DungLuong")
                        .HasColumnType("int");

                    b.HasKey("DungLuong");

                    b.ToTable("RAMs");
                });

            modelBuilder.Entity("MyWebAPI.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("MyWebAPI.Models.Tuan", b =>
                {
                    b.Property<int>("SoTuan")
                        .HasColumnType("int");

                    b.HasKey("SoTuan");

                    b.ToTable("Tuans");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("MyWebAPI.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MyWebAPI.Models.GiangVien", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MyWebAPI.Models.GiangVien", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("MyWebAPI.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.GiangVien", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MyWebAPI.Models.GiangVien", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyWebAPI.Models.CaiDatPhanMem", b =>
                {
                    b.HasOne("MyWebAPI.Models.PhanMem", "PhanMem")
                        .WithMany("CaiDatPhanMems")
                        .HasForeignKey("IdPhanMem")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.Phong", "Phong")
                        .WithMany("CaiDatPhanMems")
                        .HasForeignKey("SoPhong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PhanMem");

                    b.Navigation("Phong");
                });

            modelBuilder.Entity("MyWebAPI.Models.CauHinh", b =>
                {
                    b.HasOne("MyWebAPI.Models.CPU", "CPU")
                        .WithMany("CauHinhs")
                        .HasForeignKey("CPUTen");

                    b.HasOne("MyWebAPI.Models.OCung", "OCung")
                        .WithMany("CauHinhs")
                        .HasForeignKey("OCungDungLuong");

                    b.HasOne("MyWebAPI.Models.RAM", "RAM")
                        .WithMany("CauHinhs")
                        .HasForeignKey("RAMDungLuong");

                    b.Navigation("CPU");

                    b.Navigation("OCung");

                    b.Navigation("RAM");
                });

            modelBuilder.Entity("MyWebAPI.Models.GiangDay", b =>
                {
                    b.HasOne("MyWebAPI.Models.BuoiThucHanh", "BuoiThucHanh")
                        .WithMany("GiangDays")
                        .HasForeignKey("BuoiThucHanhSTT")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.GiangVien", "GiangVien")
                        .WithMany("GiangDays")
                        .HasForeignKey("GiangVienId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.HocKyNamHoc", "HocKyNamHoc")
                        .WithMany("GiangDays")
                        .HasForeignKey("HK_NH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.NhomHocPhan", "NhomHocPhan")
                        .WithMany("GiangDays")
                        .HasForeignKey("MaNhomHP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuoiThucHanh");

                    b.Navigation("GiangVien");

                    b.Navigation("HocKyNamHoc");

                    b.Navigation("NhomHocPhan");
                });

            modelBuilder.Entity("MyWebAPI.Models.HocPhanPhuHop", b =>
                {
                    b.HasOne("MyWebAPI.Models.HocPhan", "HocPhan")
                        .WithMany("HocPhanPhuHops")
                        .HasForeignKey("MaHP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.Phong", "Phong")
                        .WithMany("HocPhanPhuHops")
                        .HasForeignKey("SoPhong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HocPhan");

                    b.Navigation("Phong");
                });

            modelBuilder.Entity("MyWebAPI.Models.LichThucHanh", b =>
                {
                    b.HasOne("MyWebAPI.Models.Phong", "Phong")
                        .WithMany("LichThucHanhs")
                        .HasForeignKey("PhongSoPhong");

                    b.HasOne("MyWebAPI.Models.Buoi", "Buoi")
                        .WithMany("LichThucHanhs")
                        .HasForeignKey("TenBuoi")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.Tuan", "Tuan")
                        .WithMany("LichThucHanhs")
                        .HasForeignKey("TuanSoTuan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.GiangDay", "GiangDay")
                        .WithMany("LichThucHanhs")
                        .HasForeignKey("HK_NH", "GiangVienId", "BuoiThucHanhSTT", "MaNhomHP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buoi");

                    b.Navigation("GiangDay");

                    b.Navigation("Phong");

                    b.Navigation("Tuan");
                });

            modelBuilder.Entity("MyWebAPI.Models.NhomHocPhan", b =>
                {
                    b.HasOne("MyWebAPI.Models.HocPhan", "HocPhan")
                        .WithMany("NhomHocPhans")
                        .HasForeignKey("HocPhanMaHP");

                    b.Navigation("HocPhan");
                });

            modelBuilder.Entity("MyWebAPI.Models.Phong", b =>
                {
                    b.HasOne("MyWebAPI.Models.CauHinh", "CauHinh")
                        .WithMany("Phongs")
                        .HasForeignKey("CauHinhIdCauHinh");

                    b.Navigation("CauHinh");
                });

            modelBuilder.Entity("MyWebAPI.Models.Buoi", b =>
                {
                    b.Navigation("LichThucHanhs");
                });

            modelBuilder.Entity("MyWebAPI.Models.BuoiThucHanh", b =>
                {
                    b.Navigation("GiangDays");
                });

            modelBuilder.Entity("MyWebAPI.Models.CPU", b =>
                {
                    b.Navigation("CauHinhs");
                });

            modelBuilder.Entity("MyWebAPI.Models.CauHinh", b =>
                {
                    b.Navigation("Phongs");
                });

            modelBuilder.Entity("MyWebAPI.Models.GiangDay", b =>
                {
                    b.Navigation("LichThucHanhs");
                });

            modelBuilder.Entity("MyWebAPI.Models.GiangVien", b =>
                {
                    b.Navigation("GiangDays");
                });

            modelBuilder.Entity("MyWebAPI.Models.HocKyNamHoc", b =>
                {
                    b.Navigation("GiangDays");
                });

            modelBuilder.Entity("MyWebAPI.Models.HocPhan", b =>
                {
                    b.Navigation("HocPhanPhuHops");

                    b.Navigation("NhomHocPhans");
                });

            modelBuilder.Entity("MyWebAPI.Models.NhomHocPhan", b =>
                {
                    b.Navigation("GiangDays");
                });

            modelBuilder.Entity("MyWebAPI.Models.OCung", b =>
                {
                    b.Navigation("CauHinhs");
                });

            modelBuilder.Entity("MyWebAPI.Models.PhanMem", b =>
                {
                    b.Navigation("CaiDatPhanMems");
                });

            modelBuilder.Entity("MyWebAPI.Models.Phong", b =>
                {
                    b.Navigation("CaiDatPhanMems");

                    b.Navigation("HocPhanPhuHops");

                    b.Navigation("LichThucHanhs");
                });

            modelBuilder.Entity("MyWebAPI.Models.RAM", b =>
                {
                    b.Navigation("CauHinhs");
                });

            modelBuilder.Entity("MyWebAPI.Models.Tuan", b =>
                {
                    b.Navigation("LichThucHanhs");
                });
#pragma warning restore 612, 618
        }
    }
}
