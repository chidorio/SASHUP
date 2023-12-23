using System;
using System.Collections.Generic;

namespace api.Models;

public partial class StatusGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; } = new List<Group>();
}
