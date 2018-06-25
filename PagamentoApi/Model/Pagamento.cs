using System;
using System.ComponentModel.DataAnnotations;

namespace PagamentoApi.Model
{
    public class Pagamento 
    {
        [Required]
        public string BandeiraCartao { get; set; }

        [Required]
        public string UsuarioCartao { get; set; }

        [Required]
        public string NumeroCartao { get; set; }

        [Required]
        public string CodigoCartao { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public int Parcelas { get; set; }

        [Required]
        public string TokenUsuario { get; set; }
    }
}