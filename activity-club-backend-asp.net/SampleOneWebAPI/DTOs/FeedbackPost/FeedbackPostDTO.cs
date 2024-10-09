namespace SampleOneWebAPI.DTOs.FeedbackPost
{
    public class FeedbackPostDTO
    {
        public string MemberName { get; set; } = null!;

        public string Description { get; set; } = null!;


        public DateOnly? CreatedDate { get; set; }
        public string Photo { get; set; } = null!;
    }
}
