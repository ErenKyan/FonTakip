using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TefasFon.Controllers
{
    public class FonListesiController : Controller
    {
        // GET: /<controller>/
        public IActionResult Fonlar()
        {
            return View();
        }
    }
}

