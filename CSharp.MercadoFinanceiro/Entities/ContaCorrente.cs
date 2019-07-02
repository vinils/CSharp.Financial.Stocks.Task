namespace CSharp.MercadoFinanceiro.Entities
{
    using System;

    public class ContaCorrente : IContaCorrente
    {
        public DateTime DataLiquidacao { get; }
        public DateTime DataMovimentacao { get; }
        public string Descricao { get; }
        public decimal Valor { get; }
        public decimal Saldo { get; }
        public bool IsEntrada => Valor > 0;
        public bool IsSaida => !IsEntrada;

        public ContaCorrente(DateTime dataLiquidacao, DateTime dataMovimentacao, decimal valor, decimal saldo, string descricao)
        {
            DataLiquidacao = dataLiquidacao;
            DataMovimentacao = dataMovimentacao;
            Valor = valor;
            Saldo = saldo;
            Descricao = descricao;
        }
    }
}
