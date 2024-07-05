﻿// <auto-generated />
using System;
using Mediscreen.Infrastructure.SqlServerDatabase.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mediscreen.Infrastructure.SqlServerDatabase.Migrations
{
    [DbContext(typeof(MediscreenSqlServerContext))]
    [Migration("20240705160245_UpdatePatientEntityWithNonRequiredAttributes")]
    partial class UpdatePatientEntityWithNonRequiredAttributes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mediscreen.Infrastructure.SqlServerDatabase.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patient", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 249,
                            BirthDate = new DateTime(2010, 8, 15, 10, 54, 55, 880, DateTimeKind.Local).AddTicks(392),
                            FirstName = "Bernita",
                            Gender = "F",
                            HomeAddress = "0239 Bradtke Port, Madelynnburgh, Nauru",
                            LastName = "Konopelski",
                            PhoneNumber = "1-701-937-1738 x8576"
                        },
                        new
                        {
                            Id = 772,
                            BirthDate = new DateTime(2006, 10, 12, 1, 37, 15, 873, DateTimeKind.Local).AddTicks(2803),
                            FirstName = "Guillermo",
                            Gender = "M",
                            HomeAddress = "700 Dane Branch, Georgianaburgh, Philippines",
                            LastName = "Cummerata",
                            PhoneNumber = "703-237-8869"
                        },
                        new
                        {
                            Id = 294,
                            BirthDate = new DateTime(2020, 12, 8, 16, 19, 35, 866, DateTimeKind.Local).AddTicks(3368),
                            FirstName = "Max",
                            Gender = "F",
                            HomeAddress = "58600 Heidenreich Isle, Casperhaven, Saint Martin",
                            LastName = "Streich",
                            PhoneNumber = "(340) 694-5534 x225"
                        },
                        new
                        {
                            Id = 816,
                            BirthDate = new DateTime(2017, 2, 4, 7, 1, 55, 859, DateTimeKind.Local).AddTicks(3838),
                            FirstName = "Yoshiko",
                            Gender = "M",
                            HomeAddress = "253 Jany Shoals, Oswaldomouth, Mali",
                            LastName = "Maggio",
                            PhoneNumber = "(838) 864-0031 x636"
                        },
                        new
                        {
                            Id = 339,
                            BirthDate = new DateTime(2013, 4, 2, 21, 44, 15, 852, DateTimeKind.Local).AddTicks(4281),
                            FirstName = "Edd",
                            Gender = "M",
                            HomeAddress = "93004 Mertz Cove, Port Austenfort, Egypt",
                            LastName = "Gislason",
                            PhoneNumber = "(271) 872-7910 x731"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
