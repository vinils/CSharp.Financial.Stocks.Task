namespace CSharp.MercadoFinanceiro.Entities
{
    public interface IOperacao
    {
        decimal Quantidade { get; }
        decimal Total { get; }
        bool Equals(object obj);
        int GetHashCode();
    }
}