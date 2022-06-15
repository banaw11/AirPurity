﻿// <auto-generated />
using System;
using AirPurity.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AirPurity.API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("AirPurity.API.Data.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CommuneId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CommuneId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.Commune", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommuneName")
                        .HasColumnType("TEXT");

                    b.Property<int>("DistrictId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Communes");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DistrictName")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.Norm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ParamCode")
                        .HasColumnType("TEXT");

                    b.Property<double>("ParamNorm")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Norms");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IndexLevelId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LastIndexLevelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserEmail")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("StationId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.NotificationSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IndexLevelId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LastIndexLevelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NotificationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ParamCode")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NotificationId");

                    b.ToTable("NotificationSubjects");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProvinceName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressStreet")
                        .HasColumnType("TEXT");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("GegrLat")
                        .HasColumnType("REAL");

                    b.Property<double>("GegrLon")
                        .HasColumnType("REAL");

                    b.Property<string>("StationName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.City", b =>
                {
                    b.HasOne("AirPurity.API.Data.Entities.Commune", "Commune")
                        .WithMany("Cities")
                        .HasForeignKey("CommuneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Commune");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.Commune", b =>
                {
                    b.HasOne("AirPurity.API.Data.Entities.District", "District")
                        .WithMany("Communes")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.District", b =>
                {
                    b.HasOne("AirPurity.API.Data.Entities.Province", "Province")
                        .WithMany("Districts")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.Notification", b =>
                {
                    b.HasOne("AirPurity.API.Data.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AirPurity.API.Data.Entities.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.NotificationSubject", b =>
                {
                    b.HasOne("AirPurity.API.Data.Entities.Notification", "Notification")
                        .WithMany("NotificationSubjects")
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notification");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.Station", b =>
                {
                    b.HasOne("AirPurity.API.Data.Entities.City", "City")
                        .WithMany("Stations")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.City", b =>
                {
                    b.Navigation("Stations");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.Commune", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.District", b =>
                {
                    b.Navigation("Communes");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.Notification", b =>
                {
                    b.Navigation("NotificationSubjects");
                });

            modelBuilder.Entity("AirPurity.API.Data.Entities.Province", b =>
                {
                    b.Navigation("Districts");
                });
#pragma warning restore 612, 618
        }
    }
}
