namespace SampleOneWebAPI.DTOs.Event
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Destination { get; set; }

        public DateOnly? DateFrom { get; set; }

        public DateOnly? DateTo { get; set; }

        public decimal? Cost { get; set; }

        public string? Status { get; set; }

        public int CategoryId { get; set; }
    }
}
