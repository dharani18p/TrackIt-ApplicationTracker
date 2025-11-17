namespace ApplicationTrackingSystem.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Applicant, Admin, BotMimic
    }
}
