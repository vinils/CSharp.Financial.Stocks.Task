namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    using System;

    public enum EmolumentoTipo
    {
        JCP,
        DIV
    }

    public class Emolumento : Descricao
    {
        private static readonly string[] normalStartTexts = {
            "Juros sobre Capital Próprio sobre ",
            "Juros sobre Capital Próprio ",
            "Rendimentos sobre ",
            "Dividendos sobre "
        };
        private static bool IsNormal(string desc, out int quantidade, out EmolumentoTipo tipo)
        {
            var txtIdx = Array.FindIndex(normalStartTexts, d => desc.StartsWith(d));
            var textLength = normalStartTexts[txtIdx].Length;
            tipo = txtIdx != 3 ? EmolumentoTipo.JCP : EmolumentoTipo.DIV;
            quantidade = Convert.ToInt32(desc.NextWord(textLength).Replace(".", ""));

            return true;

        }
        private static bool IsEstornoSemQuantidade(string desc, out EmolumentoTipo tipo)
        {
            tipo = EmolumentoTipo.JCP;
            return desc.StartsWith("Estorno Sobre juros ");
        }
        internal static bool IsEstornoSemQuantidade(string desc)
            => IsEstornoSemQuantidade(desc, out _);
        public static Emolumento Cast(string desc, Emolumento estorno = null)
        {
            EmolumentoTipo? tipo = null;
            int? quantidade = null;
            var codigoAcao = desc.LastWord();

            if (IsEstornoSemQuantidade(desc, out EmolumentoTipo tipoES))
            {
                tipo = tipoES;
            }
            else if (IsNormal(desc, out int quantidadeN, out EmolumentoTipo tipoN))
            {
                quantidade = quantidadeN;
                tipo = tipoN;
            }

            if(estorno != null)
            {
                if((quantidade.HasValue && quantidade != estorno.Acao.Quantidade)
                    || (tipo.HasValue && tipo != estorno.Tipo)
                    || (string.IsNullOrEmpty(codigoAcao) && codigoAcao != estorno.Acao.TpAcao))
                    throw new ArgumentException();

                codigoAcao = codigoAcao ?? estorno.Acao.TpAcao;
                quantidade = quantidade ?? (int?)estorno.Acao.Quantidade;
                tipo = tipo ?? (EmolumentoTipo?)estorno.Tipo;
            }

            if (!quantidade.HasValue || !tipo.HasValue || string.IsNullOrEmpty(codigoAcao))
                throw new ArgumentException();

            var acao = new Acao(codigoAcao, quantidade.Value);
            return new Emolumento(desc, acao, tipo.Value);
        }

        //public static bool TryCast(string desc, out Emolumento emolumento, Emolumento estorno = null)
        //{
        //    try
        //    {
        //        emolumento = Cast(desc, estorno);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        emolumento = null;
        //        return false;
        //    }
        //}

        public Acao Acao { get; }
        public EmolumentoTipo Tipo { get; }

        public Emolumento(string descricao, Acao acao, EmolumentoTipo tipo) 
            : base(descricao)
        {
            Acao = acao;
            Tipo = tipo;
        }
    }
}
