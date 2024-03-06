using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Repositories.DataModels;

[Keyless]
public partial class AspNetUserRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    public virtual AspNetRole Role { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual AspNetUser User { get; set; } = null!;
}
