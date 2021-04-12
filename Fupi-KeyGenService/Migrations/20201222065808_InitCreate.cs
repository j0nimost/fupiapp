using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fupi_KeyGenService.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KeyGenModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Proxy = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    IsUtilized = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyGenModel", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "KeyGenModel",
                columns: new[] { "Id", "CreatedTime", "Proxy" },
                values: new object[,]
                {
                    { 2147483647, new DateTime(2020, 12, 22, 9, 58, 8, 329, DateTimeKind.Local).AddTicks(7491), "boEuPq2qe" },
                    { 2147483646, new DateTime(2020, 12, 22, 9, 58, 8, 331, DateTimeKind.Local).AddTicks(5094), "pIR76TDof" },
                    { 2147483645, new DateTime(2020, 12, 22, 9, 58, 8, 331, DateTimeKind.Local).AddTicks(5126), "kOd4RqemF" },
                    { 2147483644, new DateTime(2020, 12, 22, 9, 58, 8, 331, DateTimeKind.Local).AddTicks(5129), "lUts45SD0" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyGenModel_Proxy",
                table: "KeyGenModel",
                column: "Proxy",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyGenModel");
        }
    }
}
