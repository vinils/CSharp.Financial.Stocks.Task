namespace CSharp.MercadoFinanceiro.Entities
{
    using System;

    public interface IContaCorrente
    {
        DateTime DataLiquidacao { get; }
        DateTime DataMovimentacao { get; }
        string Descricao { get; }
        decimal Valor { get; }
    }
}