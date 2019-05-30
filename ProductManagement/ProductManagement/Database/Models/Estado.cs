using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Database.Models
{
    public class Estado
    {

        [Key]
        public int Id { get; set; }

        public enum Tipo { Produzir, ParaProduzir, Concluido }

        public ICollection<Producao> Producoes { get; set; }
    }
}
