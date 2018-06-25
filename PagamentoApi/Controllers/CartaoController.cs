using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PagamentoApi.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace PagamentoApi.Controllers
{
    [Route("api/v1/[controller]")]   
    [ApiController]
    public class CartaoController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> PagamentoGet([FromQuery] Pagamento _pagamento)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                if(!await ValidaUsuario(_pagamento.TokenUsuario))
                    return BadRequest("Token inválido!");

                await GeraLog(_pagamento.NumeroCartao, _pagamento.Valor.ToString(), _pagamento.Parcelas.ToString());

                return Ok("Sucesso");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        private async Task<bool> ValidaUsuario(string token)
        {
                using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => false; 
                HttpResponseMessage response = await client.GetAsync($"https://localhost:5050/api/v1/usuario/validatoken?_token={token}");

                if(response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }      
        }

        private async Task GeraLog(string cartao, string valor, string parcela)
        {
            Auditoria _auditoria = new Auditoria{
                Acao = "Pagamento",
                DescricaoAcao = $@"Cartão {cartao} validado com sucesso e pagamento de valor R$ {valor} com {parcela} parcela(as), foi efetuado!",
                Servico = "PagamentoApi"
            };

            string _uri = "https://localhost:5052/api/v1/auditoria";

            using (var client = new HttpClient())
            {
                var serializedProduto = JsonConvert.SerializeObject(_auditoria);
                var content = new StringContent(serializedProduto, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(_uri, content);

                var corpo = result;
            }
        }
    }
}