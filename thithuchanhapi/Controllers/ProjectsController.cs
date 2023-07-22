using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thithuchanhapi.Models;


namespace thithuchanhapi.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        public readonly DataContext _context;
        public ProjectsController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var pro = _context.Projects.Include(e => e.ProjectEmployees).ToList();
            return Ok(pro);

        }
        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var pro = _context.Projects.Include(e => e.ProjectEmployees).Where(e => e.ProjectId == id).First();
            if (pro == null) return NotFound();
            return Ok(pro);
        }
        [HttpPost]
        public IActionResult Create(Project pro)
        {
            _context.Projects.Add(pro);
            _context.SaveChanges();
            return Created($"/get-by-id?id={pro.ProjectId}", pro);
        }
        [HttpPut]
        public IActionResult Update(Project pro)
        {
            _context.Projects.Update(pro);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var e = _context.Projects.Find(id);
            if (e == null) return NotFound();
            _context.Projects.Remove(e);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string? name, int? limit, int? page)
        {
            limit = limit != null ? limit : 20;
            page = page != null ? page : 1;
            int offset = (int)((page - 1) * limit);
            
            var pro = _context.Projects.Where(p => p.ProjectName.Contains(name)).Where(p => DateTime.Compare((DateTime)p.ProjectEndDate, DateTime.UtcNow) > 0)
             .Skip(offset).Take((int)limit).ToArray();
            return Ok(pro);

        }
    }
}
