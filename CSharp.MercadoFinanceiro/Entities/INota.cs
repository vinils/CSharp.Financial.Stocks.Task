namespace CSharp.MercadoFinanceiro.Entities
{
    using System;

    public interface INota
    {
        DateTime Data { get; }
        decimal Valor { get; }
    }
}