using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/anuncio")]
    public class AnuncioControllerApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnuncioControllerApi(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Anuncio
        [HttpGet]
        public IActionResult GetAnuncio()
        {
            return Json(new { data = _context.Anuncio.Include(m => m.Modelo).Include(m => m.Modelo.Marca).ToList() });
        }

        // GET: api/Anuncio/Anuncios
        [HttpGet("{id}")]
        [Route("Anuncios")]
        public IActionResult Anuncios(int anuncioId)
        {
            return Json(_context.Anuncio.Include(m => m.Modelo).Where(m => m.anuncioId == anuncioId).ToDictionary(x => x.modeloId, x => x.Modelo.modeloDesc));
        }


        // POST: api/Anuncio
        [HttpPost]
        public async Task<IActionResult> PostAnuncio([FromBody] Anuncio anuncio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (anuncio.anuncioId == 0)
            {
                _context.Anuncio.Add(anuncio);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Adicionado com sucesso." });
            }
            else
            {
                _context.Update(anuncio);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Editado com sucesso." });
            }

            

        }

        // DELETE: api/Anuncio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnuncio([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var anuncio = await _context.Anuncio.SingleOrDefaultAsync(m => m.anuncioId == id);
            if (anuncio == null)
            {
                return NotFound();
            }

            _context.Anuncio.Remove(anuncio);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Deletado com sucesso." });
        }

        private bool AnuncioExists(int id)
        {
            return _context.Anuncio.Any(e => e.anuncioId == id);
        }
    }
}