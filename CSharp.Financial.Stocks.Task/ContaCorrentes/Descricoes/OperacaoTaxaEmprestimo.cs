namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    using System;

    public class OperacaoTaxaEmprestimo : Descricao
    {
        private const string strStart = "Taxa de Empréstimo de Ações sobre ";
        public static bool Is(string desc)
            => desc.StartsWith(strStart);
        public static OperacaoTaxaEmprestimo Cast(string desc)
        {
            if (!Is(desc))
                throw new ArgumentException();

            var quantidade = Convert.ToInt32(desc.NextWord(strStart.Length));
            var codigoAcao = desc.LastWord();

            if (quantidade == 0 || string.IsNullOrWhiteSpace(codigoAcao))
                throw new NotImplementedException();

            var acao = new Acao(codigoAcao, quantidade);
            return new OperacaoTaxaEmprestimo(desc, acao);
        }

        public Acao Acao { get; }

        public OperacaoTaxaEmprestimo(string descricao, Acao acao) 
            : base(descricao)
        {
            Acao = acao;
        }
    }
}
