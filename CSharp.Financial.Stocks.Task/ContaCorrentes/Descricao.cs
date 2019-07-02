namespace CSharp.Financial.Stocks.Task.ContaCorrentes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Descricao
    {
        public static bool IsNota(string desc)
            => Descricoes.Nota.Is(desc);
        public static Descricoes.Nota CastToNota(string desc)
            => Descricoes.Nota.Cast(desc);
        public static bool IsTransferencia(string desc)
            => Descricoes.Transferencia.Is(desc);
        public static Descricoes.Transferencia CastToTransferencia(string desc)
            => Descricoes.Transferencia.Cast(desc);
        public static bool IsPendenciaNormal(string desc)
            => Descricoes.Pendencia.IsNormal(desc);
        public static bool IsPendenciaEstorno(string desc)
            => Descricoes.Pendencia.IsEstorno(desc);
        public static Descricoes.Pendencia CastToPendencia(string desc)
            => Descricoes.Pendencia.Cast(desc);
        public static bool IsMargemNormal(string desc)
            => Descricoes.Margem.IsNormal(desc);
        public static bool IsMargemEstorno(string desc)
            => Descricoes.Margem.IsEstorno(desc);
        public static Descricoes.Margem CastToMargem(string desc, bool isEstorno = false)
            => Descricoes.Margem.Cast(desc, isEstorno);
        //public static bool TryCastToEmolumento(string desc, out Descricoes.Emolumento emolumento, Descricoes.Emolumento estorno = null)
        //    => Descricoes.Emolumento.TryCast(desc, out emolumento, estorno);
        public static bool IsEmolumentoEstornoSemQuantidade(string desc)
            => Descricoes.Emolumento.IsEstornoSemQuantidade(desc);
        public static Descricoes.Emolumento CastToEmolumento(string desc, Descricoes.Emolumento estorno = null)
            => Descricoes.Emolumento.Cast(desc, estorno);
        public static bool IsEmolumentoFracao(string desc)
            => Descricoes.EmolumentoFracao.Is(desc);
        public static Descricoes.EmolumentoFracao CastToEmolumentoFracao(string desc)
            => Descricoes.EmolumentoFracao.Cast(desc);
        public static bool IsTaxaCustodiaNormal(string desc)
            => Descricoes.TaxaCustodia.IsNormal(desc);
        public static Descricoes.TaxaCustodia CastToTaxaCustodia(string desc, Descricoes.TaxaCustodia estorno = null)
            => Descricoes.TaxaCustodia.Cast(desc, estorno);
        public static bool IsTaxaCustodiaEstornoComData(string desc)
            => Descricoes.TaxaCustodia.IsEstornoComData(desc);
        public static Descricoes.TaxaCustodia CastToTaxaCustodiaWhenHasDate(string desc)
            => Descricoes.TaxaCustodia.Cast(desc);
        public static bool IsTaxaCustodiaEstornoSemData(string desc)
            => Descricoes.TaxaCustodia.IsEstornoSemData(desc);
        public static Descricoes.TaxaCustodia CastToTaxaCustodiaWhenHasNoDate(string desc, Descricoes.TaxaCustodia estorno)
            => Descricoes.TaxaCustodia.Cast(desc, estorno);
        public static bool IsTransferenciaTaxa(string desc)
            => Descricoes.TransferenciaTaxa.Is(desc);
        public static Descricoes.TransferenciaTaxa CastToTransferenciaTaxa(string desc)
            => Descricoes.TransferenciaTaxa.Cast(desc);
        public static bool IsOperacaoComissaoBTC(string desc)
            => Descricoes.OperacaoComissaoBTC.Is(desc);
        public static Descricoes.OperacaoComissaoBTC CastToOperacaoComissaoBTC(string desc)
            => Descricoes.OperacaoComissaoBTC.Cast(desc);
        public static bool IsNotaIRRF(string desc)
            => Descricoes.NotaIRRF.Is(desc);
        public static Descricoes.NotaIRRF CastToNotaIRRF(string desc)
            => Descricoes.NotaIRRF.Cast(desc);
        public static bool IsOperacaoTaxaEmprestimo(string desc)
            => Descricoes.OperacaoTaxaEmprestimo.Is(desc);
        public static Descricoes.OperacaoTaxaEmprestimo CastToOperacaoTaxaEmprestimo(string desc)
            => Descricoes.OperacaoTaxaEmprestimo.Cast(desc);
        public static bool IsAcertoConta(string desc)
            => Descricoes.AcertoConta.Is(desc);



        private readonly string descricao;

        public Descricao(string descricao)
        {
            this.descricao = descricao;
        }

        //public bool TryCast(out Descricoes.Nota nota)
        //    => Descricoes.Nota.TryCast(descricao, out nota);

        public override bool Equals(object obj)
            => obj is Descricao descricao &&
                   this.descricao == descricao.descricao;

        public override int GetHashCode()
            => -253325366 + EqualityComparer<string>.Default.GetHashCode(descricao);

        public override string ToString()
            => descricao.ToString();

        public static implicit operator Descricao(string desc)
            => new Descricao(desc);

        public static implicit operator string(Descricao desc)
            => desc.descricao;

        //internal Descricao ToLower()
        //    => new Descricao(descricao.ToLower());

        //internal bool EndsWith(string v)
        //    => descricao.EndsWith(v);

        //internal Descricao Normalize(NormalizationForm normalizationForm)
        //    => descricao.Normalize(normalizationForm);

        //internal bool StartsWith(string descriptionStart)
        //    => descricao.StartsWith(descriptionStart);

        //internal Descricao Substring(int length)
        //    => descricao.Substring(length);
        //internal Descricao Substring(int startIndex, int length)
        //    => descricao.Substring(startIndex, length);
        //internal int IndexOf(char value)
        //    => descricao.IndexOf(value);
    }
}
