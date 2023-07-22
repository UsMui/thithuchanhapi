using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using nhomnetapi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Dynamic.Core;




namespace nhomnetapi.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarsController : Controller
    {
        private readonly T22netContext _context;
        public CarsController(T22netContext context)
        {
            _context = context;
        }


        [HttpGet]

        //[Authorize(Policy = "QA")]

        public IActionResult Index(int? limit, int? page, [FromHeader] int userId)
        {
            limit = limit != null ? limit : 20;
            page = page != null ? page : 1;
            int offset = (int)((page - 1) * limit);
            var cars = _context.Cars.Include(c => c.Brand).Include(c => c.TypeCar).OrderBy("c=>c.Giathue1ngay").Skip(offset).Take((int)limit).ToList();
            return Ok(cars);
        }
        [HttpGet]
        [Route("get-by-id")]
        

        public IActionResult Get(int id)
        {
            var car = _context.Cars.Where(c => c.Id == id).Include(car => car.Brand).Include(car => car.TypeCar).First();
            if (car == null) return NotFound();
            return Ok(car);
        }

       
        [HttpPost]
        public IActionResult Create(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
            return Created($"/get-by-id?id={car.Id}", car);
        }
        [HttpPut]
        public IActionResult Update(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var car = _context.Cars.Find(id);
            if (car == null) return NotFound();
            _context.Cars.Remove(car);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpGet]
        [Route("search")]
        public IActionResult Search(string? type, string? brand, string? name, int? limit, int? page, [FromHeader] int userId)
        {
            limit = limit != null ? limit : 20;
            page = page != null ? page : 1;
            int offset = (int)((page - 1) * limit);
            if (brand != null && name != null)
            {
                var c = _context.Cars.Where(p => p.Name.Contains(name) && p.Brand.Name.Contains(brand) && p.TypeCar.Name.Contains(type)).Where(p => p.Status == 0)
                .OrderBy(p => p.Giathue1ngay).Skip(offset).Take((int)limit).ToArray();
                return Ok(c);
            }
            if(brand !=null && name == null)
            {
                var d = _context.Cars.Where(p =>p.Brand.Name.Contains(brand)).Where(p => p.TypeCar.Name.Contains(type)).Where(p => p.Status == 0)
                .OrderBy(p => p.Giathue1ngay).Skip(offset).Take((int)limit).ToArray();
                return Ok(d);

            }
            if(brand == null && name != null)
            {
                var e = _context.Cars.Where(p => p.Name.Contains(name)).Where(p => p.TypeCar.Name.Contains(type)).Where(p => p.Status == 0)
                .OrderBy(p => p.Giathue1ngay).Skip(offset).Take((int)limit).ToArray();
                return Ok(e);
            }
             var car = _context.Cars.Where(p =>p.TypeCar.Name.Contains(type)).Where(p => p.Status == 0)
              .OrderBy(p => p.Giathue1ngay).Skip(offset).Take((int)limit).ToArray();
               return Ok(car);
            
        }

        [HttpGet]
        [Route("sort")]
        public IActionResult SortDesc(int? limit, int? page, [FromHeader] int userId)
        {
            limit = limit != null ? limit : 20;
            page = page != null ? page : 1;
            int offset = (int)((page - 1) * limit);
            
            var car = _context.Cars.Where(p => p.Status == 0).OrderBy("p => p.Giathue1ngay DESC")
               .Skip(offset).Take((int)limit).ToArray();
            return Ok(car);

        }
    }
}
