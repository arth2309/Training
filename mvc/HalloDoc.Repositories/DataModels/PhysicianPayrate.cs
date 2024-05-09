﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Repositories.DataModels;

[Table("PhysicianPayrate")]
public partial class PhysicianPayrate
{
    [Key]
    public int PayrateId { get; set; }

    public int? PhysicianId { get; set; }

    public int? NightShiftWeekend { get; set; }

    public int? Shift { get; set; }

    public int? HouseCallsNightWeekend { get; set; }

    public int? PhoneConsults { get; set; }

    public int? PhoneConsultsNightWeekend { get; set; }

    public int? BatchTesting { get; set; }

    public int? HouseCalls { get; set; }
}
