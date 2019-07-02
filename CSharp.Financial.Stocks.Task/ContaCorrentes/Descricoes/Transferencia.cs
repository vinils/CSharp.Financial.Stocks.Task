using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    public class Transferencia : Descricao
    {
        private static readonly string[] texts = {
            "Credito C/C", "TED - Crédito em conta",
            "TED - CREDITO EM C/C",
            "DOC - Crédito em conta",
            "DOC - CREDITO EM C/C" };

        public static bool Is(string desc)
            => texts.Any(t => t.Equals(desc))
            || desc.ToLower().RemoveDiacritics().Contains("credito em ")
            || desc.EndsWith(" Débito em Conta");

        public static Transferencia Cast(string desc)
        {
            if (!Is(desc))
                throw new ArgumentException();

            return new Transferencia(desc);
        }

        public Transferencia(string descricao) 
            : base(descricao)
        { }
    }
}
