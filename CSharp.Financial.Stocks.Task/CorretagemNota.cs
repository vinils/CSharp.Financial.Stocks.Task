namespace CSharp.Financial.Stocks.Task
{
    using CSharp.Financial.Stocks.Task.CorretagemNotas;
    using Microsoft.Office.Interop.Excel;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class CorretagemNota : IEnumerable<Nota>
    {
        private static int? FindFirstRow(dynamic xlRange, string findStr, int startRow, int column, int maxRows)
        {
            var row = startRow;
            do
            {
                string value = xlRange.Cells[row, column].Value2;
                if (value == findStr)
                    return row;

                row++;
            } while (row < maxRows);

            return null;
        }
        //public static double? GetCompraAVista(dynamic xlRange, int maxRow)
        //    => Find<double>(xlRange, "COMPRAS À VISTA:", 4, 1, 2, maxRow);
        //public static double? GetCompraAVista(dynamic xlRange, int maxRow)
        //    => Find<double>(xlRange, "COMPRAS À VISTA:", 4, 1, 2, maxRow);
        //public static double? GetVendaAVista(dynamic xlRange, int maxRow)
        //    => Find<double>(xlRange, "VENDAS À VISTA:", 4, 3, 4, maxRow);
        public static void AddOperacoes(dynamic xlRange, int maxRows, Nota nota)
        {
            var startRow = 3;
            bool isEof(int row2) => ((string)xlRange.Cells[row2, 3].Value2) == "VENDAS À VISTA:" || row2 >= maxRows;
            var row = FindFirstRow(xlRange, "ESPECIFICAÇÃO DO TÍTULO", startRow, 4, maxRows) + 1;

            //CorretagemNotas.Operacao lastOperacoes = null;
            //string lastCodigoAcao = null;
            while (!isEof(row))
            {
                //var operacao = new Operacao();
                var column4Value = (string)xlRange.Cells[row, 4].Value2;
                if (!string.IsNullOrEmpty(column4Value))
                {
                    while (!isEof(row) && column4Value != "SUBTOTAL:")
                    {
                        if (!string.IsNullOrWhiteSpace(column4Value))
                        {
                            var isVenda = ((string)xlRange.Cells[row, 2].Value2).Trim() == "V";
                            var codigoAcao = column4Value.NextWord();
                            var quantidade = ((string)xlRange.Cells[row, 6].Value2).Replace(".", "").GetValueOrNull<int>(new CultureInfo("pt-BR"));
                            var preco = ((string)xlRange.Cells[row, 7].Value2).Replace(".", "").GetValueOrNull<decimal>(new CultureInfo("pt-BR"));

                            //if (lastOperacoes == null)
                            //{
                            //    lastOperacoes = new CorretagemNotas.Operacao(codigoAcao, operacao);
                            //}
                            //else if (lastCodigoAcao != codigoAcao)
                            //{
                            //    nota.Add(lastOperacoes);
                            //    operacao = new Operacao();
                            //    lastOperacoes = new CorretagemNotas.Operacao(codigoAcao, operacao);
                            //    lastCodigoAcao = codigoAcao;
                            //}

                            if (!quantidade.HasValue || !preco.HasValue || string.IsNullOrEmpty(codigoAcao))
                                throw new NotImplementedException();

                            var valor = quantidade.Value * preco.Value;

                            if (isVenda)
                                valor *= -1;

                            var newOp = new CorretagemNotas.Operacao(codigoAcao, new Operacao(quantidade.Value, valor));
                            nota.Add(newOp);

                            //operacao.Add(new Operacao(quantidade.Value, quantidade.Value * preco.Value));
                        }

                        row++;
                        column4Value = xlRange.Cells[row, 4].Value2;
                    }
                }

                //if(lastOperacoes != null)
                //    nota.Add(lastOperacoes);

                //lastOperacoes = null;
                row++;
            }
        }
        private static void ReadNota(dynamic xlRange,
                                     int resumoDosNegociosRow,
                                     out decimal? vendasAVista,
                                     out decimal? iRRFSobreVendDayTrade,
                                     out decimal? compraAVista,
                                     out decimal? operacao,
                                     out decimal? vlLiquidoOperacao,
                                     out decimal? taxaLiquidacao,
                                     out decimal? totalA,
                                     out decimal? emolumentos,
                                     out decimal? totalB,
                                     out decimal? corretagemIss,
                                     out decimal? corretagemMaisIss,
                                     out decimal? valorLiquidacao)
        {
            decimal? GetValueOrNull(dynamic value) => ((string)value).Replace(".", ",").Replace("D", "").Replace("C", "").GetValueOrNull<decimal>(new CultureInfo("pt-BR"));

            vendasAVista = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 1, 4].Value2);
            iRRFSobreVendDayTrade = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 6, 4].Value2);
            compraAVista = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 2, 2].Value2);
            operacao = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 5, 2].Value2);
            vlLiquidoOperacao = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 1, 7].Value2);
            taxaLiquidacao = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 1, 9].Value2);
            totalA = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 2, 9].Value2);
            emolumentos = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 4, 7].Value2);
            totalB = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 4, 9].Value2);
            corretagemIss = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 5, 9].Value2);
            corretagemMaisIss = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 5, 7].Value2);
            valorLiquidacao = GetValueOrNull(xlRange.Cells[resumoDosNegociosRow + 7, 9].Value2);
        }
        private static Nota CastToNota(Application xlApp, int maxRows, string excelFilePath)
        {
            var dataStart = excelFilePath.LastIndexOf("_NotaCor_") + "_NotaCor_".Length;
            var dataEnd = excelFilePath.LastIndexOf("_");
            var dateStr = excelFilePath.Substring(dataStart, dataEnd - dataStart);
            var date = DateTime.ParseExact(dateStr, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);

            Workbook xlWorkbook = null;
            Worksheet xlWorksheet;
            Range xlRange;

            try
            {
                xlWorkbook = xlApp.Workbooks.Open(excelFilePath);
                xlWorksheet = xlWorkbook.Sheets[1];
                xlRange = xlWorksheet.UsedRange;

                var resumoDosNegociosRow = FindFirstRow(xlRange, "RESUMO DOS NEGÓCIOS", 3, 1, maxRows);

                ReadNota(xlRange,
                         resumoDosNegociosRow.Value,
                         out decimal? vendasAVista,
                         out decimal? iRRFSobreVendDayTrade,
                         out decimal? compraAVista,
                         out decimal? operacaoValue,
                         out decimal? vlLiquidoOperacao,
                         out decimal? taxaLiquidacao,
                         out decimal? totalA,
                         out decimal? emolumentos,
                         out decimal? totalB,
                         out decimal? corretagemIss,
                         out decimal? corretagemMaisIss,
                         out decimal? valorLiquidacao);

                if (!vendasAVista.HasValue && !iRRFSobreVendDayTrade.HasValue && !compraAVista.HasValue && !operacaoValue.HasValue && !vlLiquidoOperacao.HasValue && !taxaLiquidacao.HasValue && !totalA.HasValue && !emolumentos.HasValue && !totalB.HasValue && !corretagemIss.HasValue && !corretagemMaisIss.HasValue && !valorLiquidacao.HasValue)
                    throw new NotImplementedException();
 
                if (valorLiquidacao != vlLiquidoOperacao - taxaLiquidacao - emolumentos - corretagemMaisIss + iRRFSobreVendDayTrade)
                    throw new NotImplementedException();

                var nota = new Nota(date,
                                    vendasAVista.Value,
                                    iRRFSobreVendDayTrade.Value * -1,
                                    compraAVista.Value,
                                    operacaoValue.Value,
                                    vlLiquidoOperacao.Value,
                                    taxaLiquidacao.Value,
                                    totalA.Value,
                                    emolumentos.Value,
                                    totalB.Value,
                                    corretagemIss.Value,
                                    corretagemMaisIss.Value,
                                    valorLiquidacao.Value);

                AddOperacoes(xlRange, maxRows, nota);

                return nota;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xlRange = null;
                xlWorkbook.Close(false);
                xlWorksheet = null;
                xlWorkbook = null;
                xlApp.Quit();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private static List<Nota> CastToNota(Application xlApp, IEnumerable<string> excelFilePaths, int maxRows)
        {
            var ret = new List<Nota>();

            foreach (var excelFilePath in excelFilePaths)
            {
                var nota = CastToNota(xlApp, maxRows, excelFilePath);
                ret.Add(nota);
            }

            return ret;
        }

        public static List<Nota> CastToNota(IEnumerable<string> excelFilePaths, int maxRows)
        {
            Application xlApp = null;
            try
            {
                xlApp = new Application();
                return CastToNota(xlApp, excelFilePaths, maxRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xlApp.Quit();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private readonly List<Nota> notas;

        public CorretagemNota(System.IO.DirectoryInfo excelFolderPath, int maxRows)
        {
            var files = excelFolderPath.GetFiles().Select(f=>f.FullName);
            notas = CastToNota(files, maxRows);
        }

        public IEnumerator<Nota> GetEnumerator()
            => notas.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => notas.GetEnumerator();
    }
}
