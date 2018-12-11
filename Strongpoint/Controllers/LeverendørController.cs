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
    public class LeverendørController : ControllerBase
    {
        private readonly SQLDBNORGEContext _dbNorge;

        public LeverendørController(SQLDBNORGEContext context)
        {
            _dbNorge = context;
        }

        // GET: api/Leverendør
        [HttpGet]
        public object GetLeverendør()
        {
            var leverendør = (from l in _dbNorge.Leverendør
                              select new
                              {
                                  id = l.Id,
                                  navn =l.Navn
                              }).ToList();
            return leverendør;
        }

        // GET: api/Leverendør/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeverendør([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var leverendør = await _dbNorge.Leverendør.FindAsync(id);

            if (leverendør == null)
            {
                return NotFound();
            }

            return Ok(leverendør);
        }

        // PUT: api/Leverendør/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeverendør([FromRoute] int id, [FromBody] Leverendør leverendør)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != leverendør.Id)
            {
                return BadRequest();
            }

            _dbNorge.Entry(leverendør).State = EntityState.Modified;

            try
            {
                await _dbNorge.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeverendørExists(id))
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

        // POST: api/Leverendør
        [HttpPost]
        public async Task<IActionResult> PostLeverendør([FromBody] Leverendør leverendør)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbNorge.Leverendør.Add(leverendør);
            await _dbNorge.SaveChangesAsync();

            return CreatedAtAction("GetLeverendør", new { id = leverendør.Id }, leverendør);
        }

        // DELETE: api/Leverendør/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeverendør([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var leverendør = await _dbNorge.Leverendør.FindAsync(id);
            if (leverendør == null)
            {
                return NotFound();
            }

            _dbNorge.Leverendør.Remove(leverendør);
            await _dbNorge.SaveChangesAsync();

            return Ok(leverendør);
        }

        private bool LeverendørExists(int id)
        {
            return _dbNorge.Leverendør.Any(e => e.Id == id);
        }
    }
}