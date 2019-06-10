using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Database;
using ProductManagement.Database.Models;

namespace ProductManagement.Pages.Producao {

    public class IndexModel : PageModel {

        private readonly DatabaseContext _context;

        public IndexModel(DatabaseContext context) {
            _context = context;
        }
        [BindProperty]
        public ItemProducao Producao { get; set; }

        public async Task<IActionResult> OnGetAsync(int? prodId, int? parId) {

            if (prodId == null || parId == null) {
                return NotFound();
            }

            if(!ProducaoExists(prodId, parId)) {
                return NotFound();
            }

            // Verificar se existe um Par anterior e posterior na Ordem de Produção se não existir returna o Par actual
            TempData["nextPar"] = ProducaoExists(prodId, parId + 1) ? parId + 1 : parId;
            TempData["backPar"] = ProducaoExists(prodId, parId - 1) ? parId - 1 : parId;

            // Returna o Nome e o Tamanho da Sola actual
            ViewData["ParName"] = await _context.ItemProducao.Where(p => p.OrdemProducaoId.Equals(prodId) && p.ParId.Equals(parId)).Select(p => p.OrdemProducao.Sola.Nome).FirstAsync();
            ViewData["ParSize"] = await _context.ItemProducao.Where(p => p.OrdemProducaoId.Equals(prodId) && p.ParId.Equals(parId)).Select(p => p.Tamanho).FirstAsync();

            Producao = await _context.ItemProducao
                .Include(p => p.Defeito)
                .Include(p => p.Estado)
                .Include(p => p.OrdemProducao)
                .FirstOrDefaultAsync(m => m.OrdemProducaoId.Equals(prodId));

            if (Producao == null) {
                return NotFound();
            }

            // Returna o Id [Maquina] atribuida à [OrdemProducao] para ser possivel ler os valores dos sensores
            var OrdemMaquinaId = await _context.OrdemProducao
                .Where(op => op.Id.Equals(prodId))
                .Select(op => op.MaquinaId)
                .SingleOrDefaultAsync();

            // Sensor Temperatura -> ID = 1
            ViewData["TemperaturaSensor"] = await _context.Registo
                .Include(r => r.Sensor)
                .Where(s => s.Sensor.Maquina.Id == OrdemMaquinaId && s.SensorId == 1)
                .Select(t => t.Valor)
                .SingleOrDefaultAsync();

            // Sensor Pressão -> ID = 2
            ViewData["PressaoSensor"] = await _context.Registo
                .Include(r => r.Sensor)
                .Where(s => s.Sensor.Maquina.Id == OrdemMaquinaId && s.SensorId == 2)
                .Select(t => t.Valor)
                .SingleOrDefaultAsync();

            // Sensor Velocidade de Injeção -> ID = 3
            ViewData["VelocidadeInjecaoSensor"] = await _context.Registo
                .Include(r => r.Sensor)
                .Where(s => s.Sensor.Maquina.Id == OrdemMaquinaId && s.SensorId == 3)
                .Select(t => t.Valor)
                .SingleOrDefaultAsync();

            // Sensor Velocidade de Carregamento -> ID = 4
            ViewData["VelocidadeCarregamentoSensor"] = await _context.Registo
                .Include(r => r.Sensor)
                .Where(s => s.Sensor.Maquina.Id == OrdemMaquinaId && s.SensorId == 4)
                .Select(t => t.Valor)
                .SingleOrDefaultAsync();

            ViewData["DefeitoId"] = new SelectList(_context.Defeito, "Id", "Nome");

            return Page();
        }

        public async Task<IActionResult> OnPostDefeitoAsync() {

             if (!ModelState.IsValid) {
                 return Page();
             }

             var result = await _context.ItemProducao
                .Where(p => p.Id == Producao.Id)
                .SingleOrDefaultAsync();

             result.DefeitoId = Producao.DefeitoId;
             result.Tipo = Producao.Tipo;
             await _context.SaveChangesAsync();

             return RedirectToPage("./Index");
        }

         /*public async Task<IActionResult> OnPostPausaAsync() {

             if (!ModelState.IsValid)
             {
                 string messages = string.Join("; ", ModelState.Values
                         .SelectMany(x => x.Errors)
                         .Select(x => x.ErrorMessage));

                 Console.WriteLine(" =============== {0} ===============", messages);

                 return Page();
             }

             var result = await _context.Pausa
                 .Include(p => p.Operador)
                 .Where(p => p.Operador.User.UserName.Equals(User.Identity.Name))
                 .SingleOrDefaultAsync();

             result.Inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
             result.Fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
             result.OperadorId = 1;
             await _context.SaveChangesAsync();

             return RedirectToPage("./Index");
         }*/

        // Verifica se existe Ordem de Produção e Itens de Produção
        private bool ProducaoExists(int? prodId, int? parId) {
            return _context.ItemProducao.Where(p => p.OrdemProducaoId.Equals(prodId) && p.ParId.Equals(parId)).Any();
        }

    }
}
