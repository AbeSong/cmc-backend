using System;
using System.Collections.Generic;

namespace TodoProject.Models;

public partial class Todo
{
    public int TodoId { get; set; }

    public int UserProfileId { get; set; }

    public string Description { get; set; } = null!;

    public bool IsComplete { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public virtual UserProfile UserProfile { get; set; } = null!;
}
