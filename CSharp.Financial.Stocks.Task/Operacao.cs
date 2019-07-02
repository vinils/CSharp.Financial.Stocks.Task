namespace CSharp.Financial.Stocks.Task
{
    using System.Collections.Generic;

    public class Operacao : MercadoFinanceiro.Daos.Operacao<Operacao>
    {
        private readonly List<Operacao> operacoes = new List<Operacao>();

        public decimal Preco => Total / Quantidade;

        public Operacao(int quantidade = 0, decimal total = 0)
        {
            Quantidade = quantidade;
            Total = total;
        }

        public void Add(Operacao operacao)
        {
            Quantidade += operacao.Quantidade;
            Total += operacao.Total;

            operacoes.Add(operacao);
        }
    }
}
