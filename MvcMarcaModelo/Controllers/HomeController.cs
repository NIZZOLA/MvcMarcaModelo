using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMarcaModelo.Data;
using MvcMarcaModelo.Models;
using System.Diagnostics;

namespace MvcMarcaModelo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcMarcaModeloContext _context;

        public HomeController(ILogger<HomeController> logger, MvcMarcaModeloContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["MarcaId"] = new SelectList(_context.Set<MarcaModel>(), "Id", "Nome", 0);
            ViewData["ModeloId"] = new SelectList(_context.Set<ModeloModel>(), "Id", "Nome",0);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}