using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Group
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdStatusGroup { get; set; }

    public virtual ICollection<Change> Changes { get; } = new List<Change>();

    public virtual StatusGroup IdStatusGroupNavigation { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();
}
