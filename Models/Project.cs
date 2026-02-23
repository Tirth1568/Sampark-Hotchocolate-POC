using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sampark.Models.Enums;
using static Sampark.Models.Enums.Enums;

namespace Sampark.Models
{
    [Table("project")]
    public class Project
    {
        [Key]
        [Column("project_id")]
        public int ProjectId { get; set; }

        [Column("project_uucode")]
        public Guid ProjectUuCode { get; set; }

        [Column("template_id")]
        public int TemplateId { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }

        [Column("location_id")]
        public int? LocationId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "jsonb")]
        public string? Tags { get; set; }

        public ReminderFrequency? ReminderFrequency { get; set; }

        [Column(TypeName = "jsonb")]
        public string? ReminderFrequencyConfig { get; set; }

        // Navigation
        public ICollection<ProjectKaryakar> Karyakars { get; set; }
        public ICollection<ProjectKaryakarPair> KaryakarPairs { get; set; }
        public ICollection<ProjectFamily> Families { get; set; }
    }
}
