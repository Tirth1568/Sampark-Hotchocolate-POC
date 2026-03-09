using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sampark.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        [Column("person_id")]
        public int PersonId { get; set; }

        [Column("baps_id")]
        public string? BapsId { get; set; }

        [Column("baps_pid")]
        public double? BapsPid { get; set; }

        [Column("gender")]
        public string? Gender { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Column("last_name")]
        public string? LastName { get; set; }

        [Column("prefix")]
        public string? Prefix { get; set; }

        [Column("suffix")]
        public string? Suffix { get; set; }

        [Column("record_type")]
        public string? RecordType { get; set; }

        [Column("marital_status")]
        public string? MaritalStatus { get; set; }

        [Column("default_mandal_id")]
        public int? DefaultMandalId { get; set; }

        [Column("department_id")]
        public int? DepartmentId { get; set; }

        [Column("default_mandal_name")]
        public string? DefaultMandalName { get; set; }

        [Column("home_phone_masked")]
        public string? HomePhoneMasked { get; set; }

        [Column("cellphone_masked")]
        public string? CellphoneMasked { get; set; }

        [Column("email_masked")]
        public string? EmailMasked { get; set; }

        [Column("address_masked")]
        public string? AddressMasked { get; set; }

        [Column("state_code")]
        public string? StateCode { get; set; }

        [Column("country_name")]
        public string? CountryName { get; set; }

        [Column("relation")]
        public string? Relation { get; set; }

        [Column("seva_category")]
        public string? SevaCategory { get; set; }

        [Column("save_category_code")]
        public string? SaveCategoryCode { get; set; }

        [Column("is_primary")]
        public bool? IsPrimary { get; set; }

        [Column("family_id")]
        public int? FamilyId { get; set; }

        [Column("relative_type_id")]
        public int? RelativeTypeId { get; set; }

        [Column("relative_type_name")]
        public string? RelativeTypeName { get; set; }

        [Column("region_id")]
        public int? RegionId { get; set; }

        [Column("region_name")]
        public string? RegionName { get; set; }

        [Column("entity_id")]
        public int? EntityId { get; set; }

        public Entity? Entity { get; set; }

        [Column("center_name")]
        public string? CenterName { get; set; }

        [Column("uuid")]
        public string? Uuid { get; set; }

        [Column("business_name")]
        public string? BusinessName { get; set; }

        [Column("legal_business_name")]
        public string? LegalBusinessName { get; set; }

        [Column("master_entity_id")]
        public int? MasterEntityId { get; set; }

        [Column("work_phone")]
        public string? WorkPhone { get; set; }

        [Column("work_email")]
        public string? WorkEmail { get; set; }

        [Column("zone")]
        public string? Zone { get; set; }

        [Column("division_id")]
        public int? DivisionId { get; set; }

        [Column("division")]
        public string? Division { get; set; }

        [Column("primary_person_id")]
        public int? PrimaryPersonId { get; set; }

        [Column("bkms_id")]
        public string? BkmsId { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public string? CreatedAt { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("updated_at")]
        public string? UpdatedAt { get; set; }

        [Column("updated_by")]
        public string? UpdatedBy { get; set; }

        [Column("full_name")]
        public string? FullName { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("city")]
        public string? City { get; set; }

        [Column("state")]
        public string? State { get; set; }

        [Column("home_phone")]
        public string? HomePhone { get; set; }

        [Column("cellphone")]
        public string? Cellphone { get; set; }

        [Column("zipcode")]
        public string? Zipcode { get; set; }

        [Column("receipt_email_list")]
        public string? ReceiptEmailList { get; set; }

        [Column("person_status")]
        public string? PersonStatus { get; set; }
    }
}

