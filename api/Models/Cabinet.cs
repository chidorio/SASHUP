using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Cabinet
{
    public int Id { get; set; }

    public int Number { get; set; }

    public string? Name { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Change> Changes { get; } = new List<Change>();

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();
}
