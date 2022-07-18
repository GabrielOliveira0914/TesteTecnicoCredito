using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteTecnicoCredito.Infrastructure.Interface.IService;
using TesteTecnicoCredito.Model;

namespace TesteTecnicoCredito.Controllers
{
    [Route("api/credito")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        readonly ICreditoService _ICreditoService;

        public CreditoController(ICreditoService creditoService)
        {
            _ICreditoService = creditoService;
        }

        [HttpPost("solicitacao")]
        public IActionResult GetAprovacaoCredito(CreditoSolicitacao creditoSol)
        {

            CreditoRetorno creditoRet = _ICreditoService.AprovacaoCredito(creditoSol);

            if(creditoRet.StatusCredito == "Recusado")
            {
                CreditoRecusado creditoRecusado = new CreditoRecusado();
                creditoRecusado.StatusCredito = creditoRet.StatusCredito;
                creditoRecusado.Razao = creditoRet.Razao;
                return Ok(creditoRecusado);
            }

            CreditoAprovado creditoAprovado = new CreditoAprovado
            {
                StatusCredito = "Aprovado",
                ValorJuros = string.Format("{0:0.00}", creditoRet.ValorJuros),
                ValorTotalComJuros = string.Format("{0:0.00}", creditoRet.ValorTotalComJuros)
            };


            return Ok(creditoAprovado);
        }
    }
}