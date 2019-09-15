namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    using System;

    public class Nota : Descricao
    {
        private const string descriptionStart = "Líquido Operações Bovespa ";

        public static bool Is(string desc)
            => desc.StartsWith(descriptionStart);

        public static Nota Cast(string desc)
        {
            if (!Is(desc))
                throw new ArgumentException();

            var nrNotaStrStart = desc.Substring(descriptionStart.Length);
            var nrNotaEndIndex = nrNotaStrStart.IndexOf(' ');
            var nrNota = Convert.ToInt32(nrNotaStrStart.Substring(0, nrNotaEndIndex));
            var pregao = Convert.ToDateTime(nrNotaStrStart.Substring(nrNotaStrStart.Length - 10, 10), new System.Globalization.CultureInfo("pt-BR"));

            return new Nota(desc, nrNota, pregao);
        }

        public int NrNota { get; }
        public DateTime Pregao { get; }

        public Nota(string descricao, int nrNota, DateTime pregao)
            : base(descricao)
        {
            NrNota = nrNota;
            Pregao = pregao;
        }

    }
}
