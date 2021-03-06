// <auto-generated />
using System;
using Fupi_KeyGenService.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fupi_KeyGenService.Migrations
{
    [DbContext(typeof(KeyGenContext))]
    partial class KeyGenContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Fupi_KeyGenService.Models.KeyGenModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

                    b.Property<DateTime>("CreatedTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsUtilized")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Proxy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Proxy")
                        .IsUnique();

                    b.ToTable("KeyGenModel");

                    b.HasData(
                        new
                        {
                            Id = 2147483647,
                            CreatedTime = new DateTime(2020, 12, 22, 9, 58, 8, 329, DateTimeKind.Local).AddTicks(7491),
                            IsUtilized = false,
                            Proxy = "boEuPq2qe"
                        },
                        new
                        {
                            Id = 2147483646,
                            CreatedTime = new DateTime(2020, 12, 22, 9, 58, 8, 331, DateTimeKind.Local).AddTicks(5094),
                            IsUtilized = false,
                            Proxy = "pIR76TDof"
                        },
                        new
                        {
                            Id = 2147483645,
                            CreatedTime = new DateTime(2020, 12, 22, 9, 58, 8, 331, DateTimeKind.Local).AddTicks(5126),
                            IsUtilized = false,
                            Proxy = "kOd4RqemF"
                        },
                        new
                        {
                            Id = 2147483644,
                            CreatedTime = new DateTime(2020, 12, 22, 9, 58, 8, 331, DateTimeKind.Local).AddTicks(5129),
                            IsUtilized = false,
                            Proxy = "lUts45SD0"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
