﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.DataModels;

[Table("PhysicianRegion")]
public partial class PhysicianRegion
{
    [Key]
    public int PhysicianRegionId { get; set; }

    public int? PhysicianId { get; set; }

    public int? RegionId { get; set; }

    [ForeignKey("PhysicianId")]
    [InverseProperty("PhysicianRegions")]
    public virtual Physician? Physician { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("PhysicianRegions")]
    public virtual Region? Region { get; set; }
}
