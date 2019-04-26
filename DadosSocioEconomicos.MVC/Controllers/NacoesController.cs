using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DadosSocioEconomicos.Data.Contexto;
using DadosSocioEconomicos.Entidades.Entidades;
using DadosSocioEconomicos.MVC.Models;

namespace DadosSocioEconomicos.MVC.Controllers
{
    public class NacoesController : Controller
    {
        private readonly Contexto _context;

        public NacoesController(Contexto context)
        {
            _context = context;
        }

        public List<SelectListItem> MontaDropDownListPaises()
        {
            List<Nacao> nacoes = _context.Nacoes.ToList();
            List<string> listaNomes = new List<string>();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (Nacao nacao in nacoes)
            {
                listaNomes.Add(nacao.Nome);
            }

            int n = listaNomes.Count();

            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    items.Add(new SelectListItem { Value = listaNomes[i], Text = listaNomes[i],  Selected = true });
                }
                else { items.Add(new SelectListItem { Value = listaNomes[i], Text = listaNomes[i]  }); }
            }
            return items;
        }

        public List<string> MontaListaPaises()
        {
            List<Nacao> nacoes = _context.Nacoes.ToList();
            List<string> listaNomes = new List<string>();
            foreach (Nacao nacao in nacoes)
            {
                listaNomes.Add(nacao.Nome);
            }

            return listaNomes;
        }

        public List<string> MontaDropDownListNomePaises()
        {
            List<Nacao> nacoes = _context.Nacoes.ToList();
            List<string> listaNomes = new List<string>();
            
            foreach (Nacao nacao in nacoes)
            {
                listaNomes.Add(nacao.Nome);
            }
            return listaNomes;
        }

        public List<SelectListItem> MontaDropDownListIndicadores()
        {
            List<SelectListItem> indicadores = new List<SelectListItem>
            {
                new SelectListItem { Text = "População", Value = "0", Selected = true },
                new SelectListItem { Text = "PIB", Value = "1" },
                new SelectListItem { Text = "IDH", Value = "2" }
            };
            return indicadores;
        }

        // GET: Nacoes
        public IActionResult Grafico()
        {
            return View();
        }

        public IActionResult ComparativoDuas()
        {
            var listaPaises = MontaDropDownListPaises();
            List<SelectListItem> listaIndicadores = MontaDropDownListIndicadores();

            ViewBag.Paises = listaPaises;
            ViewBag.Indicadores = listaIndicadores;


            return View();
        }

        [HttpPost]
        public IActionResult ComparativoDuas([Bind("PrimeiraNacao,SegundaNacao,Indicador")] Comparativo comparativo)
        {
            var listaDePaises = MontaDropDownListPaises();
            ViewBag.Paises = listaDePaises;
            var listaPaises = MontaListaPaises();
            string nomeIndicador = comparativo.Indicador;
            string nome = "";
            decimal d1 = 0;
            decimal d2 = 0;
            Nacao Pais1 = _context.Nacoes.Where(m => m.Nome == comparativo.PrimeiraNacao).FirstOrDefault();
            Nacao Pais2 = _context.Nacoes.Where(m => m.Nome == comparativo.SegundaNacao).FirstOrDefault();

            if (nomeIndicador == "Populacao")
            {
                d1 = Pais1.Populacao;
                d2 = Pais2.Populacao;
                nome = "Análise da população";
            }
            if (nomeIndicador == "PIB")
            {
                d1 = Pais1.PIB;
                d2 = Pais2.PIB;
                nome = "Análise do PIB";
            }
            if (nomeIndicador == "IDH")
            {
                d1 = Pais1.IDH;
                d2 = Pais2.IDH;
                nome = "Análise do IDH";
            }

            var eixoX = new List<string>();
            var eixoY = new List<decimal>();

            eixoX.Add(Pais1.Nome);
            eixoY.Add(d1);
            eixoX.Add(Pais2.Nome);
            eixoY.Add(d2);

            ViewBag.EixoX = eixoX.ToList();
            ViewBag.EixoY = eixoY.ToList();
            ViewBag.Nome = nome;

            ViewData["ListaNacoes"] = new SelectList(listaPaises);

            return View();
        }

        public ActionResult ComparativoGeral()
        {
            return View();
        }

        public ActionResult ComparativoGenerico()
        {
            var listaDePaises = MontaDropDownListPaises();
            ViewBag.Paises = listaDePaises;
            return View();
        }

        [HttpPost]
        public ActionResult ComparativoGenerico([Bind("ListaNacoes,Indicador")] NacoesGeral nacoes)
        {
            var listaDePaises = MontaDropDownListPaises();
            string indicador = nacoes.Indicador;
            string[] listaNomes = nacoes.ListaNacoes;
            int qtdPaises = listaNomes.Length;

            for (int i = 0; i < qtdPaises; i++)
            {

            }

            ViewBag.Paises = listaDePaises;

            return View();
        }

        [HttpPost]
        public  ActionResult ComparativoGeral([Bind("Dado")] Indicador indicador)
        {
            string nomeIndicador = indicador.Dado;
            string nome = "";
            List<Nacao> nacoes = _context.Nacoes.ToList();
            var eixoX = new List<string>();
            var eixoY = new List<decimal>();

            foreach(Nacao nacao in nacoes)
            {
                eixoX.Add(nacao.Nome);

                if (nomeIndicador == "Populacao") { eixoY.Add(nacao.Populacao); }
                
                if (nomeIndicador == "IDH") { eixoY.Add(nacao.IDH); }

                if (nomeIndicador == "PIB") { eixoY.Add(nacao.PIB); }

            }

            if (nomeIndicador == "Populacao") { nome = "Análise da população"; }

            if (nomeIndicador == "IDH") { nome = "Análise do IDH"; }

            if (nomeIndicador == "PIB") { nome = "Análise do PIB"; }

            ViewBag.EixoX = eixoX.ToList();
            ViewBag.EixoY = eixoY.ToList();
            ViewBag.Nome = nome;
            return View();
        }

        // GET: Nacoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nacoes.ToListAsync());
        }

        // GET: Nacoes/Details/5
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

        // GET: Nacoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nacoes/Create
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

        // GET: Nacoes/Edit/5
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

        // POST: Nacoes/Edit/5
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

        // GET: Nacoes/Delete/5
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

        // POST: Nacoes/Delete/5
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
