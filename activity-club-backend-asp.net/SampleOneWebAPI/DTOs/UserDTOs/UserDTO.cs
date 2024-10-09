namespace SampleOneWebAPI.DTOs.UserDTOs
{
    public class UserDTO
    {

        public string FullName { get; set; } = null!;


        public string? Gender { get; set; }
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
