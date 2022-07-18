using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteTecnicoCredito.Model;

namespace TesteTecnicoCredito.Infrastructure.Interface.IService
{
    public interface ICreditoService
    {
        CreditoRetorno AprovacaoCredito(CreditoSolicitacao creditoSol);

    }
}
