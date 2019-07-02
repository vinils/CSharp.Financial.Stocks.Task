namespace CSharp.Financial.Stocks.Task
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ContaCorretenteExtension
    {
        public static void ForEachUniqueDtLiquidacao<T>(this IEnumerable<ContaCorrente<T>> ccs, Action<DateTime, ContaCorrente<T>> action)
            where T : ContaCorrentes.Descricao
        {
            foreach (var groupedComissoesBtc in ccs.GroupBy(op => op.DataLiquidacao))
            {
                var repeatedCount = 0;
                foreach (var op in groupedComissoesBtc)
                {
                    var data = op.DataLiquidacao.AddSeconds(repeatedCount);
                    action(data, op);
                    repeatedCount += 1;
                }
            }
        }
        public static void Add<T>(this List<MercadoFinanceiro.Entities.ContaCorrente> ccs,
                                  DateTime dataLiquidacao,
                                  DateTime dataMovimentacao,
                                  decimal valor,
                                  T descricao,
                                  decimal saldo,
                                  ContaCorrente<T> estorno = null)
            where T : ContaCorrentes.Descricao
        {
            var newCC = new ContaCorrente<T>(dataLiquidacao, dataMovimentacao, valor, descricao, saldo, estorno);
            ccs.Add(newCC);
        }
        public static IEnumerable<T> PossibleReverse<T, Y>(this IEnumerable<T> ccs, DateTime date, decimal valor)
            where T : ContaCorrente<Y>
            where Y : ContaCorrentes.Descricao
            => ccs
                .Where(e => e.Valor == valor * -1
                            && e.Estornado == null
                            && e.DataLiquidacao <= date)
                .OrderBy(e => e.DataLiquidacao);
        public static IEnumerable<T> PossibleReverse<T, Y>(this IEnumerable<T> ccs, MercadoFinanceiro.Entities.ContaCorrente estorno)
            where T : ContaCorrente<Y>
            where Y : ContaCorrentes.Descricao
            => PossibleReverse<T, Y>(ccs, estorno.DataLiquidacao, estorno.Valor);

        public static IEnumerable<ContaCorrente<T>> PossibleReverse<T>(this IEnumerable<ContaCorrente<T>> ccs, MercadoFinanceiro.Entities.ContaCorrente estorno)
            where T : ContaCorrentes.Descricao
            => PossibleReverse<ContaCorrente<T>, T>(ccs, estorno);
        //public static IEnumerable<ContaCorrentes.Nota> PossibleReverse(this List<ContaCorrentes.Nota> ccs, MercadoFinanceiro.Entities.ContaCorrente estorno)
        //    => PossibleReverse<ContaCorrentes.Nota, ContaCorrentes.Descricoes.Nota>(ccs, estorno.DataLiquidacao, estorno.Valor);
    }
}