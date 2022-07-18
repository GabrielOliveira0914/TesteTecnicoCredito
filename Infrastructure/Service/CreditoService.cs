using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteTecnicoCredito.Infrastructure.Interface.IService;
using TesteTecnicoCredito.Model;


namespace TesteTecnicoCredito.Infrastructure.Service
{
    public class CreditoService : ICreditoService
    {
            public CreditoRetorno AprovacaoCredito(CreditoSolicitacao creditoSol)
        {

            CreditoRetorno creditoRetorno = new CreditoRetorno();


            if (creditoSol.ValorCredito > 1000000)
            {
                creditoRetorno.StatusCredito = "Recusado";
                creditoRetorno.Razao = "Valor superior ao limite (R$ 1.000.000,00)";
                return creditoRetorno;
            }

            if (creditoSol.QtdParcelas < 5 || creditoSol.QtdParcelas > 70)
            {
                creditoRetorno.StatusCredito = "Recusado";
                creditoRetorno.Razao = "Quantidade de parcelas invalido (deve estar entre 5 e 70)";
                return creditoRetorno;
            }

            if (creditoSol.TipoCredito.Equals("Crédito Pessoa Jurídica") && creditoSol.ValorCredito < 15000)
            {
                creditoRetorno.StatusCredito = "Recusado";
                creditoRetorno.Razao = "Valor inferior ao minimo para a modalidade Pessoa Jurídica (R$ 15.000,00)";
                return creditoRetorno;
            }

            if (creditoSol.DataPrimeiraParcela < DateTime.Now.AddDays(15) && creditoSol.DataPrimeiraParcela > DateTime.Now.AddDays(75))
            {
                creditoRetorno.StatusCredito = "Recusado";
                creditoRetorno.Razao = "Valor inferior ao minimo para a modalidade Pessoa Jurídica (R$ 15.000,00)";
                return creditoRetorno;
            }

            switch (creditoSol.TipoCredito)
            {
                case "Crédito Direto":
                    return CreditoDireto(creditoSol);

                case "Crédito Consignado":
                    return CreditoConsignado(creditoSol);
                    
                case "Crédito Pessoa Jurídica":
                    return CreditoPJ(creditoSol);

                case "Crédito Pessoa Física":
                    return CreditoPF(creditoSol);

                case "Crédito Imobiliário":
                    return CreditoImobiliario(creditoSol);

                default:
                    creditoRetorno.StatusCredito = "Recusado";
                    creditoRetorno.Razao = "Tipo de crédito invalido.";
                    return creditoRetorno;
            }
        }

        public CreditoRetorno CreditoDireto(CreditoSolicitacao creditoSol)
        {
            CreditoRetorno creditoRetorno = new CreditoRetorno();

            creditoRetorno.ValorTotalComJuros =  creditoSol.ValorCredito * Math.Pow(1.02, creditoSol.QtdParcelas);
            creditoRetorno.ValorJuros = creditoRetorno.ValorTotalComJuros - creditoSol.ValorCredito;
            return creditoRetorno;
        }

        public CreditoRetorno CreditoConsignado(CreditoSolicitacao creditoSol)
        {
            CreditoRetorno creditoRetorno = new CreditoRetorno();

            creditoRetorno.ValorTotalComJuros = creditoSol.ValorCredito * Math.Pow(1.01, creditoSol.QtdParcelas);
            creditoRetorno.ValorJuros = creditoRetorno.ValorTotalComJuros - creditoSol.ValorCredito;
            return creditoRetorno;
        }

        public CreditoRetorno CreditoPJ(CreditoSolicitacao creditoSol)
        {
            CreditoRetorno creditoRetorno = new CreditoRetorno();

            creditoRetorno.ValorTotalComJuros = creditoSol.ValorCredito * Math.Pow(1.05, creditoSol.QtdParcelas);
            creditoRetorno.ValorJuros = creditoRetorno.ValorTotalComJuros - creditoSol.ValorCredito;
            return creditoRetorno;
        }

        public CreditoRetorno CreditoPF(CreditoSolicitacao creditoSol)
        {
            CreditoRetorno creditoRetorno = new CreditoRetorno();

            creditoRetorno.ValorTotalComJuros = creditoSol.ValorCredito * Math.Pow(1.03, creditoSol.QtdParcelas);
            creditoRetorno.ValorJuros = creditoRetorno.ValorTotalComJuros - creditoSol.ValorCredito;
            return creditoRetorno;
        }

        public CreditoRetorno CreditoImobiliario(CreditoSolicitacao creditoSol)
        {
            CreditoRetorno creditoRetorno = new CreditoRetorno();

            creditoRetorno.ValorTotalComJuros = creditoSol.ValorCredito * Math.Pow(1.09, creditoSol.QtdParcelas);
            creditoRetorno.ValorJuros = creditoRetorno.ValorTotalComJuros - creditoSol.ValorCredito;
            return creditoRetorno;
        }
    }
}
