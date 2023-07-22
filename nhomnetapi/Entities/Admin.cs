using System;
using System.Collections.Generic;

namespace nhomnetapi.Entities;

public partial class Admin
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
}
