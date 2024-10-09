namespace SampleOneWebAPI.DTOs.Member
{
    public class SignupRequest
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        public string? Gender { get; set; }
    }
}
