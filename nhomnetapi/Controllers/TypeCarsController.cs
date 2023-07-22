using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using nhomnetapi.Entities;

namespace nhomnetapi.Controllers
{
    [ApiController]
    [Route("api/typecars")]
    
    public class TypeCarsController : Controller
    {
        private readonly T22netContext _context;
        public TypeCarsController(T22netContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var typecars = _context.TypeCars.Include(t=>t.Cars).ToList();
            return Ok(typecars);
        }
        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var type = _context.TypeCars.Include(type => type.Cars).Where(t => t.Id == id).First();
            if (type == null) return NotFound();
            return Ok(type);
        }
        [HttpPost]
        public IActionResult Create(TypeCar type)
        {
            _context.TypeCars.Add(type);
            _context.SaveChanges();
            return Created($"/get-by-id?id={type.Id}", type);
        }
        [HttpPut]
        public IActionResult Update(TypeCar type)
        {
            _context.TypeCars.Update(type);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var type = _context.TypeCars.Find(id);
            if (type == null) return NotFound();
            _context.TypeCars.Remove(type);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
