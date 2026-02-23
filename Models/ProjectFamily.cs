using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sampark.Models
{
    [Table("project_family")]
    public class ProjectFamily
    {
        [Key]
        [Column("project_family_id")]
        public int ProjectFamilyId { get; set; }

        [Column("project_family_uucode")]
        public Guid ProjectFamilyUuCode { get; set; }

        [Column("project_id")]
        public int ProjectId { get; set; }

        public int PersonId { get; set; }
        public int PrimaryPersonId { get; set; }
        public int FamilyId { get; set; }

        public int? CategoryId { get; set; }
        public int? MandalId { get; set; }
        public int? DepartmentId { get; set; }

        [Column("assigned_karyakar_pair_id")]
        public int? AssignedKaryakarPairId { get; set; }

        public Project Project { get; set; }
        public ProjectKaryakarPair AssignedKaryakarPair { get; set; }
        public Entity? Mandal { get; set; }
    }
}
