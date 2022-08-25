using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Domain.Entities {
    public class Transaction {
        [Required]
        public decimal Valor { get; set; }
        [Required]
        [StringLength(1, ErrorMessage = "O campo adquirente não pode ser maior que 20 caracteres.")]
        public string Adquirente { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "O campo bandeira não pode ser maior que 20 caracteres.")]
        public string Bandeira { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "O campo tipo não pode ser maior que 20 caracteres.")]
        public string Tipo { get; set; } 
    }
}
