using System;
using System.Collections.Generic;

namespace t22netapi.Entities;

public partial class Teacher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
