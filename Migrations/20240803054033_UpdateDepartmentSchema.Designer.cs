﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using final_project.Services;

#nullable disable

namespace final_project.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240803054033_UpdateDepartmentSchema")]
    partial class UpdateDepartmentSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("final_project.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepId")
                        .HasColumnType("int");

                    b.Property<int>("minDegree")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.HasIndex("DepId");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("final_project.Models.Department", b =>
                {
                    b.Property<int>("DepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepId"));

                    b.Property<string>("DepManager")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DepId");

                    b.ToTable("Departments", (string)null);
                });

            modelBuilder.Entity("final_project.Models.Instructor", b =>
                {
                    b.Property<int>("InstId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstId"));

                    b.Property<int>("CrsCourseId")
                        .HasColumnType("int");

                    b.Property<string>("ImageFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstCrsId")
                        .HasColumnType("int");

                    b.Property<int>("InstDepId")
                        .HasColumnType("int");

                    b.Property<string>("InstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("InstSalary")
                        .HasColumnType("int");

                    b.HasKey("InstId");

                    b.HasIndex("CrsCourseId");

                    b.HasIndex("InstDepId");

                    b.ToTable("Instructors", (string)null);
                });

            modelBuilder.Entity("final_project.Models.Trainee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepId")
                        .HasColumnType("int");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<string>("ImageFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("DepId");

                    b.ToTable("Trainees", (string)null);
                });

            modelBuilder.Entity("final_project.Models.crsResult", b =>
                {
                    b.Property<int>("ResId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResId"));

                    b.Property<int>("CrsId")
                        .HasColumnType("int");

                    b.Property<int>("ResDegree")
                        .HasColumnType("int");

                    b.Property<int>("TraineeId")
                        .HasColumnType("int");

                    b.HasKey("ResId");

                    b.HasIndex("CrsId");

                    b.HasIndex("TraineeId");

                    b.ToTable("crsResults", (string)null);
                });

            modelBuilder.Entity("final_project.Models.Course", b =>
                {
                    b.HasOne("final_project.Models.Department", "Dep")
                        .WithMany("Courses")
                        .HasForeignKey("DepId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Dep");
                });

            modelBuilder.Entity("final_project.Models.Instructor", b =>
                {
                    b.HasOne("final_project.Models.Course", "Crs")
                        .WithMany("Instructors")
                        .HasForeignKey("CrsCourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("final_project.Models.Department", "Dep")
                        .WithMany("Instructors")
                        .HasForeignKey("InstDepId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Crs");

                    b.Navigation("Dep");
                });

            modelBuilder.Entity("final_project.Models.Trainee", b =>
                {
                    b.HasOne("final_project.Models.Department", "Dep")
                        .WithMany("Trainees")
                        .HasForeignKey("DepId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Dep");
                });

            modelBuilder.Entity("final_project.Models.crsResult", b =>
                {
                    b.HasOne("final_project.Models.Course", "Crs")
                        .WithMany("crsResults")
                        .HasForeignKey("CrsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("final_project.Models.Trainee", "Trainee")
                        .WithMany("crsResults")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Crs");

                    b.Navigation("Trainee");
                });

            modelBuilder.Entity("final_project.Models.Course", b =>
                {
                    b.Navigation("Instructors");

                    b.Navigation("crsResults");
                });

            modelBuilder.Entity("final_project.Models.Department", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Instructors");

                    b.Navigation("Trainees");
                });

            modelBuilder.Entity("final_project.Models.Trainee", b =>
                {
                    b.Navigation("crsResults");
                });
#pragma warning restore 612, 618
        }
    }
}
