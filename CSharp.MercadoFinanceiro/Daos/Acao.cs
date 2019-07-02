namespace CSharp.MercadoFinanceiro.Daos
{
    public abstract class Acao<TAcao> : Dao<TAcao>, IAcao<TAcao>
    {
        public virtual TAcao TpAcao { get; }
        public decimal Quantidade { get; protected set; }
        public Acao(TAcao tpAcao, decimal quantidade = 0)
        {
            TpAcao = tpAcao;
            Quantidade = quantidade;
        }

        public override void Add(TAcao acao)
            => Quantidade += 1;
    }
}
