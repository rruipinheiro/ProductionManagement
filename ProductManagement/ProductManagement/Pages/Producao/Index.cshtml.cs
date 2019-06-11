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

        public Pausa Pausa { get; set; }

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

        public async Task<IActionResult> OnPostDefeitoAsync(int? prodId, int? parId) {

            if (prodId == null || parId == null) {
                return NotFound();
            }

            if (!ModelState.IsValid) {
                 return Page();
            }

             var result = await _context.ItemProducao
                .Where(p => p.Id == Producao.Id)
                .SingleOrDefaultAsync();

             result.DefeitoId = Producao.DefeitoId;
             result.Tipo = Producao.Tipo;
             await _context.SaveChangesAsync();

            var _orderProducaoId = await _context.ItemProducao.Where(p => p.Id == Producao.Id).Select(s => s.OrdemProducaoId).FirstOrDefaultAsync();
            var _parId = await _context.ItemProducao.Where(p => p.OrdemProducaoId == _orderProducaoId).Select(s => s.ParId).CountAsync();
            var _tamanho = await _context.ItemProducao.Where(p => p.Id == Producao.Id).Select(s => s.Tamanho).FirstOrDefaultAsync();
            var _quantidade = await _context.ItemProducao.Where(p => p.Id == Producao.Id).Select(s => s.Quantidade).FirstOrDefaultAsync();
            _context.ItemProducao.Add(new ItemProducao { ParId = _parId + 1, Tamanho = _tamanho, Quantidade = _quantidade, Inicio = Producao.Inicio, Fim = Producao.Fim, OrdemProducaoId = _orderProducaoId, DefeitoId = 1, EstadoId = 1 });
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { prodId, parId });
        }

        public async Task<IActionResult> OnPostPausaInicioAsync(int? prodId, int? parId) {

            if (prodId == null || parId == null) {
                return NotFound();
            }

            if (!ModelState.IsValid) {
                return Page();
            }

            var operadorId = await _context.Operador.Where(o => o.User.UserName.Contains(User.Identity.Name)).Select(o => o.Id).FirstOrDefaultAsync();

            var pausa = new Pausa {
                Inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                Fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                OperadorId = await _context.Pausa.Where(p => p.OperadorId == operadorId).Select(o => o.OperadorId).FirstOrDefaultAsync()
            };

            _context.Pausa.Add(pausa);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { prodId, parId });
        }


        public async Task<IActionResult> OnPostPausaFimAsync(int? prodId, int? parId) {

            if (prodId == null || parId == null) {
                return NotFound();
            }

            if (!ModelState.IsValid) {
                return Page();
            }

            var operadorId = await _context.Operador.Where(o => o.User.UserName.Contains(User.Identity.Name)).Select(o => o.Id).FirstOrDefaultAsync();

            var result = await _context.Pausa
               .Where(p => p.OperadorId == operadorId)
               .LastAsync();

            result.Fim = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { prodId, parId });
        }

        // Verifica se existe Ordem de Produção e Itens de Produção
        private bool ProducaoExists(int? prodId, int? parId) {
            return _context.ItemProducao.Where(p => p.OrdemProducaoId.Equals(prodId) && p.ParId.Equals(parId)).Any();
        }

    }
}
