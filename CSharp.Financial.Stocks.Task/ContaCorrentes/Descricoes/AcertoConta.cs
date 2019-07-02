namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AcertoConta
    {
        public static bool Is(string desc)
            => desc.Trim().StartsWith("Acerto Conta Corrente");
    }
}
