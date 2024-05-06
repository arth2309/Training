using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Repositories.DataModels;

[Table("Reimbursement")]
public partial class Reimbursement
{
    [Key]
    public int ReimbursementId { get; set; }

    public int? InvoiceDetailsId { get; set; }

    [StringLength(100)]
    public string? Item { get; set; }

    public int? Amount { get; set; }

    [StringLength(128)]
    public string? File { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Date { get; set; }

    public int? PhysicianId { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }
}
