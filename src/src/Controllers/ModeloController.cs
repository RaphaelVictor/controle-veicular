using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Data;
using src.Models;

namespace src.Controllers
{
    public class ModeloController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ModeloController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Modelo()
        {
            return View();
        }

        public IActionResult AddEditModelo(int id = 0)
        {
            if (id == 0)
            {
                return View(new Modelo());
            }
            else
            {
                return View(_context.Modelo.Where(x => x.modeloId.Equals(id)).FirstOrDefault());
            }
            
        }


    }
}
