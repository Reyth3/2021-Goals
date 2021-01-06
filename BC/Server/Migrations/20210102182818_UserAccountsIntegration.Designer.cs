﻿// <auto-generated />
using System;
using BC.Server.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BC.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210102182818_UserAccountsIntegration")]
    partial class UserAccountsIntegration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("BC.Shared.Models.Goal", b =>
                {
                    b.Property<int>("GoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateCompleted")
                        .HasColumnType("datetime2");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpectedDeadline")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int?>("ParentGoalId")
                        .HasColumnType("int");

                    b.HasKey("GoalId");

                    b.HasIndex("Difficulty");

                    b.HasIndex("ExpectedDeadline");

                    b.HasIndex("IsCompleted");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ParentGoalId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("BC.Shared.Models.UserProfile", b =>
                {
                    b.Property<int>("UserProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(48)
                        .HasColumnType("nvarchar(48)");

                    b.HasKey("UserProfileId");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("BC.Shared.Models.Goal", b =>
                {
                    b.HasOne("BC.Shared.Models.UserProfile", "Owner")
                        .WithMany("Goals")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BC.Shared.Models.Goal", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentGoalId");

                    b.Navigation("Owner");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("BC.Shared.Models.UserProfile", b =>
                {
                    b.Navigation("Goals");
                });
#pragma warning restore 612, 618
        }
    }
}