using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "doggo");

            migrationBuilder.CreateTable(
                name: "breeds",
                schema: "doggo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    breed_type = table.Column<int>(type: "integer", nullable: false),
                    family = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    origin = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: true),
                    last_update_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    last_update_by = table.Column<string>(type: "character varying(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_breeds", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_breeds_external_id",
                schema: "doggo",
                table: "breeds",
                column: "external_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "breeds",
                schema: "doggo");
        }
    }
}
