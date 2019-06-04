using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Database.Models
{
    public class Defeito
    {

        [Key]
        public int Id { get; set; }

        public enum Tipo { Par, Esquerdo, Direito }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        public ICollection<Producao> Producoes { get; set; }
    }
}
