using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Database;
using ProductManagement.Database.Models;

namespace ProductManagement.Pages.Ordem2
{
    public class IndexModel : PageModel
    {
        private readonly ProductManagement.Database.DatabaseContext _context;

        public IndexModel(ProductManagement.Database.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Producao> Producao { get;set; }

        public async Task OnGetAsync()
        {

            Producao = await _context.Producao
                .Include(p => p.Defeito)
                .Include(p => p.Estado)
                .Include(p => p.OrdemProducao)
                .Include(p => p.OrdemProducao.Sola)
                .ToListAsync();
        }
    }
}
