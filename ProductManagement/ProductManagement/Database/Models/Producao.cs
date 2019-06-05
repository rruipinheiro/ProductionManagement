using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Database.Models
{
    public class Producao
    {

        [Key]
        public int Id { get; set; }

        public string Tipo { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM:ss tt}")]
        public DateTime Inicio { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:MM:ss tt}")]
        public DateTime Fim { get; set; }

        [Required]
        public int OrdemProducaoId { get; set; }
        public OrdemProducao OrdemProducao { get; set; }

        [Required]
        public int DefeitoId { get; set; }
        public Defeito Defeito { get; set; }

        [Required]
        public int EstadoId { get; set; }
        public Estado Estado { get; set; }

    }
}
