namespace CSharp.MercadoFinanceiro.Daos
{
    public abstract class Nota<TNota>  : Dao<TNota>, INota<TNota>
        //where TNota : Entities.INota
    { }
}
