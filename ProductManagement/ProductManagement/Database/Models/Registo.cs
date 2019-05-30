using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Database.Models
{
    public class Registo
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public float Valor { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }

    }
}
