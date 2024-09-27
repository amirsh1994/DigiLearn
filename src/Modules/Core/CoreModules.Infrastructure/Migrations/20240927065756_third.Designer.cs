﻿// <auto-generated />
using System;
using CoreModule.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoreModule.Infrastructure.Migrations
{
    [DbContext(typeof(CoreModuleEfContext))]
    [Migration("20240927065756_third")]
    partial class third
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoreModule.Domain.Categories.Models.CourseCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Categories", "dbo");
                });

            modelBuilder.Entity("CoreModule.Domain.Courses.Models.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CourseLevel")
                        .HasColumnType("int");

                    b.Property<int>("CourseStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("SubCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("VideoName")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Courses", "course");
                });

            modelBuilder.Entity("CoreModule.Domain.Teachers.Models.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CvFileName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Teachers", "dbo");
                });

            modelBuilder.Entity("CoreModule.Infrastructure.Persistence.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasMaxLength(110)
                        .HasColumnType("nvarchar(110)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(110)
                        .HasColumnType("nvarchar(110)");

                    b.Property<string>("Family")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("Id");

                    b.ToTable("Users", "dbo");
                });

            modelBuilder.Entity("CoreModule.Domain.Courses.Models.Course", b =>
                {
                    b.OwnsOne("Common.Domain.ValueObjects.SeoData", "SeoData", b1 =>
                        {
                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Canonical")
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("Canonical");

                            b1.Property<string>("MetaDescription")
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("MetaDescription");

                            b1.Property<string>("MetaKeyWords")
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("MetaKeyWords");

                            b1.Property<string>("MetaTitle")
                                .HasMaxLength(500)
                                .HasColumnType("nvarchar(500)")
                                .HasColumnName("MetaTitle");

                            b1.HasKey("CourseId");

                            b1.ToTable("Courses", "course");

                            b1.WithOwner()
                                .HasForeignKey("CourseId");
                        });

                    b.OwnsMany("CoreModule.Domain.Courses.Models.Section", "Sections", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("CourseId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreationDate")
                                .HasColumnType("datetime2");

                            b1.Property<int>("DisplayOrder")
                                .HasColumnType("int");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.HasKey("Id");

                            b1.HasIndex("CourseId");

                            b1.ToTable("Sections", "course");

                            b1.WithOwner()
                                .HasForeignKey("CourseId");

                            b1.OwnsMany("CoreModule.Domain.Courses.Models.Episode", "Episodes", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("AttachmentName")
                                        .HasMaxLength(200)
                                        .HasColumnType("nvarchar(200)");

                                    b2.Property<DateTime>("CreationDate")
                                        .HasColumnType("datetime2");

                                    b2.Property<string>("EnglishTitle")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .IsUnicode(false)
                                        .HasColumnType("varchar(100)");

                                    b2.Property<bool>("IsActive")
                                        .HasColumnType("bit");

                                    b2.Property<Guid>("SectionId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<TimeSpan>("TimeSpan")
                                        .HasColumnType("time");

                                    b2.Property<string>("Title")
                                        .IsRequired()
                                        .HasMaxLength(100)
                                        .HasColumnType("nvarchar(100)");

                                    b2.Property<Guid>("Token")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("VideoName")
                                        .IsRequired()
                                        .HasMaxLength(200)
                                        .HasColumnType("nvarchar(200)");

                                    b2.HasKey("Id");

                                    b2.HasIndex("SectionId");

                                    b2.ToTable("Episodes", "course");

                                    b2.WithOwner()
                                        .HasForeignKey("SectionId");
                                });

                            b1.Navigation("Episodes");
                        });

                    b.Navigation("Sections");

                    b.Navigation("SeoData")
                        .IsRequired();
                });

            modelBuilder.Entity("CoreModule.Domain.Teachers.Models.Teacher", b =>
                {
                    b.HasOne("CoreModule.Infrastructure.Persistence.Users.User", null)
                        .WithOne()
                        .HasForeignKey("CoreModule.Domain.Teachers.Models.Teacher", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
