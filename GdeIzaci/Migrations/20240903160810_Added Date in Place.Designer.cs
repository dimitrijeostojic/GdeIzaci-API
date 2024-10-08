﻿// <auto-generated />
using System;
using GdeIzaci.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GdeIzaci.Migrations
{
    [DbContext(typeof(GdeIzaciDBContext))]
    [Migration("20240903160810_Added Date in Place")]
    partial class AddedDateinPlace
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GdeIzaci.Models.Domain.Place", b =>
                {
                    b.Property<Guid>("PlaceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PlaceItemID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<Guid>("UserCreatedID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PlaceID");

                    b.HasIndex("PlaceItemID");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.PlaceItem", b =>
                {
                    b.Property<Guid>("PlaceItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlaceItemID");

                    b.ToTable("PlaceItems");

                    b.HasData(
                        new
                        {
                            PlaceItemID = new Guid("4d7d327c-62b6-4fa7-afbf-d682fe3b1a1d"),
                            Name = "Restoran"
                        },
                        new
                        {
                            PlaceItemID = new Guid("8e54fb8e-723f-4b6b-bae2-76d89e1a1c56"),
                            Name = "Kafić"
                        },
                        new
                        {
                            PlaceItemID = new Guid("3c8e3f8d-9a6a-4f91-bfce-8d8e45d14e83"),
                            Name = "Klub"
                        },
                        new
                        {
                            PlaceItemID = new Guid("e6f95667-8ef7-4c5e-b5bf-92d31fdd581b"),
                            Name = "Bar"
                        });
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlaceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReservationDateTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PlaceID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.Review", b =>
                {
                    b.Property<Guid>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlaceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("numberOfStars")
                        .HasColumnType("float");

                    b.HasKey("ReviewID");

                    b.HasIndex("PlaceID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.Place", b =>
                {
                    b.HasOne("GdeIzaci.Models.Domain.PlaceItem", "PlaceItem")
                        .WithMany("Places")
                        .HasForeignKey("PlaceItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlaceItem");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.Reservation", b =>
                {
                    b.HasOne("GdeIzaci.Models.Domain.Place", "Place")
                        .WithMany("Reservations")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Place");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.Review", b =>
                {
                    b.HasOne("GdeIzaci.Models.Domain.Place", null)
                        .WithMany("Reviews")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.Place", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.PlaceItem", b =>
                {
                    b.Navigation("Places");
                });
#pragma warning restore 612, 618
        }
    }
}
