namespace ApplicationTrackingSystem.Models
{
    public class ApplicationModel
    {
        public int Id { get; set; }

        public int ApplicantId { get; set; }
        public User Applicant { get; set; }

        public int JobRoleId { get; set; }
        public JobRole JobRole { get; set; }

        public string Status { get; set; } = "Applied";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
