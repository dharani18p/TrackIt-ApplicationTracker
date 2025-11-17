namespace ApplicationTrackingSystem.Models
{
    public class ApplicationLog
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }
        public ApplicationModel Application { get; set; }

        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string UpdatedByRole { get; set; }
        public string Comment { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
