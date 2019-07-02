namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EmolumentoFracao : Descricao
    {
        private const string descricaoSemQuantidadeStart = "Pagamento Frações sobre 0 de ";

        public static bool Is(string desc)
            => desc.StartsWith(descricaoSemQuantidadeStart);

        public static EmolumentoFracao Cast(string desc)
        {
            if (!Is(desc))
                throw new ArgumentException();

            var tipo = EmolumentoTipo.JCP;
            var codigoAcao = desc.LastWord();

            return new EmolumentoFracao(desc, codigoAcao, tipo);
        }

        public string CodigoAcao { get; }
        public EmolumentoTipo Tipo { get; }

        public EmolumentoFracao(string descricao, string codigoAcao, EmolumentoTipo tipo) 
            : base(descricao)
        {
            this.CodigoAcao = codigoAcao;
            this.Tipo = tipo;
        }
    }
}
