using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Database.Models {
    public class ItemProducao {

        [Key]
        public int Id { get; set; }
        
        [Required]
        public int ParId{ get; set; }

        public string Tipo { get; set; }

        [Required]
        public int Tamanho { get; set; }

        [Required]
        public int Quantidade { get; set; }

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

        [Required]
        public int FaseId { get; set; }
        public Fase Fase { get; set; }

    }
}
