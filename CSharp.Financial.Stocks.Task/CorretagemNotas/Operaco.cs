namespace CSharp.Financial.Stocks.Task.CorretagemNotas
{
    using CSharp.MercadoFinanceiro.Daos;
    using CSharp.MercadoFinanceiro.Entities;

    public class Operacao : IAcao<TpAcao>
    {
        private readonly Task.Operacao operacao;
        public TpAcao TpAcao { get; }
        public decimal Quantidade => operacao.Quantidade;
        public decimal Valor { get => operacao.Total; protected set => operacao.Total = value; }

        public Operacao(TpAcao tpAcao,
            Task.Operacao operacao)
        {
            TpAcao = tpAcao;
            this.operacao = operacao;
        }
    }
}
