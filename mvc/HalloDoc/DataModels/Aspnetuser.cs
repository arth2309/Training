using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.DataModels;

public partial class AspNetUser
{
    [Key]
    [StringLength(128)]
    public string Id { get; set; } = null!;

    [StringLength(256)]
    public string Username { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? PasswordHash { get; set; }

    [Column(TypeName = "character varying")]
    public string? SecurityStamp { get; set; }

    [StringLength(256)]
    public string? Email { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray EmailConfirmed { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? PhoneNumber { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray PhoneNumberConfirmed { get; set; } = null!;

    [Column(TypeName = "bit(1)")]
    public BitArray TwoFactorEnabled { get; set; } = null!;

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? LockoutEndDateUtc { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray LockoutEnabled { get; set; } = null!;

    public int AccessFailedCount { get; set; }

    [Column("IP")]
    [StringLength(20)]
    public string? Ip { get; set; }

    [Column(TypeName = "character varying")]
    public string? CorePasswordHash { get; set; }

    public int? HashVersion { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    [InverseProperty("AspNetUser")]
    public virtual ICollection<Admin> AdminAspNetUsers { get; set; } = new List<Admin>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Admin> AdminModifiedByNavigations { get; set; } = new List<Admin>();

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; } = new List<AspNetUserRole>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Business> BusinessCreatedByNavigations { get; set; } = new List<Business>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Business> BusinessModifiedByNavigations { get; set; } = new List<Business>();

    [InverseProperty("AspNetUser")]
    public virtual ICollection<Physician> PhysicianAspNetUsers { get; set; } = new List<Physician>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Physician> PhysicianCreatedByNavigations { get; set; } = new List<Physician>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<Physician> PhysicianModifiedByNavigations { get; set; } = new List<Physician>();

    [InverseProperty("ModifiedByNavigation")]
    public virtual ICollection<ShiftDetail> ShiftDetails { get; set; } = new List<ShiftDetail>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();

    [InverseProperty("AspNetUser")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
