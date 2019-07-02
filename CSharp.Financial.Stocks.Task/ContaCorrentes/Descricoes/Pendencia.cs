namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    using System;

    public class Pendencia : Descricao
    {
        public static bool IsNormal(string desc)
            => IsNormal(desc, out _);

        private static bool IsNormal(string desc, out int length)
        {
            const string descrciptionStart = "Pendência de Compra ";
            length = descrciptionStart.Length;
            return desc.StartsWith(descrciptionStart);
        }

        public static bool IsEstorno(string desc)
            => IsEstorno(desc, out _);
        private static bool IsEstorno(string desc, out int length)
        {
            const string descricaoStart = "Regularização de Compra ";
            length = descricaoStart.Length;
            return desc.StartsWith(descricaoStart);
        }

        public static Pendencia Cast(string desc)
        {
            int? length = null;

            if (IsNormal(desc, out int lengthN))
                length = lengthN;
            else if (IsEstorno(desc, out int lengthE))
                length = lengthE;
            
            if(!length.HasValue)
                throw new ArgumentException();

            var strMeio = " DE ";
            var pregao = Convert.ToDateTime(desc.NextWord(length.Value));
            var codigoAcao = desc.LastWord();
            var quantidade = Convert.ToInt32(desc.LastWord(desc.Length - codigoAcao.Length - strMeio.Length));

            var acao = new Acao(codigoAcao, quantidade);
            return new Pendencia(desc, pregao, acao);
        }

        public DateTime Pregao { get; }
        public Acao Acao { get; }
        public Pendencia(string descricao, DateTime pregao, Acao acao) 
            : base(descricao)
        {
            this.Pregao = pregao;
            this.Acao = acao;
        }
    }
}
