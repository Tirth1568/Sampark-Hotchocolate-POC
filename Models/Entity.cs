using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sampark.Models
{
    [Table("entities")]
    public class Entity
{
    [Key]
    [Column("entity_id")]
    public int EntityId { get; set; }

    public int? division_id { get; set; }
    public string? code { get; set; }
    public string? name { get; set; }

    public int? parent_entity_id { get; set; }
    public int? division_geo_level_id { get; set; }

    public string? phone { get; set; }
    public string? email { get; set; }
    public string? uuid { get; set; }

    public int is_active { get; set; }

    public string? created_at { get; set; }
    public string? created_by { get; set; }
    public string? updated_at { get; set; }
    public int? updated_by { get; set; }
        public Entity? Parent { get; set; }
        public ICollection<Entity> Children { get; set; } = new List<Entity>();
    }
}
