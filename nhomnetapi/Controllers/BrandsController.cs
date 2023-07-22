using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhomnetapi.Entities;

namespace nhomnetapi.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandsController : Controller
    {
        private readonly T22netContext _context;
        public BrandsController(T22netContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var brands = _context.Brands.Include(b => b.Cars).ToList();
            return Ok(brands);
        }
        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var brand = _context.Brands.Where(b =>b.Id == id).Include(brand => brand.Cars).First();
            if (brand == null) return NotFound();
            return Ok(brand);
        }
        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return Created($"/get-by-id?id={brand.Id}", brand);
        }
        [HttpPut]
        public IActionResult Update(Brand brand)
        {
            _context.Brands.Update(brand);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var brand = _context.Brands.Find(id);
            if (brand == null) return NotFound();
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
