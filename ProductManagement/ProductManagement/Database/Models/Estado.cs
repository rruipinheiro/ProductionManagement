using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Database.Models
{
    public class Estado {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public ICollection<ItemProducao> ItemsProducao { get; set; }
    }
}
