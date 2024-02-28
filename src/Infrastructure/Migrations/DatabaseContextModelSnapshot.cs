﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseAppContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.BookingModels.Booking", b =>
                {
                    b.Property<Guid>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("BookingId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Domain.Models.BookingModels.BookingCancelled", b =>
                {
                    b.Property<Guid>("BookingCancelledId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookingUserStatusId")
                        .HasColumnType("uuid");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.HasKey("BookingCancelledId");

                    b.HasIndex("BookingUserStatusId");

                    b.ToTable("BookingCancelleds");
                });

            modelBuilder.Entity("Domain.Models.BookingModels.BookingUserStatus", b =>
                {
                    b.Property<Guid>("BookingUserStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Accepted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("BookingUserStatusId");

                    b.HasIndex("BookingId");

                    b.HasIndex("UserId");

                    b.ToTable("BookingUserStatus");
                });

            modelBuilder.Entity("Domain.Models.UserModels.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<long>("Dni")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NickName")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Models.BookingModels.Booking", b =>
                {
                    b.HasOne("Domain.Models.UserModels.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.BookingModels.BookingCancelled", b =>
                {
                    b.HasOne("Domain.Models.BookingModels.BookingUserStatus", "BookingUserStatus")
                        .WithMany("BookingCancelleds")
                        .HasForeignKey("BookingUserStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookingUserStatus");
                });

            modelBuilder.Entity("Domain.Models.BookingModels.BookingUserStatus", b =>
                {
                    b.HasOne("Domain.Models.BookingModels.Booking", "Booking")
                        .WithMany("BookingUserStatus")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.UserModels.User", "User")
                        .WithMany("BookingsStatus")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.BookingModels.Booking", b =>
                {
                    b.Navigation("BookingUserStatus");
                });

            modelBuilder.Entity("Domain.Models.BookingModels.BookingUserStatus", b =>
                {
                    b.Navigation("BookingCancelleds");
                });

            modelBuilder.Entity("Domain.Models.UserModels.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("BookingsStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
