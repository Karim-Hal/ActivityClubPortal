using System;
using System.Collections.Generic;

namespace SampleOneWebAPI.Models;

public partial class Event
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

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<EventGuide> EventGuides { get; set; } = new List<EventGuide>();

    public virtual ICollection<EventMember> EventMembers { get; set; } = new List<EventMember>();
}
