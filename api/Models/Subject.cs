using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Change> Changes { get; } = new List<Change>();

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();
}
