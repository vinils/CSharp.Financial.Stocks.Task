namespace CSharp.MercadoFinanceiro.Entities
{
    public abstract class Cotacao : ICotacao
    {
        public decimal Preco { get; internal set; }

        public Cotacao(decimal preco)
        {
            Preco = preco;
        }
    }
}
