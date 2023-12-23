using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Date
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Change> Changes { get; } = new List<Change>();
}
