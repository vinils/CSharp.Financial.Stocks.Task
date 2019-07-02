namespace CSharp.MercadoFinanceiro.Daos
{
    using System.Collections.Generic;

    public interface IDao<T> : IEnumerable<T>
    {
        void Add(T data);
    }
}
