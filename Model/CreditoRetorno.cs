using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteTecnicoCredito.Model
{
    public class CreditoRetorno
    {
        public string StatusCredito { get; set; }

        public double ValorTotalComJuros { get; set; }

        public double ValorJuros { get; set; }

        public string? Razao { get; set; }
    }
}
