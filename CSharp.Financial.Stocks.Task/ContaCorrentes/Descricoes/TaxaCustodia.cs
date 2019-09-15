namespace CSharp.Financial.Stocks.Task.ContaCorrentes.Descricoes
{
    using System;
    using System.Globalization;

    public class TaxaCustodia : Descricao
    {
        public static bool IsNormal(string desc)
            => desc.StartsWith("TAXA DE CUSTODIA ");
        public static bool IsEstornoComData(string desc)
            => desc.StartsWith("ESTORNO TAXA DE CUSTODIA ");
        public static bool IsEstornoSemData(string desc)
            => desc.Equals("Estorno Taxa de Custódia");
        public static TaxaCustodia Cast(string desc, TaxaCustodia estorno = null)
        {
            if (!(estorno == null && IsNormal(desc))
                && !IsEstornoComData(desc)
                && !(estorno != null && IsEstornoSemData(desc)))
                throw new ArgumentException();

            var periodo = desc.LastWord().Replace("Ref.:", "").GetValueOrNull<DateTime>(new CultureInfo("pt-BR"));

            var month = periodo?.Month;
            var year = periodo?.Year;

            if (estorno != null)
            {
                month = month ?? estorno.Month;
                year = year ?? estorno.Year;
            }

            if (!month.HasValue || !year.HasValue)
                throw new ArgumentNullException();

            return new TaxaCustodia(desc, month.Value, year.Value);
        }
        public static bool TryCast(string desc, out TaxaCustodia txCustodia, TaxaCustodia estorno = null)
        {
            try
            {
                txCustodia = Cast(desc, estorno);
                return true;
            }
            catch (Exception)
            {
                txCustodia = null;
                return false;
            }
        }

        public int Month { get; }
        public int Year { get; }

        public TaxaCustodia(string descricao, int month, int year) 
            : base(descricao)
        {
            this.Month = month;
            this.Year = year;
        }
    }
}
