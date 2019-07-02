namespace CSharp.Financial.Stocks.Task.ContaCorrentes
{
    using CSharp.MercadoFinanceiro.Entities;
    using System;

    public class Nota : ContaCorrente<Descricoes.Nota>, INota
    {
        DateTime INota.Data => Descricao.Pregao;
        public Nota(DateTime dataLiquidacao,
                    DateTime dataMovimentacao,
                    decimal valor,
                    Descricoes.Nota descricao,
                    decimal saldo,
                    Nota estorno = null) 
            : base(dataLiquidacao, dataMovimentacao, valor, descricao, saldo, estorno)
        { }
    }
}
