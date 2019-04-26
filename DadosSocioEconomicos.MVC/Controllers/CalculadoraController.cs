using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DadosSocioEconomicos.Data.Contexto;
using DadosSocioEconomicos.Entidades.Entidades;

namespace DadosSocioEconomicos.MVC.Controllers
{
    public class CalculadoraController : Controller
    {
        private readonly Contexto _context;

        public CalculadoraController(Contexto context)
        {
            _context = context;
        }

        // GET: Calculadora
        public async Task<IActionResult> Index()
        {

            return View(await _context.Nacoes.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind("Id,Nome,Populacao,PIB,IDH")] Nacao nacao)
        {

            return View();
        }

        // GET: Calculadora/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nacao = await _context.Nacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nacao == null)
            {
                return NotFound();
            }

            return View(nacao);
        }

        // GET: Calculadora/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calculadora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Populacao,PIB,IDH")] Nacao nacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nacao);
        }

        // GET: Calculadora/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nacao = await _context.Nacoes.FindAsync(id);
            if (nacao == null)
            {
                return NotFound();
            }
            return View(nacao);
        }

        // POST: Calculadora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Populacao,PIB,IDH")] Nacao nacao)
        {
            if (id != nacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NacaoExists(nacao.Id))
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
            return View(nacao);
        }

        // GET: Calculadora/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nacao = await _context.Nacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nacao == null)
            {
                return NotFound();
            }

            return View(nacao);
        }

        // POST: Calculadora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nacao = await _context.Nacoes.FindAsync(id);
            _context.Nacoes.Remove(nacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NacaoExists(int id)
        {
            return _context.Nacoes.Any(e => e.Id == id);
        }
    }
}
