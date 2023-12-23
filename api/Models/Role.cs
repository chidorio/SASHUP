using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Autorization> Autorizations { get; } = new List<Autorization>();
}
