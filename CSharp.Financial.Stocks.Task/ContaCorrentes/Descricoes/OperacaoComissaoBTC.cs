namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    using System;

    public class OperacaoComissaoBTC : Descricao
    {
        private const string strStart = "Comissão BTC sobre ";
        public static bool Is(string desc)
            => desc.StartsWith(strStart);
        public static OperacaoComissaoBTC Cast(string desc)
        {
            if (!Is(desc))
                throw new ArgumentException();

            var quantidade = Convert.ToInt32(desc.NextWord(strStart.Length));
            var codigoAcao = desc.LastWord();

            if (quantidade == 0 || string.IsNullOrWhiteSpace(codigoAcao))
                throw new NotImplementedException();

            var acao = new Acao(codigoAcao, quantidade);
            return new OperacaoComissaoBTC(desc, acao);
        }

        public Acao Acao { get; }

        public OperacaoComissaoBTC(string descricao, Acao acao) 
            : base(descricao)
        {
            this.Acao = acao;
        }
    }
}
