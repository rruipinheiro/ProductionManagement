using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Database.Models
{
    public class Pausa
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Inicio { get; set; }

        [Required]
        public DateTime Fim { get; set; }

        [Required]
        public int OperadorId { get; set; }
        public Operador Operador { get; set; }
    }
}
