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
    }
}