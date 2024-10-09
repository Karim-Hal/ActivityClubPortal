using System;
using System.Collections.Generic;

namespace SampleOneWebAPI.Models;

public partial class Member
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public string? MobileNumber { get; set; }

    public string? EmergencyNumber { get; set; }

    public string? Photo { get; set; }

    public string? Profession { get; set; }

    public string? Nationality { get; set; }

    public virtual ICollection<EventMember> EventMembers { get; set; } = new List<EventMember>();

    public virtual ICollection<FeedbackPost> FeedbackPosts { get; set; } = new List<FeedbackPost>();

    public virtual ICollection<MemberPhoto> MemberPhotos { get; set; } = new List<MemberPhoto>();
}
