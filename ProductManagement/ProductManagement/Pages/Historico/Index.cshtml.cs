using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Database;
using ProductManagement.Database.Models;

namespace ProductManagement.Pages.Historico {

    public class IndexModel : PageModel {
        private readonly DatabaseContext _context;

        public IndexModel(DatabaseContext context) {
            _context = context;
        }

        public IList<Producao> Producao { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {

            if (id == null) {
                return NotFound();
            }
            
            ViewData["OrdemProducao"] = _context.OrdemProducao.Where(op => op.Id.Equals(id)).Select(op => op.Id).First();

            Producao = await _context.Producao
                .Include(p => p.Defeito)
                .Include(p => p.Estado)
                .Include(p => p.OrdemProducao.Sola)
                .Where(p => p.OrdemProducaoId.Equals(id))
                .ToListAsync();

            if (Producao == null) {
                return NotFound();
            }

            return Page();
        }

    }
}
