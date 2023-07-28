using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistGestBankABC.Data;
using SistGestBankABC.Models;

namespace SistGestBankABC
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosContactoController : ControllerBase
    {
        private readonly SistGestBankABCContext _context;

        public DatosContactoController(SistGestBankABCContext context)
        {
            _context = context;
        }

        // GET: api/DatosContacto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DatosContacto>>> GetDatosContacto()
        {
          if (_context.DatosContacto == null)
          {
              return NotFound();
          }
            return await _context.DatosContacto.ToListAsync();
        }

        // GET: api/DatosContacto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DatosContacto>> GetDatosContacto(int id)
        {
          if (_context.DatosContacto == null)
          {
              return NotFound();
          }
            var datosContacto = await _context.DatosContacto.FindAsync(id);

            if (datosContacto == null)
            {
                return NotFound();
            }

            return datosContacto;
        }

        // PUT: api/DatosContacto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDatosContacto(int id, DatosContacto datosContacto)
        {
            if (id != datosContacto.id)
            {
                return BadRequest();
            }

            _context.Entry(datosContacto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatosContactoExists(id))
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

        // POST: api/DatosContacto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DatosContacto>> PostDatosContacto(DatosContacto datosContacto)
        {
          if (_context.DatosContacto == null)
          {
              return Problem("Entity set 'SistGestBankABCContext.DatosContacto'  is null.");
          }
            _context.DatosContacto.Add(datosContacto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDatosContacto", new { id = datosContacto.id }, datosContacto);
        }

        // DELETE: api/DatosContacto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDatosContacto(int id)
        {
            if (_context.DatosContacto == null)
            {
                return NotFound();
            }
            var datosContacto = await _context.DatosContacto.FindAsync(id);
            if (datosContacto == null)
            {
                return NotFound();
            }

            _context.DatosContacto.Remove(datosContacto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DatosContactoExists(int id)
        {
            return (_context.DatosContacto?.Any(e => e.id == id)).GetValueOrDefault();
        }

        [HttpGet("cadena")]
        public async Task<IActionResult> searchCadena(string cadena)
        {
            if (_context.DatosContacto == null)
            {
                return NotFound();
            }

            var contacto = from c in _context.DatosContacto
                           select c;

            if (!String.IsNullOrEmpty(cadena))
            {
                contacto = contacto.Where(s => s.firstName!.Contains(cadena) || s.secondName!.Contains(cadena)
                || s.address!.Contains(cadena));
            }

            return Ok(contacto);
        }

        [HttpGet("rangoEdades")]
        public async Task<IActionResult> searchRangoEdades(int age_inicial, int age_final)
        {
            if (_context.DatosContacto == null)
            {
                return NotFound();
            }

            var contactoEdades = from c in _context.DatosContacto where c.age >= age_inicial && c.age <= age_final
                           select c;            

            return Ok(contactoEdades);
        }
    }
}
