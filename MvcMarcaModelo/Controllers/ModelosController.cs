using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMarcaModelo.Data;
using MvcMarcaModelo.Models;
using NuGet.Protocol.Core.Types;

namespace MvcMarcaModelo.Controllers
{
    public class ModelosController : Controller
    {
        private readonly MvcMarcaModeloContext _context;

        public ModelosController(MvcMarcaModeloContext context)
        {
            _context = context;
        }

        // GET: Modelos
        public async Task<IActionResult> Index()
        {
            var mvcMarcaModeloContext = _context.Modelos.Include(m => m.Marca);
            return View(await mvcMarcaModeloContext.ToListAsync());
        }

        // GET: Modelos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Modelos == null)
            {
                return NotFound();
            }

            var modeloModel = await _context.Modelos
                .Include(m => m.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modeloModel == null)
            {
                return NotFound();
            }

            return View(modeloModel);
        }

        // GET: Modelos/Create
        public IActionResult Create()
        {
            CreateDropDown(0);
            return View();
        }

        // POST: Modelos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,MarcaId")] ModeloModel modeloModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modeloModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CreateDropDown(modeloModel.MarcaId);
            return View(modeloModel);
        }

        // GET: Modelos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Modelos == null)
            {
                return NotFound();
            }

            var modeloModel = await _context.Modelos.FindAsync(id);
            if (modeloModel == null)
            {
                return NotFound();
            }
            CreateDropDown( modeloModel.MarcaId);
            return View(modeloModel);
        }

        private void CreateDropDown(int marcaId)
        {
            ViewData["MarcaId"] = new SelectList(_context.Set<MarcaModel>(), "Id", "Nome", marcaId);
        }

        // POST: Modelos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,MarcaId")] ModeloModel modeloModel)
        {
            if (id != modeloModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modeloModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeloModelExists(modeloModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            CreateDropDown(modeloModel.MarcaId);
            return View(modeloModel);
        }

        // GET: Modelos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Modelos == null)
            {
                return NotFound();
            }

            var modeloModel = await _context.Modelos
                .Include(m => m.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modeloModel == null)
            {
                return NotFound();
            }

            return View(modeloModel);
        }

        // POST: Modelos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Modelos == null)
            {
                return Problem("Entity set 'MvcMarcaModeloContext.ModeloModel'  is null.");
            }
            var modeloModel = await _context.Modelos.FindAsync(id);
            if (modeloModel != null)
            {
                _context.Modelos.Remove(modeloModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeloModelExists(int id)
        {
          return (_context.Modelos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult GetModelosPorMarca(int id)
        {
            var modelosPorMarca = _context.Modelos?.Where(s => s.MarcaId == id).Select(c => new { Id = c.Id, Name = c.Nome}).ToList();
            return Json(modelosPorMarca);
        }
    }
}
