using System;
using System.Collections.Generic;

namespace TodoProject.Models;

public partial class RolePermission
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }
}
