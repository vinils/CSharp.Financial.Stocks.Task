namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NotaIRRF : Descricao
    {
        private const string strStart = "IRRF sobre Operações Pregão de ";
        public static bool Is(string desc)
            => desc.StartsWith(strStart);
        public static NotaIRRF Cast(string desc)
        {
            if (!Is(desc))
                throw new ArgumentException();

            var pregao = Convert.ToDateTime(desc.Substring(strStart.Length), new System.Globalization.CultureInfo("pt-BR"));

            return new NotaIRRF(desc, pregao);
        }

        public DateTime pregao { get; }

        public NotaIRRF(string descricao, DateTime pregao)
            : base(descricao)
        {
            this.pregao = pregao;
        }
    }
}
