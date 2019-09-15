namespace CSharp.Financial.Stocks.Task
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Microsoft.Office.Interop.Excel;

    public class ContaCorrenteCollection
    {
        public static List<MercadoFinanceiro.Entities.ContaCorrente> CastExcel(string excelFilePath)
        {
            Console.WriteLine("ContaCorrente Excel file path: {0}", excelFilePath);

            var ret = new List<MercadoFinanceiro.Entities.ContaCorrente>();
            var xlApp = new Application();
            var xlWorkbook = xlApp.Workbooks.Open(excelFilePath);
            var xlWorksheet = xlWorkbook.Sheets[1];
            var xlRange = xlWorksheet.UsedRange;

            var row = 3;
            var liquidacao = ((string)xlRange.Cells[row, 1].Value2).GetValueOrNull<DateTime>();

            while (liquidacao.HasValue)
            {
                var liquidacaoDate = liquidacao.Value;
                var movimentacao = Convert.ToDateTime(xlRange.Cells[row, 2].Value2);
                var descricao = ((string)xlRange.Cells[row, 3].Value2).Trim().RemoveDuplicateSpaces();
                var saldo = ((string)xlRange.Cells[row, 6].Value2).Replace(".", "").GetValueOrNull<decimal>();

                var debito = ((string)xlRange.Cells[row, 4].Value2).GetValueOrNull<decimal>();
                var credito = ((string)xlRange.Cells[row, 5].Value2).GetValueOrNull<decimal>();

                if (!debito.HasValue && !credito.HasValue || (debito.Value > 0 && credito < 0))
                {
                    throw new NotImplementedException();
                }

                var valor = credito.HasValue && credito > 0 ? credito.Value : debito.Value;

                var newCC = new MercadoFinanceiro.Entities.ContaCorrente(liquidacaoDate,
                                                                         movimentacao,
                                                                         valor,
                                                                         saldo.Value,
                                                                         descricao);
                ret.Add(newCC);

                row++;
                liquidacao = ((string)xlRange.Cells[row, 1].Value2).GetValueOrNull<DateTime>();
            }

            return ret;
        }

        public List<ContaCorrente<ContaCorrentes.Descricoes.Emolumento>> Emolumentos = new List<ContaCorrente<ContaCorrentes.Descricoes.Emolumento>>();
        public List<ContaCorrentes.Nota> Notas = new List<ContaCorrentes.Nota>();
        public List<ContaCorrente<ContaCorrentes.Descricoes.Transferencia>> Transferencias = new List<ContaCorrente<ContaCorrentes.Descricoes.Transferencia>>();
        public List<ContaCorrente<ContaCorrentes.Descricoes.Pendencia>> Pendencias = new List<ContaCorrente<ContaCorrentes.Descricoes.Pendencia>>();
        public List<ContaCorrente<ContaCorrentes.Descricoes.Margem>> Margens = new List<ContaCorrente<ContaCorrentes.Descricoes.Margem>>();
        public List<ContaCorrente<ContaCorrentes.Descricoes.TaxaCustodia>> CustodiaTaxas = new List<ContaCorrente<ContaCorrentes.Descricoes.TaxaCustodia>>();
        public List<ContaCorrente<ContaCorrentes.Descricoes.TransferenciaTaxa>> TransferenciaTaxas = new List<ContaCorrente<ContaCorrentes.Descricoes.TransferenciaTaxa>>();
        public List<ContaCorrente<ContaCorrentes.Descricoes.OperacaoComissaoBTC>> OperacaoBTCs = new List<ContaCorrente<ContaCorrentes.Descricoes.OperacaoComissaoBTC>>();
        public List<ContaCorrente<ContaCorrentes.Descricoes.NotaIRRF>> NotaIRRFs = new List<ContaCorrente<ContaCorrentes.Descricoes.NotaIRRF>>();
        public List<ContaCorrente<ContaCorrentes.Descricoes.OperacaoTaxaEmprestimo>> OperacaoEmprestimoTaxas = new List<ContaCorrente<ContaCorrentes.Descricoes.OperacaoTaxaEmprestimo>>();
        public List<ContaCorrente<ContaCorrentes.Descricoes.EmolumentoFracao>> EmolumentoFracoes = new List<ContaCorrente<ContaCorrentes.Descricoes.EmolumentoFracao>>();

        public ContaCorrenteCollection(IEnumerable<MercadoFinanceiro.Entities.ContaCorrente> ccs)
        {
            //var emolumentos = new List<ContaCorrentes.ContaCorrente<ContaCorrentes.Descricoes.Emolumento>>();
            //var notas = new List<ContaCorrentes.ContaCorrente<ContaCorrentes.Descricoes.Nota>>();
            //var transfs = new List<ContaCorrentes.ContaCorrente<ContaCorrentes.Descricoes.Transferencia>>();
            //var pendencias = new List<ContaCorrentes.ContaCorrente<ContaCorrentes.Descricoes.Pendencia>>();
            //var margens = new List<ContaCorrentes.ContaCorrente<ContaCorrentes.Descricoes.Margem>>();
            //var txCustodias = new List<ContaCorrentes.ContaCorrente<ContaCorrentes.Descricoes.TaxaCustodia>>();
            //var txTransfs = new List<ContaCorrentes.ContaCorrente<ContaCorrentes.Descricoes.TransferenciaTaxa>>();
            //var opBtcs = new List<ContaCorrentes.ContaCorrente<ContaCorrentes.Descricoes.OperacaoComissaoBTC>>();
            //var notaIrrfs = new List<ContaCorrentes.ContaCorrente<ContaCorrentes.Descricoes.NotaIRRF>>();
            //var opTxEmps = new List<ContaCorrentes.ContaCorrente<ContaCorrentes.Descricoes.OperacaoTaxaEmprestimo>>();
            //var emoFras = new List<ContaCorrentes.ContaCorrente<ContaCorrentes.Descricoes.EmolumentoFracoes>>();

            var possivelEstornos = new List<MercadoFinanceiro.Entities.ContaCorrente>();
            foreach(var cc in ccs)
            {
                if (cc.TryCastToNota(out ContaCorrentes.Nota nota))
                    Notas.Add(nota);
                else if (cc.TryCastToTransferencia(out ContaCorrente<ContaCorrentes.Descricoes.Transferencia> transf) && cc.IsSaida)
                    Transferencias.Add(transf);
                else if (cc.TryCastToTransferenciaTaxa(out ContaCorrente<ContaCorrentes.Descricoes.TransferenciaTaxa> txTransf) && cc.IsSaida)
                    TransferenciaTaxas.Add(txTransf);
                else if (cc.TryCastToPendencia(out ContaCorrente<ContaCorrentes.Descricoes.Pendencia> pend))
                    Pendencias.Add(pend);
                else if (cc.TryCastToMargem(out ContaCorrente<ContaCorrentes.Descricoes.Margem> margem))
                    Margens.Add(margem);
                else if (cc.TryCastToEmolumento(out ContaCorrente<ContaCorrentes.Descricoes.Emolumento> ccEmo))
                    Emolumentos.Add(ccEmo);
                else if (cc.TryCastToEmolumentoFracao(out ContaCorrente<ContaCorrentes.Descricoes.EmolumentoFracao> emoFra))
                    EmolumentoFracoes.Add(emoFra);
                else if (cc.TryCastToTaxaCustodia(out ContaCorrente<ContaCorrentes.Descricoes.TaxaCustodia> txCust))
                    CustodiaTaxas.Add(txCust);
                else if (cc.TryCastToOperacaoComissaoBTC(out ContaCorrente<ContaCorrentes.Descricoes.OperacaoComissaoBTC> opBtc))
                    OperacaoBTCs.Add(opBtc);
                else if (cc.TryCastToNotaIRRF(out ContaCorrente<ContaCorrentes.Descricoes.NotaIRRF> notaIrrf))
                    NotaIRRFs.Add(notaIrrf);
                else if (cc.TryCastToOperacaoTaxaEmprestimo(out ContaCorrente<ContaCorrentes.Descricoes.OperacaoTaxaEmprestimo> opTxEmp))
                    OperacaoEmprestimoTaxas.Add(opTxEmp);
                else
                    possivelEstornos.Add(cc);
            }

            foreach(var cc in possivelEstornos)
            {
                if (cc.IsNotaEntrada())
                {
                    var estorno = Notas
                        .PossibleReverse<ContaCorrentes.Nota, ContaCorrentes.Descricoes.Nota>(cc)
                        .LastOrDefault();

                    var nota = cc.CastToNota(estorno);
                    Notas.Add(nota);
                }
                else if (cc.IsTransferenciaEntrada())
                {
                    var estorno = Transferencias
                        .PossibleReverse(cc)
                        .LastOrDefault();

                    var transf = cc.CastToTransferencia(estorno);
                    Transferencias.Add(transf);
                }
                else if (cc.IsTransferenciaTaxaEntrada())
                {
                    var estorno = TransferenciaTaxas
                        .PossibleReverse(cc)
                        .Last();

                    var txTransf = cc.CastToTransferenciaTaxa(estorno);
                    TransferenciaTaxas.Add(txTransf);
                }
                else if (cc.IsPendenciaSaida())
                {
                    var estorno = Pendencias
                        .PossibleReverse(cc)
                        .Last();

                    var pend = cc.CastToPendencia(estorno);
                    Pendencias.Add(pend);
                }
                else if (cc.IsMargemEntrada())
                {
                    var estorno = Margens
                        .PossibleReverse(cc)
                        .Last();

                    var margem = cc.CastToMargem(estorno);
                    Margens.Add(margem);
                }
                else if (cc.IsEmolumentoSaida())
                {
                    var estorno = Emolumentos
                        .PossibleReverse(cc)
                        .Last();

                    var emo = cc.CastToEmolumento(estorno);

                    Emolumentos.Add(emo);
                }
                else if (cc.IsTaxaCustodiaEntrada())
                {
                    var estorno = CustodiaTaxas
                        .PossibleReverse(cc)
                        .LastOrDefault();
                    var txCust = cc.CastToTaxaCustodia(estorno);

                    CustodiaTaxas.Add(txCust);
                }
                else if (cc.IsAcertoContaSaida())
                {
                    var emoEstorno = Emolumentos
                        .PossibleReverse(cc)
                        .LastOrDefault();

                    if (emoEstorno != null)
                    {

                        var emoDesc = new ContaCorrentes.Descricoes.Emolumento(cc.Descricao, emoEstorno.Descricao.Acao, emoEstorno.Descricao.Tipo);
                        var emo = new ContaCorrente<ContaCorrentes.Descricoes.Emolumento>(cc.DataLiquidacao, cc.DataMovimentacao, cc.Valor, emoDesc, cc.Saldo, emoEstorno);

                        Emolumentos.Add(emo);
                    }
                    else
                        throw new NotImplementedException();
                }
                else
                    throw new NotImplementedException();
            }
        }

        public ContaCorrenteCollection(string excelFilePath)
            : this(CastExcel(excelFilePath))
        { }
    }
}