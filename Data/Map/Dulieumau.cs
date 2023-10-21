using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace MyWebAPI.Data.Map;

public partial class Dulieumau
{
    public int Id { get; set; }

    public Geometry? TheGeom { get; set; }

    public string? Name { get; set; }
}
