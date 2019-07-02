namespace CSharp.Financial.Stocks.Task.CorretagemNotas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CSharp.MercadoFinanceiro.Entities;

    public class Nota : MercadoFinanceiro.Daos.Nota<Operacao>, MercadoFinanceiro.Entities.INota
    {
        private readonly List<Operacao> notas = new List<Operacao>();

        public DateTime Data { get; }
        public decimal VendasAVista { get; }
        public decimal VendasAVistaOp => notas.Where(n => n.Valor < 0).Sum(n => n.Valor) * -1;
        public decimal IRRFSobreVendDayTrade { get; }
        public decimal ComprasAVista { get; }
        public decimal ComprasAVistaOp => notas.Where(n => n.Valor > 0).Sum(n => n.Valor);
        public decimal Operacao { get; }
        private decimal OperacaoOp => VendasAVista + ComprasAVista;
        public decimal VlLiquidoOperacao { get; }
        public decimal VlLiquidoOperacaoOp => VendasAVistaOp - ComprasAVistaOp;
        public decimal VlTxLiquidacao { get; }
        //public decimal TotalA { get; }
        public decimal TotalAOp => VlLiquidoOperacaoOp - VlTxLiquidacao;
        public decimal Emolumentos { get; }
        //public decimal TotalB { get; }
        public decimal CorretagemSemIss => CorretagemMaisIss - CorretagemIss;
        public decimal CorretagemIss { get; set;  }
        public decimal TxCorretagemIss => CorretagemIss / CorretagemSemIss;
        public decimal CorretagemMaisIss { get; }
        public decimal ValorLiquidacao { get; }
        public decimal ValorImpostos => VlTxLiquidacao + Emolumentos + IRRFSobreVendDayTrade + CorretagemSemIss + CorretagemIss;
        public decimal ValorLiquidacaoOp => VendasAVistaOp - ComprasAVistaOp - ValorImpostos;

        DateTime INota.Data => Data;
        decimal INota.Valor => ValorLiquidacao;

        public Nota(DateTime data,
                    decimal vendasAVista,
                    decimal iRRFSobreVendDayTrade,
                    decimal compraAVista,
                    decimal operacao,
                    decimal vlLiquidoOperacao,
                    decimal taxaLiquidacao,
                    decimal totalA,
                    decimal emolumentos,
                    decimal totalB,
                    decimal corretagemIss,
                    decimal corretagemMaisIss,
                    decimal valorLiquidacao)
        {
            Data = data;
            VendasAVista = vendasAVista;
            IRRFSobreVendDayTrade = iRRFSobreVendDayTrade;
            ComprasAVista = compraAVista;
            Operacao = operacao;
            VlLiquidoOperacao = vlLiquidoOperacao;
            VlTxLiquidacao = taxaLiquidacao;
            //TotalA = totalA;
            Emolumentos = emolumentos;
            //TotalB = totalB;
            CorretagemIss = corretagemIss;
            CorretagemMaisIss = corretagemMaisIss;
            ValorLiquidacao = valorLiquidacao;
        }

        public override void Add(Operacao nota)
            => notas.Add(nota);

        public int GetNumberOfOperations()
            => notas.Count();

        public int GetNumberOfBuyOperations()
            => notas.Count(n => n.Valor > 0);

        public int GetNumberOfSalesOperations()
            => notas.Count(n => n.Valor < 0);

        public bool IsVendaOnly()
            => GetNumberOfOperations() == GetNumberOfSalesOperations();

        public bool IsCompraOnly()
            => GetNumberOfOperations() == GetNumberOfBuyOperations();

        public bool IsMixed()
            => GetNumberOfSalesOperations() > 0 && GetNumberOfBuyOperations() > 0;

        public bool IsOperationCorretagemSemIssRight(decimal expectedOperationCorretagemSemISS)
            => GetNumberOfOperations() * expectedOperationCorretagemSemISS == CorretagemSemIss;
        public bool IsOperationCorretagemIssRight(decimal expectedOperationCorretagemISS)
            => GetNumberOfOperations() * expectedOperationCorretagemISS == CorretagemIss;

        public bool IsCompraAVistaRight()
            => ComprasAVistaOp == ComprasAVista;

        public bool IsVendaAVistaRight()
            => VendasAVistaOp == VendasAVista;

        public bool IsValorLiquidoOperacaoRight()
            => VlLiquidoOperacao == VlLiquidoOperacao;
        public bool IsOperacaoRight()
            => OperacaoOp == Operacao;
        //public bool IsTotalARight()
        //    => TotalAOp == TotalA;
        //public bool IsTotalBRight()
        //    => Emolumentos * -1 == TotalB;
        public bool IsLiquidacaoRight()
            => ValorLiquidacaoOp == ValorLiquidacao;


        public override IEnumerator<Operacao> GetEnumerator()
        {
            return notas.GetEnumerator();
        }
    }
}
