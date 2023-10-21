using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Data.Map;
using NetTopologySuite.Features;
using NetTopologySuite.IO;

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
            IQueryable<Dulieumau> data;
            if (q == "ALL")
            {
                data = _context.Dulieumaus;
            }
            else
            {
                data = _context.Dulieumaus.Where(e => e.Id.ToString() == q);
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
    }
}