using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nhomnetapi.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace nhomnetapi.Controllers
{
    [ApiController]
    [Route("api/contracts")]
    public class ContractsController : Controller
    {
        private readonly T22netContext _context;
        public ContractsController(T22netContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(int? limit, int? page, [FromHeader] int userId)
        {
            limit = limit != null ? limit : 20;
            page = page != null ? page : 1;
            int offset = (int)((page - 1) * limit);
            var contracts = _context.Contracts.Where(c=>c.Status==0).Include(c=>c.Users).Include(c=>c.Cars).Skip(offset).Take((int)limit).ToList();
            return Ok(contracts);
        }
        [HttpGet]
        [Route("get-by-id")]
        public IActionResult Get(int id)
        {
            var contract = _context.Contracts.Where(c => c.Id == id).Include(contract => contract.Users).Include(contract=>contract.Cars).First();
            if (contract == null) return NotFound();
            return Ok(contract);
        }
        [HttpGet]
        [Route("getmangngay")]
        public IActionResult GetMangNgay(int id)
        {
            var mangngay = _context.Contracts.Where(c => c.Status == 2).Where(c=>c.CarsId==id).Where(c => DateTime.Compare(c.Ngaythue, DateTime.UtcNow) > 0|| DateTime.Compare(c.Ngaytra, DateTime.UtcNow) > 0).Select(k => new { k.Ngaythue, k.Ngaytra }).ToList();
            return Ok(mangngay);
        }


        [HttpPost]
        public IActionResult Create(Contract contract)
        {
            var car = _context.Cars.Find(contract.CarsId);
            if (car == null) return NotFound();
            var mangngay = _context.Contracts.Where(c => c.Status == 2).Where(c => c.CarsId == car.Id).Where(c => DateTime.Compare(c.Ngaythue,DateTime.UtcNow) > 0 || DateTime.Compare(c.Ngaytra, DateTime.UtcNow) > 0).Select(k => new { k.Ngaythue, k.Ngaytra }).ToList();
            
            foreach(var c in mangngay)
            {
                if (contract.Ngaythue >= c.Ngaythue && contract.Ngaytra <= c.Ngaytra)
                {
                    return NotFound();
                }
                if (contract.Ngaythue >= c.Ngaythue && contract.Ngaythue <= c.Ngaytra)
                {
                    return NotFound();

                }
                if(contract.Ngaytra>=c.Ngaythue && contract.Ngaytra <= c.Ngaytra)
                {
                    return NotFound();
                }
            }
            var giathue = car.Giathue1ngay;
            DateTime ngaythue = Convert.ToDateTime(contract.Ngaythue);
            DateTime ngaytra = Convert.ToDateTime(contract.Ngaytra);
            TimeSpan Time = ngaytra-ngaythue;
            int TongSoNgay = Time.Days;
            contract.Ngaykyhopdong = DateTime.Today;
            contract.Giatridatcoc = giathue * 10;
            contract.Giatrihopdong = giathue * TongSoNgay;
            _context.Contracts.Add(contract);
            _context.SaveChanges();
            return Created($"/get-by-id?id={contract.Id}", contract);
        }

        [HttpPut]
        public IActionResult Update(Contract contract)
        {
            _context.Contracts.Update(contract);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPut]
        [Route("payment")]
        public IActionResult UpdatePayment(int id)
        {
            var contract = _context.Contracts.Find(id);
            if (contract == null) return NotFound();
            contract.Status = 2;
            _context.Contracts.Update(contract);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpGet]
        [Route("filteruserid")]
        public IActionResult FilterUser(int id)
        {
            var con = _context.Contracts.Include(con => con.Cars).Include(con => con.Users).Where(c => c.UsersId == id).ToArray();
            return Ok(con);
        }
        [HttpGet]
        //[Authorize(Policy = "QA")]
        [Route("total")]
        public IActionResult Total() 
        {
            var total = _context.Contracts.Where(c => c.Status == 1).Select(i => Convert.ToDouble(i.Giatrihopdong)).Sum();
           
            return Ok(total);
        }

        [HttpGet]
        //[Authorize(Policy = "QA")]
        [Route("totalmonth")]
        public IActionResult TotalMonth()
        {
            var data = _context.Contracts.Where(c=>c.Status ==1).Select(k => new { k.Ngaykyhopdong.Year, k.Ngaykyhopdong.Month, k.Giatrihopdong }).GroupBy(x => new { x.Year, x.Month }, (key, group) => new
            {
                yr = key.Year,
                mnth = key.Month,
                tCharge = group.Sum(k => k.Giatrihopdong)
            }).ToList();
            return Ok(data);
        }
    }
}
