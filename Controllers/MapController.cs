using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebAPI.Data.Map;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System.Data;
using System.Runtime.CompilerServices;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        public MapController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet("Dulieumau")]
        public IActionResult Dulieumau(string q)
        {
            List<Dulieumau> data;
            if (q == "ALL")
            {
                data = _context.Dulieumaus.Select(e => new Dulieumau
                {
                    Id = e.Id,
                    TheGeom = e.TheGeom,
                    Name = e.Name
                }).ToList();
            }
            else
            {
                data = _context.Dulieumaus.Where(e => e.Id.ToString() == q).Select(e => new Dulieumau
                {
                    Id = e.Id,
                    TheGeom = e.TheGeom,
                    Name = e.Name
                }).ToList();
            }

            var featureCollection = new FeatureCollection();

            foreach (var item in data)
            {
                var attr = new AttributesTable();
                attr.Add("name", item.Name);
                attr.Add("id", item.Id);
                var feature = new Feature(item.TheGeom, attr);
                featureCollection.Add(feature);
            }
            var w = new GeoJsonWriter();
            return Ok(w.Write(featureCollection));
        }

        [HttpGet("Khoangcach")]
        public IActionResult Khoangcach(string q)
        {
            var sql = FormattableStringFactory.Create(q);
            var data = _context.Dulieumaus.FromSql(sql).ToList();

            var featureCollection = new FeatureCollection();

            foreach (var item in data)
            {
                var attr = new AttributesTable();
                attr.Add("name", item.Name);
                attr.Add("id", item.Id);
                attr.Add("dis_met", item.dis_met);
                var feature = new Feature(item.TheGeom, attr);
                featureCollection.Add(feature);
            }
            var w = new GeoJsonWriter();
            return Ok(w.Write(featureCollection));
        }

        [HttpPost("saveFeature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult saveFeature([FromForm] string q, [FromForm] string? name)
        {
            try
            {
                var geoReader = new GeoJsonReader();
                var geometry = geoReader.Read<Geometry>(q);

                var model = new Dulieumau
                {
                    TheGeom = geometry,
                    Name = name
                };
                _context.Dulieumaus.Add(model);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("updateFeature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult updateFeature([FromForm] int id, [FromForm] string? name)
        {
            try
            {
                var model = _context.Dulieumaus.Find(id)!;
                model.Name = name;

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("searchFeature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult searchFeature(string? name)
        {
            var data = _context.Dulieumaus.Where(x => x.Name!.Contains(name!)).ToList();

            var featureCollection = new FeatureCollection();

            foreach (var item in data)
            {
                var attr = new AttributesTable();
                attr.Add("name", item.Name);
                attr.Add("id", item.Id);
                var feature = new Feature(item.TheGeom, attr);
                featureCollection.Add(feature);
            }
            var w = new GeoJsonWriter();
            return Ok(w.Write(featureCollection));
        }
    }
}