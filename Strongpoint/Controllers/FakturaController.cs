using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strongpoint.Models;

namespace Strongpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakturaController : ControllerBase
    {
        private readonly SQLDBNORGEContext _context;

        public FakturaController(SQLDBNORGEContext context)
        {
            _context = context;
        }

        // GET: api/Faktura
        [HttpGet]
        public IEnumerable<Faktura> GetFaktura()
        {
            return _context.Faktura;
        }

        // GET: api/Faktura/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFaktura([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var faktura = await _context.Faktura.FindAsync(id);

            if (faktura == null)
            {
                return NotFound();
            }

            return Ok(faktura);
        }


        // PUT: api/Faktura/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaktura([FromRoute] int id, [FromBody] Faktura faktura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faktura.Faktura_Nummer)
            {
                return BadRequest();
            }

            _context.Entry(faktura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FakturaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Faktura
        [HttpPost]
        public async Task<IActionResult> PostFaktura([FromBody] Faktura faktura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Faktura.Add(faktura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFaktura", new { id = faktura.Faktura_Nummer }, faktura);
        }

        // DELETE: api/Faktura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaktura([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var faktura = await _context.Faktura.FindAsync(id);
            if (faktura == null)
            {
                return NotFound();
            }

            _context.Faktura.Remove(faktura);
            await _context.SaveChangesAsync();

            return Ok(faktura);
        }

        private bool FakturaExists(int id)
        {
            return _context.Faktura.Any(e => e.Faktura_Nummer == id);
        }
    }
}