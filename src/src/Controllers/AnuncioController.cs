using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers
{
    public class AnuncioController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AnuncioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Anuncio()
        {
            return View();
        }

        public IActionResult AddEditAnuncio(int id = 0)
        {

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

            if (id == 0)
            {
                return View(new Anuncio());
            }
            else
            {
                return View(_context.Anuncio.Include(x => x.Modelo).Include(x => x.Modelo.Marca).Where(x => x.anuncioId.Equals(id)).FirstOrDefault());
            }
            
        }


    }
}
