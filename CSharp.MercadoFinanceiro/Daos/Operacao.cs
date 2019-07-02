namespace CSharp.MercadoFinanceiro.Daos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class Operacao<TCotacoes> : IOperacao<TCotacoes>
        where TCotacoes : IOperacao<TCotacoes>
    {
        public decimal Quantidade { get; set; } = 0;
        public decimal Total { get; set; } = 0;

        public virtual void Add(decimal quantidade, decimal total)
        {
            Quantidade += quantidade;
            Total += total;
        }

        //public virtual void Add(long quantidade)
        //    => Quantidade += quantidade;

        //public override bool Equals(object obj)
        //    => obj is Operacao<TCotacoes> operacao &&
        //           Total == operacao.Total;

        //public override int GetHashCode()
        //{
        //    var hashCode = 1278364215;
        //    hashCode = hashCode * -1521134295 + Total.GetHashCode();
        //    return hashCode;
        //}


        //public IEnumerator<TCotacoes> GetEnumerator(long unit, Func<Operacao<TCotacoes>, TCotacoes> getCotacao)
        //{
        //    var units = Math.Abs(Quantidade / unit);

        //    for (var i = 0; i < units; i++)
        //    {
        //        yield return getCotacao(this);
        //    }
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //    => GetEnumerator();

        //public static Operacao<TCotacoes> operator +(Operacao<TCotacoes> operacao, long quantity)
        //{
        //    operacao.Add(quantity);
        //    return operacao;
        //}

        //public static Operacao<TCotacoes> operator -(Operacao<TCotacoes> operacao, long quantity)
        //{
        //    operacao.Add(quantity *-1);
        //    return operacao;
        //}
    }
}
