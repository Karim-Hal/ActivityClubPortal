namespace SampleOneWebAPI.DTOs.Guide
{
    public class GuideDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public DateOnly? JoiningDate { get; set; }

        public byte[] Photo { get; set; } = null!;
        public string PhotoBase64 { get; set; } = null!;
        public string? Profession { get; set; }
    }
}
