using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhomnetapi.Entities;

namespace nhomnetapi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : Controller
    {
        private readonly T22netContext _context;
        public CategoriesController(T22netContext context)
        {
            _context = context;
        }
        [HttpGet]
        //[Authorize(Policy = "QA", Roles ="Staff")]
        public IActionResult Index()
        {
            var categories = _context.Categories
               .ToList<Category>();
            return Ok(categories);
        }

        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories.Where(c => c.Id == id)
                .Include(category => category.Products)
                .First();
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Created($"/get-by-id?id={category.Id}", category);
        }

        [HttpPut]
        public IActionResult Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var categoryDelete = _context.Categories.Find(id);
            if (categoryDelete == null)
                return NotFound();
            _context.Categories.Remove(categoryDelete);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
