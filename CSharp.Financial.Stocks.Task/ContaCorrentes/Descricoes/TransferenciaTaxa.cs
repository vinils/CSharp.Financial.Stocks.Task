namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TransferenciaTaxa : Descricao
    {
        public static bool Is(string desc)
            => desc.Equals("TAXA TRANSF. DOC/TED");

        public static TransferenciaTaxa Cast(string desc)
        {
            if (!Is(desc))
                throw new ArgumentException();

            return new TransferenciaTaxa(desc);
        }
        public TransferenciaTaxa(string descricao) 
            : base(descricao)
        { }

    }
}
