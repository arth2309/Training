using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystemRepo.DataModels;

[Table("Project")]
public partial class Project
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string ProjectName { get; set; } = null!;

    [StringLength(256)]
    public string? Assignee { get; set; }

    public int DomainId { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    [StringLength(256)]
    public string? Description { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? DueDate { get; set; }

    [StringLength(256)]
    public string? Domain { get; set; }

    [StringLength(256)]
    public string? City { get; set; }

    [ForeignKey("DomainId")]
    [InverseProperty("Projects")]
    public virtual Domain DomainNavigation { get; set; } = null!;
}
