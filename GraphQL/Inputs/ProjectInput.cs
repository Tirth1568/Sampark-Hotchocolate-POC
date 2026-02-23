using Sampark.Models.Enums;
using static Sampark.Models.Enums.Enums;

namespace Sampark.GraphQL.Inputs
{
    public class ProjectInput
    {
        public int TemplateId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? LocationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Tags { get; set; }
        public ReminderFrequency? ReminderFrequency { get; set; }
        public string? ReminderFrequencyConfig { get; set; }
    }
}
