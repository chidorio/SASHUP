using System;
using System.Collections.Generic;

namespace api.Models;

public partial class StatusTeacher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Teacher> Teachers { get; } = new List<Teacher>();
}
