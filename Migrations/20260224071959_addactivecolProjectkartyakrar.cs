using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sampark.Migrations
{
    /// <inheritdoc />
    public partial class addactivecolProjectkartyakrar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Person",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Person",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Person",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Person",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "Cellphone",
                table: "Person",
                newName: "cellphone");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Person",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "PrimaryPersonId",
                table: "Person",
                newName: "primary_person_id");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Person",
                newName: "middle_name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Person",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Person",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Person",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Person",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "FamilyId",
                table: "Person",
                newName: "family_id");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "Person",
                newName: "entity_id");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "Person",
                newName: "division_id");

            migrationBuilder.RenameColumn(
                name: "BapsId",
                table: "Person",
                newName: "baps_id");

            migrationBuilder.AddColumn<bool>(
                name: "Is_Active",
                table: "project_karyakar",
                type: "boolean",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "state",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "city",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "cellphone",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "middle_name",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "baps_id",
                table: "Person",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "address_masked",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "baps_pid",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bkms_id",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "business_name",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "cellphone_masked",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "center_name",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country_name",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Person",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "default_mandal_id",
                table: "Person",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "default_mandal_name",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "department_id",
                table: "Person",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "division",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email_masked",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "home_phone",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "home_phone_masked",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_primary",
                table: "Person",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "legal_business_name",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "marital_status",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "master_entity_id",
                table: "Person",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "person_status",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "prefix",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "receipt_email_list",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "record_type",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "region_id",
                table: "Person",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "region_name",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "relation",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "relative_type_id",
                table: "Person",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "relative_type_name",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "save_category_code",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "seva_category",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "state_code",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "suffix",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Person",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uuid",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "work_email",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "work_phone",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zipcode",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zone",
                table: "Person",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_karyakar_pair_PrimaryKaryakarPersonId",
                table: "project_karyakar_pair",
                column: "PrimaryKaryakarPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_project_karyakar_pair_SecondaryKaryakarPersonId",
                table: "project_karyakar_pair",
                column: "SecondaryKaryakarPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_project_family_MandalId",
                table: "project_family",
                column: "MandalId");

            migrationBuilder.AddForeignKey(
                name: "FK_project_family_entities_MandalId",
                table: "project_family",
                column: "MandalId",
                principalTable: "entities",
                principalColumn: "entity_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_karyakar_pair_Person_PrimaryKaryakarPersonId",
                table: "project_karyakar_pair",
                column: "PrimaryKaryakarPersonId",
                principalTable: "Person",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_project_karyakar_pair_Person_SecondaryKaryakarPersonId",
                table: "project_karyakar_pair",
                column: "SecondaryKaryakarPersonId",
                principalTable: "Person",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_family_entities_MandalId",
                table: "project_family");

            migrationBuilder.DropForeignKey(
                name: "FK_project_karyakar_pair_Person_PrimaryKaryakarPersonId",
                table: "project_karyakar_pair");

            migrationBuilder.DropForeignKey(
                name: "FK_project_karyakar_pair_Person_SecondaryKaryakarPersonId",
                table: "project_karyakar_pair");

            migrationBuilder.DropIndex(
                name: "IX_project_karyakar_pair_PrimaryKaryakarPersonId",
                table: "project_karyakar_pair");

            migrationBuilder.DropIndex(
                name: "IX_project_karyakar_pair_SecondaryKaryakarPersonId",
                table: "project_karyakar_pair");

            migrationBuilder.DropIndex(
                name: "IX_project_family_MandalId",
                table: "project_family");

            migrationBuilder.DropColumn(
                name: "Is_Active",
                table: "project_karyakar");

            migrationBuilder.DropColumn(
                name: "address_masked",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "baps_pid",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "bkms_id",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "business_name",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "cellphone_masked",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "center_name",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "country_name",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "default_mandal_id",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "default_mandal_name",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "department_id",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "division",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "email_masked",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "home_phone",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "home_phone_masked",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "is_primary",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "legal_business_name",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "marital_status",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "master_entity_id",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "person_status",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "prefix",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "receipt_email_list",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "record_type",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "region_id",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "region_name",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "relation",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "relative_type_id",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "relative_type_name",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "save_category_code",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "seva_category",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "state_code",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "suffix",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "uuid",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "work_email",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "work_phone",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "zipcode",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "zone",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "Person",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Person",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Person",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Person",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "cellphone",
                table: "Person",
                newName: "Cellphone");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Person",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "primary_person_id",
                table: "Person",
                newName: "PrimaryPersonId");

            migrationBuilder.RenameColumn(
                name: "middle_name",
                table: "Person",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Person",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Person",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "Person",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Person",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "family_id",
                table: "Person",
                newName: "FamilyId");

            migrationBuilder.RenameColumn(
                name: "entity_id",
                table: "Person",
                newName: "EntityId");

            migrationBuilder.RenameColumn(
                name: "division_id",
                table: "Person",
                newName: "DivisionId");

            migrationBuilder.RenameColumn(
                name: "baps_id",
                table: "Person",
                newName: "BapsId");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cellphone",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BapsId",
                table: "Person",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
