using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sampark.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "entities",
            //    columns: table => new
            //    {
            //        entity_id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        DivisionId = table.Column<int>(type: "integer", nullable: true),
            //        Code = table.Column<string>(type: "text", nullable: false),
            //        Name = table.Column<string>(type: "text", nullable: false),
            //        ParentEntityId = table.Column<int>(type: "integer", nullable: true),
            //        DivisionGeoLevelId = table.Column<int>(type: "integer", nullable: true),
            //        Phone = table.Column<string>(type: "text", nullable: false),
            //        Email = table.Column<string>(type: "text", nullable: false),
            //        Uuid = table.Column<string>(type: "text", nullable: false),
            //        IsActive = table.Column<bool>(type: "boolean", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            //        CreatedBy = table.Column<string>(type: "text", nullable: false),
            //        UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            //        UpdatedBy = table.Column<int>(type: "integer", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_entities", x => x.entity_id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Person",
            //    columns: table => new
            //    {
            //        person_id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        BapsId = table.Column<string>(type: "text", nullable: false),
            //        Gender = table.Column<string>(type: "text", nullable: false),
            //        FirstName = table.Column<string>(type: "text", nullable: false),
            //        MiddleName = table.Column<string>(type: "text", nullable: false),
            //        LastName = table.Column<string>(type: "text", nullable: false),
            //        FullName = table.Column<string>(type: "text", nullable: false),
            //        FamilyId = table.Column<int>(type: "integer", nullable: true),
            //        PrimaryPersonId = table.Column<int>(type: "integer", nullable: true),
            //        EntityId = table.Column<int>(type: "integer", nullable: true),
            //        DivisionId = table.Column<int>(type: "integer", nullable: true),
            //        Email = table.Column<string>(type: "text", nullable: false),
            //        Cellphone = table.Column<string>(type: "text", nullable: false),
            //        Address = table.Column<string>(type: "text", nullable: false),
            //        City = table.Column<string>(type: "text", nullable: false),
            //        State = table.Column<string>(type: "text", nullable: false),
            //        IsActive = table.Column<bool>(type: "boolean", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Person", x => x.person_id);
            //    });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    project_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_uucode = table.Column<Guid>(type: "uuid", nullable: false),
                    template_id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    location_id = table.Column<int>(type: "integer", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Tags = table.Column<string>(type: "jsonb", nullable: false),
                    ReminderFrequency = table.Column<string>(type: "text", nullable: false),
                    ReminderFrequencyConfig = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.project_id);
                });

            migrationBuilder.CreateTable(
                name: "project_karyakar",
                columns: table => new
                {
                    project_karyakar_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_karyakar_uucode = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    KaryakarPersonId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    MandalId = table.Column<int>(type: "integer", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_karyakar", x => x.project_karyakar_id);
                    table.ForeignKey(
                        name: "FK_project_karyakar_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_karyakar_pair",
                columns: table => new
                {
                    karyakar_pair_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    karyakar_pair_uucode = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    PrimaryKaryakarPersonId = table.Column<int>(type: "integer", nullable: false),
                    SecondaryKaryakarPersonId = table.Column<int>(type: "integer", nullable: false),
                    PairType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_karyakar_pair", x => x.karyakar_pair_id);
                    table.ForeignKey(
                        name: "FK_project_karyakar_pair_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_family",
                columns: table => new
                {
                    project_family_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_family_uucode = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    PersonId = table.Column<int>(type: "integer", nullable: false),
                    PrimaryPersonId = table.Column<int>(type: "integer", nullable: false),
                    FamilyId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true),
                    MandalId = table.Column<int>(type: "integer", nullable: true),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true),
                    assigned_karyakar_pair_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_family", x => x.project_family_id);
                    table.ForeignKey(
                        name: "FK_project_family_project_karyakar_pair_assigned_karyakar_pair~",
                        column: x => x.assigned_karyakar_pair_id,
                        principalTable: "project_karyakar_pair",
                        principalColumn: "karyakar_pair_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_project_family_project_project_id",
                        column: x => x.project_id,
                        principalTable: "project",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_family_assigned_karyakar_pair_id",
                table: "project_family",
                column: "assigned_karyakar_pair_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_family_project_id",
                table: "project_family",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_karyakar_project_id",
                table: "project_karyakar",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_karyakar_pair_project_id",
                table: "project_karyakar_pair",
                column: "project_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entities");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "project_family");

            migrationBuilder.DropTable(
                name: "project_karyakar");

            migrationBuilder.DropTable(
                name: "project_karyakar_pair");

            migrationBuilder.DropTable(
                name: "project");
        }
    }
}
