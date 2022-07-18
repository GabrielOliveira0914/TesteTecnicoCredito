using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteTecnicoCredito.Model
{
    public class CreditoSolicitacao
    {
        public double ValorCredito { get; set; }

        public string TipoCredito { get; set; }

        public int QtdParcelas { get; set; }

        public DateTime DataPrimeiraParcela { get; set; }
    }
}
