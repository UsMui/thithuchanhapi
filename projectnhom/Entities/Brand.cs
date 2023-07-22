using System;
using System.Collections.Generic;

namespace projectnhom.Entities;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Thumbnail { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
