namespace CSharp.Financial.Stocks.Task
{
    using CSharp.Financial.Stocks.Task.ContaCorrentes;
    using System;

    public class ContaCorrente<TDesc> : MercadoFinanceiro.Entities.ContaCorrente
        where TDesc : Descricao
    {
        public static bool IsNota(string desc)
            => ContaCorrentes.Descricao.IsNota(desc);

        public ContaCorrente<TDesc> Estornado { get; private set; }
        public ContaCorrente<TDesc> Estorno { get; }

        public new TDesc Descricao { get; }
        public ContaCorrente(DateTime dataLiquidacao,
                             DateTime dataMovimentacao,
                             decimal valor,
                             TDesc descricao,
                             decimal saldo,
                             ContaCorrente<TDesc> estorno = null)
            : base(dataLiquidacao, dataMovimentacao, valor, saldo, descricao)
        {
            Descricao = descricao;

            if (estorno != null)
            {
                Estorno = estorno;

                if (estorno.DataLiquidacao > DataLiquidacao || estorno.Valor != valor * -1)
                    throw new Exception("Wrong estno data or value");

                estorno.Estornado = this;
            }
        }
    }
}
