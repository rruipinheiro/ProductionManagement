using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Database.Models
{
    public class Operador
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public int Numero { get; set; }

        public ICollection<OrdemProducao> OrdemProducoes { get; set; }
        public ICollection<Pausa> Pausas { get; set; }

    }
}
