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
    [Migration("20240427000527_Initial migration")]
    partial class Initialmigration
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

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfReviews")
                        .HasColumnType("int");

                    b.Property<Guid>("PlaceItemID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserCreatedID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserReservedID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PlaceID");

                    b.HasIndex("PlaceItemID");

                    b.HasIndex("UserCreatedID");

                    b.HasIndex("UserReservedID");

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

                    b.Property<int>("NumberOfPlacesCurrentlyOfThisType")
                        .HasColumnType("int");

                    b.HasKey("PlaceItemID");

                    b.ToTable("PlaceItems");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.Review", b =>
                {
                    b.Property<Guid>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CommentOfReview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberOfStars")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PlaceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TypeOfReview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReviewID");

                    b.HasIndex("PlaceID");

                    b.HasIndex("UserID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsManager")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.Place", b =>
                {
                    b.HasOne("GdeIzaci.Models.Domain.PlaceItem", "PlaceItem")
                        .WithMany("Places")
                        .HasForeignKey("PlaceItemID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GdeIzaci.Models.Domain.User", "PlaceCreatedBy")
                        .WithMany("CreatedPlaces")
                        .HasForeignKey("UserCreatedID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GdeIzaci.Models.Domain.User", "PlaceReservedBy")
                        .WithMany("ReservedPlaces")
                        .HasForeignKey("UserReservedID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PlaceCreatedBy");

                    b.Navigation("PlaceItem");

                    b.Navigation("PlaceReservedBy");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.Review", b =>
                {
                    b.HasOne("GdeIzaci.Models.Domain.Place", "Place")
                        .WithMany("Reviews")
                        .HasForeignKey("PlaceID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GdeIzaci.Models.Domain.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Place");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.Place", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.PlaceItem", b =>
                {
                    b.Navigation("Places");
                });

            modelBuilder.Entity("GdeIzaci.Models.Domain.User", b =>
                {
                    b.Navigation("CreatedPlaces");

                    b.Navigation("ReservedPlaces");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
