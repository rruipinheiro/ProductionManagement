using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Database.Models;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Pages {
    public class IndexModel : PageModel {

        private readonly Database.DatabaseContext _context;

        public IndexModel(Database.DatabaseContext context) {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IList<OrdemProducao> OrdemProducao { get; set; }

        public async Task OnGetAsync() {

            /*var OrdemProducaoList = (from op in _context.OrdemProducao
                                     join p in _context.Producao 
                                     on op.Id equals p.OrdemProducaoId
                                     select new {
                                          op.Id,
                                          Sola = op.Sola.Nome,
                                          Defeito = _context.Producao.Where(prod => prod.DefeitoId == prod.DefeitoId).Count(),
                                          Estado = p.Estado.Id
                                      }).ToList();*/

            var searchOP = from m in _context.OrdemProducao
                             select m;

            if (!string.IsNullOrEmpty(SearchString)) {
                searchOP = _context.OrdemProducao.Where(s => s.Sola.Nome.Contains(SearchString));
            }

            OrdemProducao = await searchOP
                .Include(p => p.Sola)
                .Include(p => p.Maquina)
                .Include(p => p.Operador)
                .ToListAsync();

        }
    }
}
