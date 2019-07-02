namespace CSharp.Financial.Stocks.Task
{
    using System;

    public interface IAcertoConta
    {
        DateTime DataLiquidacao { get; }
        DateTime DataMovimentacao { get; }
        decimal Valor { get; }
    }
}
