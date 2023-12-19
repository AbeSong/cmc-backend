using System;
using System.Collections.Generic;

namespace TodoProject.Models;

public partial class UserProfile
{
    public int UserProfileId { get; set; }

    public int RoleId { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string ModifiedBy { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();
}
