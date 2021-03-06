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
    [Migration("20201230175105_GoalValidation")]
    partial class GoalValidation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("BC.Server.Models.DB.Goal", b =>
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

                    b.Property<int?>("ParentGoalId")
                        .HasColumnType("int");

                    b.HasKey("GoalId");

                    b.HasIndex("Difficulty");

                    b.HasIndex("ExpectedDeadline");

                    b.HasIndex("IsCompleted");

                    b.HasIndex("ParentGoalId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("BC.Server.Models.DB.Goal", b =>
                {
                    b.HasOne("BC.Server.Models.DB.Goal", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentGoalId");

                    b.Navigation("Parent");
                });
#pragma warning restore 612, 618
        }
    }
}
