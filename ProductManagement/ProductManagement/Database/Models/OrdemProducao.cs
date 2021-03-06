﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Database.Models {

    public class OrdemProducao {

        [Key]
        [Display(Name = "Ordem Produção")]
        public int Id { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public int SolaId { get; set; }
        public Sola Sola { get; set; }

        public int MaquinaId { get; set; }
        public Maquina Maquina { get; set; }

        public int OperadorId { get; set; }
        public Operador Operador { get; set; }

        public ICollection<ItemProducao> Producoes { get; set; }
    }
}
