namespace CSharp.MercadoFinanceiro.Daos
{
    public interface IPregao<TPregao> : IDao<TPregao>
        where TPregao : Entities.IPregao
    { }
}