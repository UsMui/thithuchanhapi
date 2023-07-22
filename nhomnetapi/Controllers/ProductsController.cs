using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhomnetapi.Entities;

namespace nhomnetapi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly T22netContext _context;
        public ProductsController(T22netContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Category).ToArray();
            return Ok(products);
        }
        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return Ok(product);

        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Created($"/get-by-id?id={product.Id}", product);
        }

        [HttpPut]
        public IActionResult Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productDelete = _context.Products.Find(id);
            if (productDelete == null)
                return NotFound();
            _context.Products.Remove(productDelete);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string? q, int? limit, int? page, [FromHeader] int userId)
        {
            limit = limit != null ? limit : 10;
            page = page != null ? page : 1;
            int offset = (int)((page - 1) * limit);
            var products = _context.Products.Where(p => p.Name.Contains(q))
                .Skip(offset).Take((int)limit).ToArray();
            return Ok(products);
        }
    }
}
