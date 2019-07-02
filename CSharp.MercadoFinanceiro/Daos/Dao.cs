namespace CSharp.MercadoFinanceiro.Daos
{
    using System.Collections;
    using System.Collections.Generic;

    public abstract class Dao<T> : IDao<T>
    {
        public abstract void Add(T data);
        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
