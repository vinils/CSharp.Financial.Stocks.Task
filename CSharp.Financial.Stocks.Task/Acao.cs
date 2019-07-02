namespace CSharp.Financial.Stocks.Task
{
    using System.Collections.Generic;
    using MercDaos = MercadoFinanceiro.Daos;

    public class Acao : MercDaos.Acao<TpAcao>
    {
        //public TpAcao TpAcao { get; }
            //=> base.TpAcao[base.TpAcao.Length-1] == 'F' ? base.TpAcao.Remove(base.TpAcao.Length) : base.TpAcao;

        public Acao(TpAcao codigo, int quantidade)
            :base(codigo, quantidade)
        { }

        public override IEnumerator<TpAcao> GetEnumerator()
        {
            for (var i = 0; i < Quantidade; i++)
                yield return TpAcao;
        }
    }
}
