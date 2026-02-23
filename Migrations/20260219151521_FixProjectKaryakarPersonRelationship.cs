using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sampark.Migrations
{
    /// <inheritdoc />
    public partial class FixProjectKaryakarPersonRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddForeignKey(
                name: "FK_entities_entities_parent_entity_id",
                table: "entities",
                column: "parent_entity_id",
                principalTable: "entities",
                principalColumn: "entity_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_project_karyakar_Person_KaryakarPersonId",
                table: "project_karyakar",
                column: "KaryakarPersonId",
                principalTable: "Person",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_entities_entities_parent_entity_id",
                table: "entities");

            migrationBuilder.DropForeignKey(
                name: "FK_project_karyakar_Person_KaryakarPersonId",
                table: "project_karyakar");


        }
    }
}
