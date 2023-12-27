﻿using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Change
{
    public int Id { get; set; }

    public int IdTeacher { get; set; }

    public int IdGroup { get; set; }

    public DateTime? Date { get; set; }

    public int IdSubject { get; set; }

    public int IdNumberPair { get; set; }

    public int IdCabinets { get; set; }

    public virtual Cabinet IdCabinetsNavigation { get; set; } = null!;

    public virtual Group IdGroupNavigation { get; set; } = null!;

    public virtual NumberPair IdNumberPairNavigation { get; set; } = null!;

    public virtual Subject IdSubjectNavigation { get; set; } = null!;

    public virtual Teacher IdTeacherNavigation { get; set; } = null!;
}
