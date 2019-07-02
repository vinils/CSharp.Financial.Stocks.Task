using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    public class Margem : Descricao
    {
        public static bool IsNormal(string desc)
            => desc.Equals("Chamada de Margem Opções/Termo/a Vista");

        public static bool IsEstorno(string desc)
            => desc.StartsWith("Estorno de Margem sobre ")
            || desc.Equals("Liberação de Margem Opções/Termo/Vista");

        public static Margem Cast(string desc, bool isEstorno = false)
        {
            if (!(isEstorno && IsEstorno(desc))
                && !(!isEstorno && IsNormal(desc)))
                throw new ArgumentException();

            return new Margem(desc);
        }

        public Margem(string descricao) 
            : base(descricao)
        { }
    }
}
