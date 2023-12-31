﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace movieappbackend.Migrations
{
    [DbContext(typeof(MovieAppDbContext))]
    partial class MovieAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.MovieList", b =>
                {
                    b.Property<string>("MovieListId")
                        .HasColumnType("text");

                    b.Property<List<string>>("ImbdIds")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("ListDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ListName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MProfileId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileId")
                        .HasColumnType("text");

                    b.HasKey("MovieListId");

                    b.HasIndex("MProfileId");

                    b.HasIndex("ProfileId");

                    b.ToTable("MovieLists");
                });

            modelBuilder.Entity("Models.Profile", b =>
                {
                    b.Property<string>("ProfileId")
                        .HasColumnType("text");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProfileId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Models.Review", b =>
                {
                    b.Property<string>("ReviewId")
                        .HasColumnType("text");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImdbID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MovieTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileId")
                        .HasColumnType("text");

                    b.Property<DateTime>("PublishedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RProfileId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ReviewTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ReviewId");

                    b.HasIndex("ProfileId");

                    b.HasIndex("RProfileId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.MovieList", b =>
                {
                    b.HasOne("Models.Profile", null)
                        .WithMany()
                        .HasForeignKey("MProfileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Models.Profile", null)
                        .WithMany("MovieLists")
                        .HasForeignKey("ProfileId");
                });

            modelBuilder.Entity("Models.Review", b =>
                {
                    b.HasOne("Models.Profile", null)
                        .WithMany("Reviews")
                        .HasForeignKey("ProfileId");

                    b.HasOne("Models.Profile", null)
                        .WithMany()
                        .HasForeignKey("RProfileId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Profile", b =>
                {
                    b.Navigation("MovieLists");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
