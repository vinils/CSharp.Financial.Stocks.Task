namespace CSharp.Financial.Stocks.Task
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class Program
    {
        public class DataAtivaTrade
        {
            public DateTime Date { get; set; }
            public DictionaryTree<string, string> Group { get; set; }
            public decimal Value { get; set; }
        }

        static void Main(string[] args)
        {
            //// The code provided will print ‘Hello World’ to the console.
            //// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            //Console.WriteLine("Hello World!");
            //Console.ReadKey();

            var contaCorrente = new ContaCorrenteCollection(@"U:\DADOS\Pessoal\Documentos\Financeiro\ComprovantesInvestimentosAcoes\ExtratoContaCorrente\20070730_20190426.xls");

            var transferencias = contaCorrente.Transferencias;
            var transferenciasEstornos = transferencias.Where(t => t.Estorno != null);
            var emolumentos = contaCorrente.Emolumentos;
            var emolumentoFracoes = contaCorrente.EmolumentoFracoes;
            var ccNotas = contaCorrente.Notas;
            var operacaoComissoesBTCs = contaCorrente.OperacaoBTCs;
            var operacaoEmprestimoTaxas = contaCorrente.OperacaoEmprestimoTaxas;
            var notaIRRFs = contaCorrente.NotaIRRFs;
            var margens = contaCorrente.Margens;
            var transferenciasTaxas = contaCorrente.TransferenciaTaxas;
            var txCustodias = contaCorrente.CustodiaTaxas;
            var pendenciaCompra = contaCorrente.Pendencias;

            txCustodias.ForEachUniqueDtLiquidacao((uniqueDate, cc) =>
            {
                AtivaTradeData
                .AddDespesaGeral("TaxaCustodia",
                                   uniqueDate,
                                   cc.Valor,
                                   cc.Estornado != null,
                                   cc.Estorno != null);
            });

            transferenciasTaxas.ForEachUniqueDtLiquidacao((uniqueDate, cc) =>
            {
                AtivaTradeData
                .AddDespesaGeral("TaxaTransferência",
                                   uniqueDate,
                                   cc.Valor,
                                   cc.Estornado != null,
                                   cc.Estorno != null);
            });

            transferencias.ForEachUniqueDtLiquidacao((uniqueDate, cc) =>
            {
                AtivaTradeData
                .AddTransferencia(uniqueDate,
                                  cc.Valor * -1,
                                  cc.Estornado != null,
                                  cc.Estorno != null);
            });

            operacaoComissoesBTCs.ForEachUniqueDtLiquidacao((uniqueDate, cc) =>
            {
                AtivaTradeData
                .AddDespesa("ComissaoBtc",
                            uniqueDate,
                            cc.Descricao.Acao.TpAcao,
                            cc.Descricao.Acao.Quantidade,
                            cc.Valor);
            });

            operacaoEmprestimoTaxas.ForEachUniqueDtLiquidacao((uniqueDate, cc) =>
            {
                AtivaTradeData
                .AddDespesa("TaxaEmprestimo",
                            uniqueDate,
                            cc.Descricao.Acao.TpAcao,
                            cc.Descricao.Acao.Quantidade,
                            cc.Valor);
            });

            notaIRRFs.ForEachUniqueDtLiquidacao((uniqueDate, cc) =>
            {
                if (cc.Descricao == "IRRF sobre Operações Pregão de 30/09/2008")
                    AtivaTradeData
                    .AddDespesa("NotaIRRF",
                                uniqueDate,
                                "PETR4",
                                200,
                                cc.Valor);
                else if (cc.Descricao == "IRRF sobre Operações Pregão de 22/09/2008")
                    AtivaTradeData
                    .AddDespesa("NotaIRRF",
                                uniqueDate,
                                "PETR4",
                                200,
                                cc.Valor);
                else
                    throw new NotImplementedException();
            });

            margens.ForEachUniqueDtLiquidacao((uniqueDate, cc) =>
            {
                if (cc.DataLiquidacao == new DateTime(2008, 09, 25)
                    && cc.Descricao == "Estorno de Margem sobre 368217")
                    AtivaTradeData
                    .AddMargem(uniqueDate,
                                "PETR4",
                                200,
                                cc.Valor);
                else if (cc.DataLiquidacao == new DateTime(2008, 09, 25)
                    && cc.Descricao == "Chamada de Margem Opções/Termo/a Vista")
                    AtivaTradeData
                    .AddMargem(uniqueDate,
                                "PETR4",
                                200,
                                cc.Valor);
                else if (cc.DataLiquidacao == new DateTime(2008, 09, 25)
                    && cc.Descricao == "Chamada de Margem Opções/Termo/a Vista")
                    AtivaTradeData
                    .AddMargem(uniqueDate,
                                "PETR4",
                                200,
                                cc.Valor);
                else if (cc.DataLiquidacao == new DateTime(2008, 09, 26)
                    && cc.Descricao == "Chamada de Margem Opções/Termo/a Vista")
                    AtivaTradeData
                    .AddMargem(uniqueDate,
                                "PETR4",
                                200,
                                cc.Valor);
                else if (cc.DataLiquidacao == new DateTime(2008, 09, 29)
                    && cc.Descricao == "Liberação de Margem Opções/Termo/Vista")
                    AtivaTradeData
                    .AddMargem(uniqueDate,
                                "PETR4",
                                200,
                                cc.Valor);
                else
                    throw new NotImplementedException();
            });

            pendenciaCompra.ForEachUniqueDtLiquidacao((uniqueDate, cc) =>
            {
                AtivaTradeData
                .AddPendenciaCompra(uniqueDate,
                            cc.Descricao.Acao.TpAcao,
                            cc.Descricao.Acao.Quantidade,
                            cc.Valor);
            });

            if (emolumentoFracoes.Any(ef => !(ef.Descricao.CodigoAcao == "ITAUUNIBANCO" 
                                            && ef.DataLiquidacao >= new DateTime(2011, 11, 30) 
                                            && ef.DataLiquidacao <= new DateTime(2016, 12, 09))))
                throw new NotImplementedException();

            ContaCorrente<ContaCorrentes.Descricoes.Emolumento> cast(
                ContaCorrente<ContaCorrentes.Descricoes.EmolumentoFracao> emoFra,
                string acao,
                int quantidade)
            {
                var estorno = emoFra.Estorno != null ? cast(emoFra.Estorno, acao, quantidade) : null;
                var acao2 = new Acao(acao, quantidade);
                var emoDesc = new ContaCorrentes.Descricoes.Emolumento(emoFra.Descricao,
                                                                       acao2,
                                                                       emoFra.Descricao.Tipo);

                return new ContaCorrente<ContaCorrentes.Descricoes.Emolumento>(emoFra.DataLiquidacao,
                                                                               emoFra.DataMovimentacao,
                                                                               emoFra.Valor,
                                                                               emoDesc,
                                                                               emoFra.Saldo,
                                                                               estorno);
            }

            foreach (var emoFra in emolumentoFracoes)
            {
                ContaCorrente<ContaCorrentes.Descricoes.Emolumento> newEmo;

                if (emoFra.DataLiquidacao == new DateTime(2011, 11, 30))
                    newEmo = cast(emoFra, "ITUB4", 300);
                else if (emoFra.DataLiquidacao == new DateTime(2015, 09, 08))
                    newEmo = cast(emoFra, "ITUB4", 399);
                else if (emoFra.DataLiquidacao == new DateTime(2016, 12, 09))
                    newEmo = cast(emoFra, "ITUB4", 438);
                else
                    throw new NotImplementedException();

                emolumentos.Add(newEmo);
            }


            emolumentos.ForEachUniqueDtLiquidacao((uniqueDate, cc) =>
            {
                AtivaTradeData
                .AddEmolumentos(uniqueDate,
                                cc.Descricao.Acao.TpAcao,
                                cc.Descricao.Tipo,
                                cc.Descricao.Acao.Quantidade,
                                cc.Valor,
                                cc.Estorno != null,
                                cc.Estornado != null);
            });


            /////////////////////////////////////////////////

            var corretagem = new CorretagemNota(
                new DirectoryInfo(@"U:\DADOS\Pessoal\Documentos\Financeiro\ComprovantesInvestimentosAcoes\NotasCorretagem"),
                60);

            var crgNotas = corretagem;

            if (crgNotas.Where(crg => !crg.IsCompraAVistaRight()).Count() > 0)
                throw new NotImplementedException();
            if (crgNotas.Where(crg => !crg.IsVendaAVistaRight()).Count() > 0)
                throw new NotImplementedException();
            if (crgNotas.Where(crg => !crg.IsValorLiquidoOperacaoRight()).Count() > 0)
                throw new NotImplementedException();
            if (crgNotas.Where(crg => !crg.IsOperacaoRight()).Count() > 0)
                throw new NotImplementedException();
            //if (crgNotas.Where(crg => !crg.IsTotalARight()).Count() > 0)
            //    throw new NotImplementedException();
            //if (crgNotas.Where(crg => !crg.IsTotalBRight()).Count() > 0)
            //    throw new NotImplementedException();
            if (crgNotas.Where(crg => !crg.IsLiquidacaoRight()).Count() > 0)
                throw new NotImplementedException();

            var notasComProblemaNaCorretagem = crgNotas.Where(n => n.CorretagemSemIss == 14.99m);
            foreach (var n in notasComProblemaNaCorretagem)
                n.CorretagemIss -= 0.01m;

            var corretagensProblema = crgNotas
                .Where(n => !n.IsOperationCorretagemSemIssRight(15m));

            //if (corretagensProblema.Count() > 0)
            //    throw new NotImplementedException();

            var notas = from CC in ccNotas
                        join CRG in corretagem
                        on new { date = CC.Descricao.Pregao, valor = CC.Valor } equals new { date = CRG.Data, valor = CRG.ValorLiquidacao }
                        select ( CC.Descricao.Pregao, CC.Valor, CC, CRG );

            if (ccNotas.Count() != crgNotas.Count())
                throw new NotImplementedException();
            if (crgNotas.Where(crg => !notas.Any(n=>n.CRG == crg)).Count() > 0)
                throw new NotImplementedException();
            if (ccNotas.Where(cc => !notas.Any(n => n.CC == cc)).Count() > 0)
                throw new NotImplementedException();


            bool vendaATermo ((DateTime Pregao, decimal Valor, ContaCorrentes.Nota CC, CorretagemNotas.Nota CRG) n) 
                => n.Pregao == new DateTime(2008, 09, 22);
            //bool compraATermo ((DateTime Pregao, decimal Valor, ContaCorrentes.Nota CC, CorretagemNotas.Nota CRG) n) 
            //    => n.Pregao == new DateTime(2008, 09, 22);

            bool exceptions ((DateTime Pregao, decimal Valor, ContaCorrentes.Nota CC, CorretagemNotas.Nota CRG) n) 
                => vendaATermo(n); //&& compraATermo(n);

            (DateTime Data, string Codigo, decimal OpQuantidade, decimal OpValor, decimal NotaCorretagem, decimal NotaCorretagemIss, decimal NotaValorTaxaLiquidacao, decimal NotaEmolumentos, decimal NotaIRRFSobreVendDayTrade) NewOp(
                DateTime Data,
                decimal VlTxLiquidacao,
                decimal Emolumentos,
                decimal IRRFSobreVendDayTrade,
                decimal corretagemPorOperacao,
                decimal corretagemIssPorOperacao,
                decimal totalOp,
                decimal totalOpVendas,
                CorretagemNotas.Operacao op)
            {
                var percentualOperacao = op.Valor / totalOp;
                var opVal = op.Valor * -1;
                var operacao = (
                    Data,
                    Codigo: op.TpAcao,
                    OpQuantidade: opVal < 0 ? op.Quantidade : op.Quantidade * -1,
                    OpValor: opVal,
                    NotaCorretagem: Math.Abs(corretagemPorOperacao) * -1,
                    NotaCorretagemIss: Math.Abs(corretagemIssPorOperacao) * -1,
                    NotaValorTaxaLiquidacao: Math.Abs(VlTxLiquidacao * percentualOperacao) * -1,
                    NotaEmolumentos: Math.Abs(Emolumentos * percentualOperacao) * -1,
                    NotaIRRFSobreVendDayTrade: 0m);

                if (op.Valor < 0)
                {
                    var percentualOperacaoVenda = op.Valor / totalOpVendas;
                    operacao.NotaIRRFSobreVendDayTrade = Math.Abs(IRRFSobreVendDayTrade * percentualOperacaoVenda) * -1;
                }

                return operacao;
            }

            var operacoes = new List<(DateTime Data, string Codigo, decimal Quantidade, decimal Valor, decimal Corretagem, decimal CorretagemIss, decimal ValorTaxaLiquidacao, decimal Emolumentos, decimal IRRFSobreVendDayTrade)>();

            foreach (var n in notas.Where(exceptions))
            {
                if (vendaATermo(n))
                {
                    var corretagemTotalIss = n.CRG.CorretagemIss;
                    var corretagemTotal = n.CRG.CorretagemSemIss;
                    var operacaoList = n.CRG.ToList();
                    var totalOp = n.CRG.Sum(n2 => Math.Abs(n2.Valor));
                    var totalOpVendas = n.CRG.Sum(n2 => n2.Valor > 0 ? 0 : n2.Valor);

                    var corretagemOp1 = 15;
                    var corretagemOp2 = corretagemTotal - corretagemOp1;
                    var corretagemIssOp1 = corretagemOp1 / corretagemTotal * corretagemTotalIss;
                    var corretagemIssOp2 = corretagemOp2 / corretagemTotal * corretagemTotalIss;

                    var op1 = operacaoList[0];
                    var newOp1 = NewOp(n.CC.DataLiquidacao.AddSeconds(0),
                                       n.CRG.VlTxLiquidacao,
                                       n.CRG.Emolumentos,
                                       n.CRG.IRRFSobreVendDayTrade,
                                       corretagemOp1,
                                       corretagemIssOp1,
                                       totalOp,
                                       totalOpVendas,
                                       op1);

                    operacoes.Add(newOp1);

                    var op2 = operacaoList[1];
                    var newOp2 = NewOp(n.CC.DataLiquidacao.AddSeconds(1),
                                       n.CRG.VlTxLiquidacao,
                                       n.CRG.Emolumentos,
                                       n.CRG.IRRFSobreVendDayTrade,
                                       corretagemOp2,
                                       corretagemIssOp2,
                                       totalOp,
                                       totalOpVendas,
                                       op2);

                    operacoes.Add(newOp2);
                }
                else
                    throw new NotImplementedException();
            }

            foreach (var (Pregao, Valor, CC, CRG) in notas.Where(n=> !exceptions(n)))
            {
                var nota = CRG;
                var corretagemPorOperacao = nota.CorretagemSemIss / nota.GetNumberOfOperations();
                var corretagemIssPorOperacao = nota.CorretagemIss / nota.GetNumberOfOperations();
                var totalOp = nota.Sum(n2 => Math.Abs(n2.Valor));
                var totalOpVendas = nota.Sum(n2 => n2.Valor > 0 ? 0 : n2.Valor);

                var repeatCount = 0;
                foreach (var op in nota)
                {
                    repeatCount++;
                    var newOp = NewOp(CC.DataLiquidacao.AddSeconds(repeatCount),
                                          nota.VlTxLiquidacao,
                                          nota.Emolumentos,
                                          nota.IRRFSobreVendDayTrade,
                                          corretagemPorOperacao,
                                          corretagemIssPorOperacao,
                                          totalOp,
                                          totalOpVendas,
                                          op);
                    operacoes.Add(newOp);
                }
            }

            //using (StreamWriter sw = File.CreateText(@"c:\test.csv"))
            //{
            //    sw.WriteLine("Data;"
            //                 + "Codigo;"
            //                 + "Quantidade;"
            //                 + "Valor;"
            //                 + "SobreQuantidade;"
            //                 + "Corretagem;"
            //                 + "CorretagemIss;"
            //                 + "ValorTaxaLiquidacao;"
            //                 + "Emolumentos;"
            //                 + "IRRFSobreVendDayTrade");
            //    foreach (var n in operacoes.OrderBy(o => o.Data))
            //        sw.WriteLine($"{n.Data};"
            //                     + $"{n.Codigo};"
            //                     + $"{n.Quantidade};"
            //                     + $"{n.Valor};"
            //                     + $"{n.SobreQuantidade};"
            //                     + $"{n.Corretagem};"
            //                     + $"{n.CorretagemIss};"
            //                     + $"{n.ValorTaxaLiquidacao};"
            //                     + $"{n.Emolumentos};"
            //                     + $"{n.IRRFSobreVendDayTrade}");
            //}

            //using (StreamWriter sw = File.CreateText(@"c:\test.csv"))
            //{
            //    sw.WriteLine("DataLiquidacao;" +
            //        "DataMovimentacao;" +
            //        "Saldo;" +
            //        "Descricao;" +
            //        "NrNota;" +
            //        "ComprasAVista;" +
            //        "VendasAVista;" +
            //        "Operacao;" +
            //        "VlLiquidoOperacao;" +
            //        "IRRFSobreVendDayTrade;" +
            //        "VlTxLiquidacao;" +
            //        "Emolumentos;" +
            //        "CorretagemIss;" +
            //        "CorretagemSemIss;" +
            //        "ValorLiquidacao;" +
            //        "NumberOfOperations;" +
            //        "IsVendaOnly;" +
            //        "IsCompraOnly;" +
            //        "IsMixed");
            //    foreach (var n in asdf)
            //        sw.WriteLine($"{n.DataLiquidacao};" +
            //            $"{n.DataMovimentacao};" +
            //            $"{n.Saldo};" +
            //            $"{n.Descricao};" +
            //            $"{n.NrNota};" +
            //            $"{n.ComprasAVista};" +
            //            $"{n.VendasAVista};" +
            //            $"{n.Operacao};" +
            //            $"{n.VlLiquidoOperacao};" +
            //            $"{n.IRRFSobreVendDayTrade};" +
            //            $"{n.VlTxLiquidacao};" +
            //            $"{n.Emolumentos};" +
            //            $"{n.CorretagemIss};" +
            //            $"{n.CorretagemSemIss};" +
            //            $"{n.ValorLiquidacao};" +
            //            $"{n.NumberOfOperations};" +
            //            $"{n.IsVendaOnly};" +
            //            $"{n.IsCompraOnly};" +
            //            $"{n.IsMixed}");
            //}


            //////////////////////////////////////////////////////////////////////
            foreach (var op in operacoes)
            {
                AtivaTradeData.AddOperacao(op.Data, op.Codigo, op.Quantidade, op.Valor);
                AtivaTradeData.AddDespesa("NotaCorretagem", op.Data, op.Codigo, op.Quantidade, op.Corretagem);
                AtivaTradeData.AddDespesa("NotaCorretagemIss", op.Data, op.Codigo, op.Quantidade, op.CorretagemIss);
                AtivaTradeData.AddDespesa("NotaIRRFSobreVendDayTrade", op.Data, op.Codigo, op.Quantidade, op.IRRFSobreVendDayTrade);
                AtivaTradeData.AddDespesa("NotaEmolumentos", op.Data, op.Codigo, op.Quantidade, op.Emolumentos);
                AtivaTradeData.AddDespesa("NotaTaxaLiquidacao", op.Data, op.Codigo, op.Quantidade, op.ValorTaxaLiquidacao);
            }

            AtivaTradeData.BulkInsert();
        }
    }
}