using System;
using System.Collections.Generic;

namespace SampleOneWebAPI.Models;

public partial class FeedbackPost
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int MemberId { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public virtual Member Member { get; set; } = null!;
}
