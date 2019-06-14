using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Database;
using ProductManagement.Database.Models;

namespace ProductManagement.Pages.Historico {

    public class IndexModel : PageModel {
        private readonly DatabaseContext _context;

        public IndexModel(DatabaseContext context) {
            _context = context;
        }

        [BindProperty]
        public ItemProducao Fase { get; set; }

        public IList<ItemProducao> Producao { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Defeitos { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {

            if (id == null) {
                return NotFound();
            }
            
            ViewData["OrdemProducao"] = _context.OrdemProducao.Where(op => op.Id.Equals(id)).Select(op => op.Id).First();

            var searchHistorico = _context.ItemProducao.Where(p => p.OrdemProducaoId.Equals(id));

            if (!string.IsNullOrEmpty(Defeitos)) {
                if(Defeitos == "SemDefeito") {
                    searchHistorico = searchHistorico.Where(x => x.DefeitoId == 1);
                }
                if(Defeitos == "ComDefeito") {
                    searchHistorico = searchHistorico.Where(x => x.DefeitoId > 1);
                }
            }

            Producao = await searchHistorico
                .Include(p => p.Defeito)
                .Include(p => p.Estado)
                .Include(p => p.Fase)
                .Include(p => p.OrdemProducao.Sola)
                .ToListAsync();

            if (Producao == null) {
                return NotFound();
            }

            ViewData["FaseId"] = new SelectList(_context.Fase, "Id", "Nome");

            return Page();
        }

    }
}
