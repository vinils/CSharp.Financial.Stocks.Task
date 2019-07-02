namespace CSharp.MercadoFinanceiro.Daos
{
    public abstract class Pregao<TPregao> : Dao<TPregao>, IPregao<TPregao>
        where TPregao: Entities.IPregao
    { }
}
