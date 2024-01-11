﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RekdApi.Models;

#nullable disable

namespace RekdApi.Migrations
{
    [DbContext(typeof(GameDbContext))]
    partial class GameDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GameSessionUser", b =>
                {
                    b.Property<long>("GameSessionsId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlayersId")
                        .HasColumnType("bigint");

                    b.HasKey("GameSessionsId", "PlayersId");

                    b.HasIndex("PlayersId");

                    b.ToTable("GameSessionUser");
                });

            modelBuilder.Entity("RekdApi.Models.GameSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("RekdApi.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("ExpoPushToken")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GameSessionUser", b =>
                {
                    b.HasOne("RekdApi.Models.GameSession", null)
                        .WithMany()
                        .HasForeignKey("GameSessionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RekdApi.Models.User", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}