using System;
using System.Collections.Generic;

namespace projectnhom.Entities;

public partial class Contract
{
    public int Id { get; set; }

    public string? NumberContract { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Tel { get; set; } = null!;

    public string? Thumbnail { get; set; }

    public string Cccd { get; set; } = null!;

    public int Status { get; set; }

    public string? Contents { get; set; }

    public DateTime Ngaykyhopdong { get; set; }

    public DateTime Ngaythue { get; set; }

    public DateTime Ngaytra { get; set; }

    public decimal Giatrihopdong { get; set; }

    public decimal Giatridatcoc { get; set; }

    public int CarsId { get; set; }

    public int UsersId { get; set; }

    public virtual Car Cars { get; set; } = null!;

    public virtual User Users { get; set; } = null!;
}
