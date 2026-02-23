namespace Sampark.GraphQL.Inputs
{
    public class EntityInput
    {
        public int? DivisionId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int? ParentEntityId { get; set; }
        public int? DivisionGeoLevelId { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int IsActive { get; set; }
    }
}
