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
    [Route("api/modelo")]
    public class ModeloControllerApi : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModeloControllerApi(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Modelo
        [HttpGet]
        public IActionResult GetModelo()
        {
            return Json(new { data = _context.Modelo.Include(m => m.Marca).ToList() });
        }

        // GET: api/Marcas
        [HttpGet("{id}")]
        [Route("Marcas")]
        public IActionResult Marcas(int marcaid)
        {
            return Json(_context.Modelo.Include(m => m.Marca).Where(m => m.marcaid == marcaid).ToList().ToDictionary(x => x.modeloId, x => x.modeloDesc));
        }


        // POST: api/Modelo
        [HttpPost]
        public async Task<IActionResult> PostModelo([FromBody] Modelo modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (modelo.modeloId == 0)
            {
                _context.Modelo.Add(modelo);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Adicionado com sucesso." });
            }
            else
            {
                _context.Update(modelo);

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Editado com sucesso." });
            }

            

        }

        // DELETE: api/Modelo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelo = await _context.Modelo.SingleOrDefaultAsync(m => m.modeloId == id);
            if (modelo == null)
            {
                return NotFound();
            }

            _context.Modelo.Remove(modelo);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Deletado com sucesso." });
        }

        private bool ModeloExists(int id)
        {
            return _context.Modelo.Any(e => e.modeloId == id);
        }
    }
}