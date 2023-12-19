using System;
using System.Collections.Generic;

namespace TodoProject.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}
