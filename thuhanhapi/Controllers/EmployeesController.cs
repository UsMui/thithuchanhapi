using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using thithuchanhapi.Models;

namespace thithuchanhapi.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public readonly DataContext _context;
        public EmployeesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _context.Employees.Include(e=>e.ProjectEmployees).ToList();
            return Ok(employees);

        }
        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var empl = _context.Employees.Include(e=>e.ProjectEmployees).Where(e=>e.EmployeeId==id).First();
            if (empl == null) return NotFound();
            return Ok(empl);
        }
        [HttpPost]
        public IActionResult Create(Employee empl)
        {
            _context.Employees.Add(empl);
            _context.SaveChanges();
            return Created($"/get-by-id?id={empl.EmployeeId}", empl);
        }
        [HttpPut]
        public IActionResult Update(Employee empl)
        {
            _context.Employees.Update(empl);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var e = _context.Employees.Find(id);
            if (e == null) return NotFound();
            _context.Employees.Remove(e);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search( string? name, int? limit, int? page)
        {
            limit = limit != null ? limit : 20;
            page = page != null ? page : 1;
            int offset = (int)((page - 1) * limit);
          
            var em= _context.Employees.Where(p => p.EmployeeName.Contains(name))
             .Skip(offset).Take((int)limit).ToArray();
            return Ok(em);

        }

        

    }
}
