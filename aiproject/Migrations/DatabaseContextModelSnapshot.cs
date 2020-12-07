﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using aiproject;

namespace aiproject.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("aiproject.Entities.AppearanceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("MatchEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("MatchId")
                        .HasColumnType("integer");

                    b.Property<int?>("PlayerEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MatchEntityId");

                    b.HasIndex("PlayerEntityId");

                    b.ToTable("Appearances");
                });

            modelBuilder.Entity("aiproject.Entities.GoalEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("AppearanceEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("AppearanceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppearanceEntityId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("aiproject.Entities.MatchEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("aiproject.Entities.PlayerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("Apps")
                        .HasColumnType("integer");

                    b.Property<int>("Goals")
                        .HasColumnType("integer");

                    b.Property<string>("Img")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("aiproject.Entities.RatingEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("AppearanceEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("AppearanceId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppearanceEntityId");

                    b.HasIndex("UserEntityId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("aiproject.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("aiproject.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<int?>("RoleEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleEntityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("aiproject.Entities.AppearanceEntity", b =>
                {
                    b.HasOne("aiproject.Entities.MatchEntity", "MatchEntity")
                        .WithMany("Appearances")
                        .HasForeignKey("MatchEntityId");

                    b.HasOne("aiproject.Entities.PlayerEntity", "PlayerEntity")
                        .WithMany()
                        .HasForeignKey("PlayerEntityId");

                    b.Navigation("MatchEntity");

                    b.Navigation("PlayerEntity");
                });

            modelBuilder.Entity("aiproject.Entities.GoalEntity", b =>
                {
                    b.HasOne("aiproject.Entities.AppearanceEntity", "AppearanceEntity")
                        .WithMany("Goals")
                        .HasForeignKey("AppearanceEntityId");

                    b.Navigation("AppearanceEntity");
                });

            modelBuilder.Entity("aiproject.Entities.RatingEntity", b =>
                {
                    b.HasOne("aiproject.Entities.AppearanceEntity", "AppearanceEntity")
                        .WithMany("Ratings")
                        .HasForeignKey("AppearanceEntityId");

                    b.HasOne("aiproject.Entities.UserEntity", "UserEntity")
                        .WithMany("Ratings")
                        .HasForeignKey("UserEntityId");

                    b.Navigation("AppearanceEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("aiproject.Entities.UserEntity", b =>
                {
                    b.HasOne("aiproject.Entities.RoleEntity", "RoleEntity")
                        .WithMany("Users")
                        .HasForeignKey("RoleEntityId");

                    b.Navigation("RoleEntity");
                });

            modelBuilder.Entity("aiproject.Entities.AppearanceEntity", b =>
                {
                    b.Navigation("Goals");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("aiproject.Entities.MatchEntity", b =>
                {
                    b.Navigation("Appearances");
                });

            modelBuilder.Entity("aiproject.Entities.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("aiproject.Entities.UserEntity", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
