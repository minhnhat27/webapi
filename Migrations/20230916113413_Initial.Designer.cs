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
    [Migration("20230916113413_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
                    b.Property<int>("IdCauHinh")
                        .HasColumnType("int");

                    b.Property<int>("IdPhanMem")
                        .HasColumnType("int");

                    b.HasKey("IdCauHinh", "IdPhanMem");

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

                    b.Property<string>("MSCB")
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("STT")
                        .HasColumnType("int");

                    b.Property<string>("MaNhomHP")
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("HK_NH", "MSCB", "STT", "MaNhomHP");

                    b.HasIndex("MSCB");

                    b.HasIndex("MaNhomHP");

                    b.HasIndex("STT");

                    b.ToTable("GiangDays");
                });

            modelBuilder.Entity("MyWebAPI.Models.GiangVien", b =>
                {
                    b.Property<string>("MSCB")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("MSCB");

                    b.ToTable("GiangViens");
                });

            modelBuilder.Entity("MyWebAPI.Models.HocKyNamHoc", b =>
                {
                    b.Property<string>("HK_NH")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("HocKyHienTai")
                        .HasColumnType("bit");

                    b.Property<string>("NgayBatDau")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

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
                    b.Property<string>("NgayThucHanh")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("SoPhong")
                        .HasColumnType("int");

                    b.Property<string>("TenBuoi")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("GhiChu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("GiangDayHK_NH")
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("GiangDayMSCB")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("GiangDayMaNhomHP")
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("GiangDaySTT")
                        .HasColumnType("int");

                    b.Property<int?>("TuanSoTuan")
                        .HasColumnType("int");

                    b.HasKey("NgayThucHanh", "SoPhong", "TenBuoi");

                    b.HasIndex("SoPhong");

                    b.HasIndex("TenBuoi");

                    b.HasIndex("TuanSoTuan");

                    b.HasIndex("GiangDayHK_NH", "GiangDayMSCB", "GiangDaySTT", "GiangDayMaNhomHP");

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

            modelBuilder.Entity("MyWebAPI.Models.Tuan", b =>
                {
                    b.Property<int>("SoTuan")
                        .HasColumnType("int");

                    b.HasKey("SoTuan");

                    b.ToTable("Tuans");
                });

            modelBuilder.Entity("MyWebAPI.Models.CaiDatPhanMem", b =>
                {
                    b.HasOne("MyWebAPI.Models.CauHinh", "CauHinh")
                        .WithMany("CaiDatPhanMems")
                        .HasForeignKey("IdCauHinh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.PhanMem", "PhanMem")
                        .WithMany("CaiDatPhanMems")
                        .HasForeignKey("IdPhanMem")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CauHinh");

                    b.Navigation("PhanMem");
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
                    b.HasOne("MyWebAPI.Models.HocKyNamHoc", "HocKyNamHoc")
                        .WithMany("GiangDays")
                        .HasForeignKey("HK_NH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.GiangVien", "GiangVien")
                        .WithMany("GiangDays")
                        .HasForeignKey("MSCB")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.NhomHocPhan", "NhomHocPhan")
                        .WithMany("GiangDays")
                        .HasForeignKey("MaNhomHP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.BuoiThucHanh", "BuoiThucHanh")
                        .WithMany("GiangDays")
                        .HasForeignKey("STT")
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
                        .HasForeignKey("SoPhong")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.Buoi", "Buoi")
                        .WithMany("LichThucHanhs")
                        .HasForeignKey("TenBuoi")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyWebAPI.Models.Tuan", "Tuan")
                        .WithMany("LichThucHanhs")
                        .HasForeignKey("TuanSoTuan");

                    b.HasOne("MyWebAPI.Models.GiangDay", "GiangDay")
                        .WithMany("LichThucHanhs")
                        .HasForeignKey("GiangDayHK_NH", "GiangDayMSCB", "GiangDaySTT", "GiangDayMaNhomHP");

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
                    b.Navigation("CaiDatPhanMems");

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