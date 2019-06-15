using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Database;
using ProductManagement.Database.Models;

namespace ProductManagement.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistoController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public RegistoController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Registo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registo>>> GetRegisto()
        {
            return await _context.Registo.ToListAsync();
        }

        // GET: api/Registo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Registo>> GetRegisto(int id)
        {
            var registo = await _context.Registo.FindAsync(id);

            if (registo == null)
            {
                return NotFound();
            }

            return registo;
        }

        // POST: api/Registo
        // POST api/values
        /*[HttpPost]
        public async Task<IActionResult> PostRegisto([FromBody] string value) {

            Console.WriteLine("=================== {0} ==================", v)

            var registo = new Registo
            {
                Valor = int.Parse(value),
                Data = new DateTime(),
                SensorId = 1
            };

            _context.Registo.Add(registo);
            await _context.SaveChangesAsync();

            return Ok();
        }*/

        [HttpPost]
        public async Task<ActionResult> PostRegisto(Registo registo) {

            var _registo = new Registo
            {
                Valor = registo.Valor,
                Data = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                SensorId = 5
            };

            _context.Registo.Add(_registo);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
        private bool RegistoExists(int id) {
            return _context.Registo.Any(e => e.Id == id);
        }

    }
}
