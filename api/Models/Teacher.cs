using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public string Family { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int IdTypeEmployment { get; set; }

    public int IdStatusTeacher { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Change> Changes { get; } = new List<Change>();

    public virtual StatusTeacher IdStatusTeacherNavigation { get; set; } = null!;

    public virtual TypeEmployment IdTypeEmploymentNavigation { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; } = new List<Schedule>();
}
