namespace CSharp.MercadoFinanceiro.Daos
{
    public interface IAcao<TAcao>
    {
        decimal Quantidade { get; }
        TAcao TpAcao { get; }
    }
}