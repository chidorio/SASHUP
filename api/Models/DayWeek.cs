using System;
using System.Collections.Generic;

namespace api.Models;

public partial class DayWeek
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();
}
