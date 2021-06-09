using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Models;

namespace src.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Marca()
        {
            return View();
        }

        public IActionResult AddEditMarca(int id = 0)
        {
            if (id == 0)
            {
                return View(new Marca());
            }
            else
            {
                return View(_context.Marca.Where(x => x.marcaId.Equals(id)).FirstOrDefault());
            }
            
        }


    }
}
