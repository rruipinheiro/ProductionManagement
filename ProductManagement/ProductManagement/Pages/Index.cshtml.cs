using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Pages {

    public class IndexModel : PageModel {

        private readonly Database.DatabaseContext _context;

        public IndexModel(Database.DatabaseContext context) {
            _context = context;
        }

        public class OrdemProducaoModel {

            public int Id { get; set; }
            public string Sola { get; set; }

            [Display(Name = "Nº de defeitos")]
            public int Defeito { get; set; }
            public string Estado { get; set; }

        }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty]
        public List<OrdemProducaoModel> OrdemProducao { get; set; }
             
        public async Task OnGetAsync() {

            var searchOP = _context.OrdemProducao.Select(op => new OrdemProducaoModel {
                Id = op.Id,
                Sola = op.Sola.Nome,
                Defeito = _context.ItemProducao.Where(p => p.OrdemProducaoId == op.Id && p.DefeitoId > 1).Select(d => d.DefeitoId).Count(),
                Estado = _context.ItemProducao.Where(p => p.OrdemProducaoId == op.Id && p.EstadoId == 3).Count() == _context.ItemProducao.Where(p => p.OrdemProducaoId == op.Id).Count() ? "Produzido" : _context.ItemProducao.Where(p => p.OrdemProducaoId == op.Id && p.EstadoId == 2).Count() > 0 ? "A Produzir" : "Para Produzir"
            });

            if (!string.IsNullOrEmpty(Search)) {
                searchOP = _context.OrdemProducao.Select(op => new OrdemProducaoModel {
                    Id = op.Id,
                    Sola = op.Sola.Nome,
                    Defeito = _context.ItemProducao.Where(p => p.OrdemProducaoId == op.Id && p.DefeitoId > 1).Select(d => d.DefeitoId).Count(),
                    Estado = _context.ItemProducao.Where(p => p.OrdemProducaoId == op.Id && p.EstadoId == 3).Count() == _context.ItemProducao.Where(p => p.OrdemProducaoId == op.Id).Count() ? "Produzido" : _context.ItemProducao.Where(p => p.OrdemProducaoId == op.Id && p.EstadoId == 2).Count() > 0 ? "A Produzir" : "Para Produzir"
                }).Where(s => s.Sola.Contains(Search) || s.Id.ToString().Contains(Search));
            }

            OrdemProducao = await searchOP.ToListAsync();

        }

    }
}
