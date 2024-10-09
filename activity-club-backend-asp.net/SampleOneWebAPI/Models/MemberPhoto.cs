using System;
using System.Collections.Generic;

namespace SampleOneWebAPI.Models;

public partial class MemberPhoto
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public byte[]? Photo { get; set; }

    public virtual Member Member { get; set; } = null!;
}
