using System;
using System.Collections.Generic;

namespace t22netapi.Entities;

public partial class Exam
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public TimeSpan Starttime { get; set; }

    public DateTime Examdate { get; set; }

    public int Examduration { get; set; }

    public int? Classroomid { get; set; }

    public int? Subjectid { get; set; }

    public int? Teacherid { get; set; }

    public string? Status { get; set; }

    public virtual Classroom? Classroom { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
