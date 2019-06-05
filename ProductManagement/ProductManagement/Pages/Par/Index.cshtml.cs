using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Database;
using ProductManagement.Database.Models;

namespace ProductManagement.Pages.Par {

    public class IndexModel : PageModel {

        private readonly DatabaseContext _context;

        public IndexModel(DatabaseContext context) {
            _context = context;
        }
        [BindProperty]
        public Producao Producao { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {

            if (id == null) {
                return NotFound();
            }

            Producao = await _context.Producao
                .Include(p => p.Defeito)
                .Include(p => p.Estado)
                .Include(p => p.OrdemProducao).FirstOrDefaultAsync(m => m.Id == id);

            if (Producao == null) {
                return NotFound();
            }

            // Vai buscar o Id [Maquina] atribuida à [OrdemProducao] para ser possivel ler os valores dos sensores associados à maquina
            var OrdemMaquinaId = await _context.OrdemProducao
                .Where(op => op.Id == id)
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

        public async Task<IActionResult> OnPostAsync() {

            if (!ModelState.IsValid) {
                return Page();
            }

            var result = await _context.Producao
               .Where(p => p.Id == Producao.Id)
               .SingleOrDefaultAsync();

            result.DefeitoId = Producao.DefeitoId;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
