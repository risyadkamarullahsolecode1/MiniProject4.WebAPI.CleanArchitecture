﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniProject4.Infrastructure.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiniProject4.Infrastructure.Migrations
{
    [DbContext(typeof(CompaniesContext))]
    partial class CompaniesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MiniProject4.Domain.Entities.Department", b =>
                {
                    b.Property<int>("Deptno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("deptno");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Deptno"));

                    b.Property<string>("Deptname")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("deptname");

                    b.Property<int?>("Mgrempno")
                        .HasColumnType("integer")
                        .HasColumnName("mgrempno");

                    b.HasKey("Deptno")
                        .HasName("departments_pkey");

                    b.HasIndex("Mgrempno");

                    b.ToTable("departments");
                });

            modelBuilder.Entity("MiniProject4.Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Empno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("empno");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Empno"));

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("address");

                    b.Property<int?>("Deptno")
                        .HasColumnType("integer")
                        .HasColumnName("deptno");

                    b.Property<DateOnly?>("Dob")
                        .HasColumnType("date");

                    b.Property<string>("Fname")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("fname");

                    b.Property<string>("Lname")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("lname");

                    b.Property<string>("Position")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("position");

                    b.Property<string>("Sex")
                        .HasColumnType("character varying")
                        .HasColumnName("sex");

                    b.HasKey("Empno")
                        .HasName("employees_pkey");

                    b.HasIndex("Deptno");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("MiniProject4.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Projno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("projno");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Projno"));

                    b.Property<int?>("Deptno")
                        .HasColumnType("integer")
                        .HasColumnName("deptno");

                    b.Property<string>("Projname")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("projname");

                    b.HasKey("Projno")
                        .HasName("projects_pkey");

                    b.HasIndex("Deptno");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("MiniProject4.Domain.Entities.Workson", b =>
                {
                    b.Property<int>("Empno")
                        .HasColumnType("integer")
                        .HasColumnName("empno");

                    b.Property<int>("Projno")
                        .HasColumnType("integer")
                        .HasColumnName("projno");

                    b.Property<DateTime?>("Dateworked")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("dateworked");

                    b.Property<int?>("Hoursworked")
                        .HasColumnType("integer")
                        .HasColumnName("hoursworked");

                    b.HasKey("Empno", "Projno")
                        .HasName("workson_pkey");

                    b.HasIndex("Projno");

                    b.ToTable("workson");
                });

            modelBuilder.Entity("MiniProject4.Domain.Entities.Department", b =>
                {
                    b.HasOne("MiniProject4.Domain.Entities.Employee", "MgrempnoNavigation")
                        .WithMany("Departments")
                        .HasForeignKey("Mgrempno")
                        .HasConstraintName("departments_mgrempno_fkey");

                    b.Navigation("MgrempnoNavigation");
                });

            modelBuilder.Entity("MiniProject4.Domain.Entities.Employee", b =>
                {
                    b.HasOne("MiniProject4.Domain.Entities.Department", "DeptnoNavigation")
                        .WithMany("Employees")
                        .HasForeignKey("Deptno")
                        .HasConstraintName("fk_deptno");

                    b.Navigation("DeptnoNavigation");
                });

            modelBuilder.Entity("MiniProject4.Domain.Entities.Project", b =>
                {
                    b.HasOne("MiniProject4.Domain.Entities.Department", "DeptnoNavigation")
                        .WithMany("Projects")
                        .HasForeignKey("Deptno")
                        .HasConstraintName("projects_deptno_fkey");

                    b.Navigation("DeptnoNavigation");
                });

            modelBuilder.Entity("MiniProject4.Domain.Entities.Workson", b =>
                {
                    b.HasOne("MiniProject4.Domain.Entities.Employee", "EmpnoNavigation")
                        .WithMany("Worksons")
                        .HasForeignKey("Empno")
                        .IsRequired()
                        .HasConstraintName("workson_empno_fkey");

                    b.HasOne("MiniProject4.Domain.Entities.Project", "ProjnoNavigation")
                        .WithMany("Worksons")
                        .HasForeignKey("Projno")
                        .IsRequired()
                        .HasConstraintName("workson_projno_fkey");

                    b.Navigation("EmpnoNavigation");

                    b.Navigation("ProjnoNavigation");
                });

            modelBuilder.Entity("MiniProject4.Domain.Entities.Department", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("MiniProject4.Domain.Entities.Employee", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Worksons");
                });

            modelBuilder.Entity("MiniProject4.Domain.Entities.Project", b =>
                {
                    b.Navigation("Worksons");
                });
#pragma warning restore 612, 618
        }
    }
}
