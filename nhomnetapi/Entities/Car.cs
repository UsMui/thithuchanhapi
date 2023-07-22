using System;
using System.Collections.Generic;

namespace nhomnetapi.Entities;

public partial class Car
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Status { get; set; }

    public int? Namsanxuat { get; set; }

    public string? Description { get; set; }

    public decimal Giathue1ngay { get; set; }

    public string Thumbnail { get; set; } = null!;

    public string Bienso { get; set; } = null!;

    public string? Sokhung { get; set; }

    public string? Somay { get; set; }

    public int Sochongoi { get; set; }

    public int BrandId { get; set; }

    public int TypeCarId { get; set; }

    public virtual Brand? Brand { get; set; } = null!;

    public virtual ICollection<Contract>? Contracts { get; set; } = new List<Contract>();

    public virtual TypeCar? TypeCar { get; set; } = null!;
}
