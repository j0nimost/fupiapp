﻿// <auto-generated />
using System;
using Fupi_WebApplication.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fupi_WebApplication.Migrations
{
    [DbContext(typeof(FupiAppContext))]
    [Migration("20201216104225_InitCreate")]
    partial class InitCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Fupi_WebApplication.Models.FupiModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<int>("AccessCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("IssuedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("OriginalUrl")
                        .HasColumnType("text");

                    b.Property<string>("ProxyCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProxyCode")
                        .IsUnique();

                    b.ToTable("FupiModel");
                });
#pragma warning restore 612, 618
        }
    }
}
