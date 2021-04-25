﻿// <auto-generated />
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210421114602_ModifiedEntiesAndNewAdded")]
    partial class ModifiedEntiesAndNewAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("API.Entities.City", b =>
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

            modelBuilder.Entity("API.Entities.Commune", b =>
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

            modelBuilder.Entity("API.Entities.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DistrictName")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProvienceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProvienceId");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("API.Entities.Norm", b =>
                {
                    b.Property<string>("ParamCode")
                        .HasColumnType("TEXT");

                    b.Property<double>("ParamNorm")
                        .HasColumnType("REAL");

                    b.HasKey("ParamCode");

                    b.ToTable("Norms");
                });

            modelBuilder.Entity("API.Entities.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProvinceName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("API.Entities.Station", b =>
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

            modelBuilder.Entity("API.Entities.City", b =>
                {
                    b.HasOne("API.Entities.Commune", "Commune")
                        .WithMany("Cities")
                        .HasForeignKey("CommuneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Commune");
                });

            modelBuilder.Entity("API.Entities.Commune", b =>
                {
                    b.HasOne("API.Entities.District", "District")
                        .WithMany("Communes")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("API.Entities.District", b =>
                {
                    b.HasOne("API.Entities.Province", "Province")
                        .WithMany("Districts")
                        .HasForeignKey("ProvienceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("API.Entities.Station", b =>
                {
                    b.HasOne("API.Entities.City", "City")
                        .WithMany("Stations")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("API.Entities.City", b =>
                {
                    b.Navigation("Stations");
                });

            modelBuilder.Entity("API.Entities.Commune", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("API.Entities.District", b =>
                {
                    b.Navigation("Communes");
                });

            modelBuilder.Entity("API.Entities.Province", b =>
                {
                    b.Navigation("Districts");
                });
#pragma warning restore 612, 618
        }
    }
}