namespace CSharp.MercadoFinanceiro.Daos
{
    public abstract class DaoRecurrent<T> : Dao<T>, IDao<T>
        where T : Dao<T>, IDao<T>
    { }
}
