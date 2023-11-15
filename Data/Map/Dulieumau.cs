using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace MyWebAPI.Data.Map;

[Table("DULIEUMAU")]
public partial class Dulieumau
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("The_geom", TypeName = "geometry")]
    public Geometry? TheGeom { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [NotMapped]
    public double dis_met { get; set; }
}
