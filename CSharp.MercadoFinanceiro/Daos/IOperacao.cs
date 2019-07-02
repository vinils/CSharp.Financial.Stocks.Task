namespace CSharp.MercadoFinanceiro.Daos
{
    public interface IOperacao<TCotacao> : Entities.IOperacao
        where TCotacao : IOperacao<TCotacao>
    { }
}