using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Sampark.Models.Enums.Enums;

namespace Sampark.Models
{
    [Table("project_karyakar_pair")]
    public class ProjectKaryakarPair
    {
        [Key]
        [Column("karyakar_pair_id")]
        public int KaryakarPairId { get; set; }

        [Column("karyakar_pair_uucode")]
        public Guid KaryakarPairUuCode { get; set; }

        [Column("project_id")]
        public int ProjectId { get; set; }

        public int PrimaryKaryakarPersonId { get; set; }
        public int SecondaryKaryakarPersonId { get; set; }

        public PairType PairType { get; set; }

        // Navigation properties
        public Project Project { get; set; }
        public Person? PrimaryKaryakar { get; set; }
        public Person? SecondaryKaryakar { get; set; }
        public ICollection<ProjectFamily> AssignedFamilies { get; set; }
    }
}
