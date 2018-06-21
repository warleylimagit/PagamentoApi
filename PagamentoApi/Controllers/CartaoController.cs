using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using PagamentoApi.Model;

namespace PagamentoApi.Controllers
{
    [Route("api/v1/[controller]")]   
    [ApiController]
    public class CartaoController : ControllerBase
    {
        public IActionResult PagamentoGet([FromQuery] Pagamento _pagamento)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok("Sucesso");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}