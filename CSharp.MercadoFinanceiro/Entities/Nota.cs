namespace CSharp.MercadoFinanceiro.Entities
{
    using System;

    public abstract class Nota : INota
    {
        public abstract DateTime Data { get; }
        public abstract decimal Valor { get; protected set; }

        public virtual void Add(decimal valor)
            => Valor += valor;
    }
}
