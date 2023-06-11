﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(SmartyContext))]
    partial class SmartyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Core.Entities.Mark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("MarkBrandId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MarkTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("decimal(18.2)");

                    b.HasKey("Id");

                    b.HasIndex("MarkBrandId");

                    b.HasIndex("MarkTypeId");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("Core.Entities.MarkBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MarkBrands");
                });

            modelBuilder.Entity("Core.Entities.MarkType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MarkTypes");
                });

            modelBuilder.Entity("Core.Entities.Test.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TestId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("answer1")
                        .HasColumnType("TEXT");

                    b.Property<string>("answer2")
                        .HasColumnType("TEXT");

                    b.Property<string>("answer3")
                        .HasColumnType("TEXT");

                    b.Property<string>("answer4")
                        .HasColumnType("TEXT");

                    b.Property<string>("correctAnswer")
                        .HasColumnType("TEXT");

                    b.Property<string>("question")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Core.Entities.Test.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Core.Entities.Mark", b =>
                {
                    b.HasOne("Core.Entities.MarkBrand", "MarkBrand")
                        .WithMany()
                        .HasForeignKey("MarkBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.MarkType", "MarkType")
                        .WithMany()
                        .HasForeignKey("MarkTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarkBrand");

                    b.Navigation("MarkType");
                });

            modelBuilder.Entity("Core.Entities.Test.Question", b =>
                {
                    b.HasOne("Core.Entities.Test.Test", null)
                        .WithMany("questions")
                        .HasForeignKey("TestId");
                });

            modelBuilder.Entity("Core.Entities.Test.Test", b =>
                {
                    b.Navigation("questions");
                });
#pragma warning restore 612, 618
        }
    }
}
