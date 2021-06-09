using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Marca")]
    public class MarcaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarcaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Marca
        [HttpGet]
        public IActionResult GetMarca()
        {
            return Json(new { data = _context.Marca.ToList() });
        }

        // GET: api/Marcas
        [HttpGet]
        [Route("Marcas")]
        public IActionResult Marcas()
        {
            return Json(_context.Marca.ToList().ToDictionary(x => x.marcaId, x => x.marcaDesc));
        }


        // POST: api/Marca
        [HttpPost]
        public async Task<IActionResult> PostMarca([FromBody] Marca marca)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (marca.marcaId == 0)
            {
                _context.Marca.Add(marca);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Adicionado com sucesso." });
            }
            else
            {
                _context.Update(marca);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Editado com sucesso." });
            }

            

        }

        // DELETE: api/Marca/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarca([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var marca = await _context.Marca.SingleOrDefaultAsync(m => m.marcaId == id);
            if (marca == null)
            {
                return NotFound();
            }

            _context.Marca.Remove(marca);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Deletado com sucesso." });
        }

        private bool MarcaExists(int id)
        {
            return _context.Marca.Any(e => e.marcaId == id);
        }
    }
}