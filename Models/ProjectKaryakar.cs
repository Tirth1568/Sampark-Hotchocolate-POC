using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sampark.Models
{
    [Table("project_karyakar")]
    public class ProjectKaryakar
    {
        [Key]
        [Column("project_karyakar_id")]
        public int ProjectKaryakarId { get; set; }

        [Column("project_karyakar_uucode")]
        public Guid ProjectKaryakarUuCode { get; set; }

        [Column("project_id")]
        public int ProjectId { get; set; }

        public int KaryakarPersonId { get; set; }
        public int? CategoryId { get; set; }
        public int? MandalId { get; set; }
        public int? DepartmentId { get; set; }

        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }
        public Person? KaryakarPerson { get; set; }

        [Column("is_active")]
        public bool? Is_Active { get; set; }
    }
}

