﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using hotelASP.Data;

#nullable disable

namespace hotelASP.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("hotelASP.Entities.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("CreateDate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("hotelASP.Models.Reservation", b =>
                {
                    b.Property<int>("Id_reservation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_reservation"));

                    b.Property<DateTime>("Date_from")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date_to")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("First_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Id_room")
                        .HasColumnType("int");

                    b.Property<string>("Last_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id_reservation");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("hotelASP.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("hotelASP.Models.Room", b =>
                {
                    b.Property<int>("IdRoom")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdRoom"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("FloorNumber")
                        .HasColumnType("int");

                    b.Property<int>("IdStandard")
                        .HasColumnType("int");

                    b.Property<int>("IdType")
                        .HasColumnType("int");

                    b.Property<int>("IsTaken")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.HasKey("IdRoom");

                    b.HasIndex("IdStandard");

                    b.HasIndex("IdType");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("hotelASP.Models.RoomType", b =>
                {
                    b.Property<int>("IdType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdType"));

                    b.Property<float>("BasePrice")
                        .HasColumnType("float");

                    b.Property<int>("BedNumber")
                        .HasColumnType("int");

                    b.Property<int>("PeopleNumber")
                        .HasColumnType("int");

                    b.HasKey("IdType");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("hotelASP.Models.Standard", b =>
                {
                    b.Property<int>("IdStandard")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdStandard"));

                    b.Property<string>("StandardName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("StandardValue")
                        .HasColumnType("float");

                    b.HasKey("IdStandard");

                    b.ToTable("Standards");
                });

            modelBuilder.Entity("hotelASP.Entities.UserAccount", b =>
                {
                    b.HasOne("hotelASP.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("hotelASP.Models.Room", b =>
                {
                    b.HasOne("hotelASP.Models.Standard", "Standard")
                        .WithMany("Rooms")
                        .HasForeignKey("IdStandard")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hotelASP.Models.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("IdType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomType");

                    b.Navigation("Standard");
                });

            modelBuilder.Entity("hotelASP.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("hotelASP.Models.RoomType", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("hotelASP.Models.Standard", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
