namespace CSharp.Financial.Stocks.Task
{
    using System;
    using ContaCorrentes.Descricoes;
    using Merc = MercadoFinanceiro.Entities;

    public static class ContaCorrenteExtension
    {
        public static bool GerericTryCast<T>(Func<T> action, out T casted)
        {
            try
            {
                casted = action();
                return true;
            }
            catch (Exception)
            {
                casted = default;
                return false;
            }
        }
        public static bool IsNotaSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsNota(cc.Descricao) && cc.IsSaida;
        public static bool IsNotaEntrada(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsNota(cc.Descricao) && cc.IsEntrada;
        public static ContaCorrentes.Nota CastToNota(this Merc.ContaCorrente cc,
                                                     ContaCorrentes.Nota estorno = null)
        {
            if (estorno != null && cc.IsSaida)
                throw new ArgumentException();

            var desc = ContaCorrentes.Descricao.CastToNota(cc.Descricao);
            return new ContaCorrentes.Nota(cc.DataLiquidacao, cc.DataMovimentacao, cc.Valor, desc, cc.Saldo);
        }
        public static bool TryCastToNota(this Merc.ContaCorrente cc,
                                         out ContaCorrentes.Nota ccNota,
                                         ContaCorrentes.Nota estorno = null)
        {
            ContaCorrentes.Nota cast() => CastToNota(cc, estorno);
            return GerericTryCast(cast, out ccNota);
        }
        public static bool IsTransferenciaSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsTransferencia(cc.Descricao) && cc.IsSaida;
        public static bool IsTransferenciaEntrada(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsTransferencia(cc.Descricao) && cc.IsEntrada;
        public static ContaCorrente<Transferencia> CastToTransferencia(
            this Merc.ContaCorrente cc,
            ContaCorrente<Transferencia> estorno = null)
        {
            if (estorno != null && cc.IsSaida)
                throw new ArgumentException();

            var desc = ContaCorrentes.Descricao.CastToTransferencia(cc.Descricao);
            return new ContaCorrente<Transferencia>(cc.DataLiquidacao,
                                                    cc.DataMovimentacao,
                                                    cc.Valor,
                                                    desc,
                                                    cc.Saldo,
                                                    estorno);
        }
        public static bool TryCastToTransferencia(this Merc.ContaCorrente cc,
                                         out ContaCorrente<Transferencia> ccNota,
                                         ContaCorrente<Transferencia> estorno = null)
        {
            ContaCorrente<Transferencia> cast() => CastToTransferencia(cc, estorno);
            return GerericTryCast(cast, out ccNota);
        }
        public static bool IsTransferenciaTaxaSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsTransferenciaTaxa(cc.Descricao) && cc.IsSaida;
        public static bool IsTransferenciaTaxaEntrada(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsTransferenciaTaxa(cc.Descricao) && cc.IsEntrada;
        public static ContaCorrente<TransferenciaTaxa> CastToTransferenciaTaxa(
            this Merc.ContaCorrente cc,
            ContaCorrente<TransferenciaTaxa> estorno = null)
        {
            if (estorno != null && cc.IsSaida)
                throw new ArgumentException();

            var desc = ContaCorrentes.Descricao.CastToTransferenciaTaxa(cc.Descricao);
            return new ContaCorrente<TransferenciaTaxa>(cc.DataLiquidacao,
                                                        cc.DataMovimentacao,
                                                        cc.Valor,
                                                        desc,
                                                        cc.Saldo,
                                                        estorno);
        }
        public static bool TryCastToTransferenciaTaxa(this Merc.ContaCorrente cc,
                                         out ContaCorrente<TransferenciaTaxa> ccNota,
                                         ContaCorrente<TransferenciaTaxa> estorno = null)
        {
            ContaCorrente<TransferenciaTaxa> cast() => CastToTransferenciaTaxa(cc, estorno);
            return GerericTryCast(cast, out ccNota);
        }
        public static bool IsPendenciaSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsPendenciaEstorno(cc.Descricao) && cc.IsSaida;
        public static bool IsPendenciaEntrada(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsPendenciaNormal(cc.Descricao) && cc.IsEntrada;
        public static ContaCorrente<Pendencia> CastToPendencia(this Merc.ContaCorrente cc,
                                                               ContaCorrente<Pendencia> estorno = null)
        {
            if (estorno != null && cc.IsEntrada)
                throw new ArgumentException();

            var desc = ContaCorrentes.Descricao.CastToPendencia(cc.Descricao);
            return new ContaCorrente<Pendencia>(cc.DataLiquidacao,
                                                cc.DataMovimentacao,
                                                cc.Valor,
                                                desc,
                                                cc.Saldo,
                                                estorno);
        }
        public static bool TryCastToPendencia(this Merc.ContaCorrente cc,
                                         out ContaCorrente<Pendencia> ccNota,
                                         ContaCorrente<Pendencia> estorno = null)
        {
            ContaCorrente<Pendencia> cast() => CastToPendencia(cc, estorno);
            return GerericTryCast(cast, out ccNota);
        }
        public static bool IsMargemSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsMargemNormal(cc.Descricao) && cc.IsSaida;
        public static bool IsMargemEntrada(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsMargemEstorno(cc.Descricao) && cc.IsEntrada;
        public static ContaCorrente<Margem> CastToMargem(this Merc.ContaCorrente cc,
                                                         ContaCorrente<Margem> estorno = null)
        {
            if (estorno != null && cc.IsSaida)
                throw new ArgumentException();

            var desc = ContaCorrentes.Descricao.CastToMargem(cc.Descricao, estorno != null);
            return new ContaCorrente<Margem>(cc.DataLiquidacao,
                                             cc.DataMovimentacao,
                                             cc.Valor,
                                             desc,
                                             cc.Saldo,
                                             estorno);
        }
        public static bool TryCastToMargem(this Merc.ContaCorrente cc,
                                         out ContaCorrente<Margem> ccNota,
                                         ContaCorrente<Margem> estorno = null)
        {
            ContaCorrente<Margem> cast() => CastToMargem(cc, estorno);
            return GerericTryCast(cast, out ccNota);
        }
        public static bool IsEmolumentoSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsEmolumentoEstornoSemQuantidade(cc.Descricao) && cc.IsSaida;
        //public static bool IsEmolumentoFracaoEntrada(this Merc.ContaCorrente cc)
        //    => ContaCorrentes.Descricao.IsEmolumento(cc.Descricao) && cc.IsEntrada;
        public static ContaCorrente<Emolumento> CastToEmolumento(this Merc.ContaCorrente cc,
                                                                 ContaCorrente<Emolumento> estorno = null)
        {
            if (estorno != null && cc.IsEntrada)
                throw new ArgumentException();

            var desc = ContaCorrentes.Descricao.CastToEmolumento(cc.Descricao, estorno?.Descricao);
            return new ContaCorrente<Emolumento>(cc.DataLiquidacao,
                                                 cc.DataMovimentacao,
                                                 cc.Valor,
                                                 desc,
                                                 cc.Saldo,
                                                 estorno);
        }
        public static bool TryCastToEmolumento(this Merc.ContaCorrente cc,
                                         out ContaCorrente<Emolumento> ccNota,
                                         ContaCorrente<Emolumento> estorno = null)
        {
            ContaCorrente<Emolumento> cast() => CastToEmolumento(cc, estorno);
            return GerericTryCast(cast, out ccNota);
        }
        public static bool IsEmolumentoFracaoSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsEmolumentoFracao(cc.Descricao) && cc.IsSaida;
        public static bool IsEmolumentoFracaoEntrada(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsEmolumentoFracao(cc.Descricao) && cc.IsEntrada;
        public static ContaCorrente<EmolumentoFracao> CastToEmolumentoFracao(
            this Merc.ContaCorrente cc,
            ContaCorrente<EmolumentoFracao> estorno = null)
        {
            if (estorno != null && cc.IsEntrada)
                throw new ArgumentException();

            var desc = ContaCorrentes.Descricao.CastToEmolumentoFracao(cc.Descricao);
            return new ContaCorrente<EmolumentoFracao>(cc.DataLiquidacao,
                                                       cc.DataMovimentacao,
                                                       cc.Valor,
                                                       desc,
                                                       cc.Saldo,
                                                       estorno);
        }
        public static bool TryCastToEmolumentoFracao(this Merc.ContaCorrente cc,
                                         out ContaCorrente<EmolumentoFracao> ccEmoFra,
                                         ContaCorrente<EmolumentoFracao> estorno = null)
        {
            ContaCorrente<EmolumentoFracao> cast() => CastToEmolumentoFracao(cc, estorno);
            return GerericTryCast(cast, out ccEmoFra);
        }
        public static bool IsTaxaCustodiaSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsTaxaCustodiaNormal(cc.Descricao) && cc.IsSaida;
        public static bool IsTaxaCustodiaEntrada(this Merc.ContaCorrente cc)
            => (ContaCorrentes.Descricao.IsTaxaCustodiaEstornoSemData(cc.Descricao)
                || ContaCorrentes.Descricao.IsTaxaCustodiaEstornoComData(cc.Descricao))
               && cc.IsEntrada;
        public static ContaCorrente<TaxaCustodia> CastToTaxaCustodia(
            this Merc.ContaCorrente cc,
            ContaCorrente<TaxaCustodia> estorno = null)
        {
            if (estorno != null && cc.IsSaida)
                throw new ArgumentException();

            var desc = ContaCorrentes.Descricao.CastToTaxaCustodia(cc.Descricao, estorno?.Descricao);
            return new ContaCorrente<TaxaCustodia>(cc.DataLiquidacao,
                                                   cc.DataMovimentacao,
                                                   cc.Valor,
                                                   desc,
                                                   cc.Saldo,
                                                   estorno);
        }
        public static bool TryCastToTaxaCustodia(this Merc.ContaCorrente cc,
                                         out ContaCorrente<TaxaCustodia> ccNota,
                                         ContaCorrente<TaxaCustodia> estorno = null)
        {
            ContaCorrente<TaxaCustodia> cast() => CastToTaxaCustodia(cc, estorno);
            return GerericTryCast(cast, out ccNota);
        }
        public static bool IsOperacaoComissaoBTCSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsOperacaoComissaoBTC(cc.Descricao) && cc.IsSaida;
        public static bool IsOperacaoComissaoBTCEntrada(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsOperacaoComissaoBTC(cc.Descricao) && cc.IsEntrada;
        public static ContaCorrente<OperacaoComissaoBTC> CastToOperacaoComissaoBTC(
            this Merc.ContaCorrente cc,
            ContaCorrente<OperacaoComissaoBTC> estorno = null)
        {
            if (estorno != null && cc.IsSaida)
                throw new ArgumentException();

            var desc = ContaCorrentes.Descricao.CastToOperacaoComissaoBTC(cc.Descricao);
            return new ContaCorrente<OperacaoComissaoBTC>(cc.DataLiquidacao,
                                                          cc.DataMovimentacao,
                                                          cc.Valor,
                                                          desc,
                                                          cc.Saldo,
                                                          estorno);
        }
        public static bool TryCastToOperacaoComissaoBTC(this Merc.ContaCorrente cc,
                                         out ContaCorrente<OperacaoComissaoBTC> ccNota,
                                         ContaCorrente<OperacaoComissaoBTC> estorno = null)
        {
            ContaCorrente<OperacaoComissaoBTC> cast() => CastToOperacaoComissaoBTC(cc, estorno);
            return GerericTryCast(cast, out ccNota);
        }
        public static bool IsNotaIRRFSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsNotaIRRF(cc.Descricao) && cc.IsSaida;
        public static bool IsNotaIRRFEntrada(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsNotaIRRF(cc.Descricao) && cc.IsEntrada;
        public static ContaCorrente<NotaIRRF> CastToNotaIRRF(
            this Merc.ContaCorrente cc,
            ContaCorrente<NotaIRRF> estorno = null)
        {
            if (estorno != null && cc.IsSaida)
                throw new ArgumentException();

            var desc = ContaCorrentes.Descricao.CastToNotaIRRF(cc.Descricao);
            return new ContaCorrente<NotaIRRF>(cc.DataLiquidacao,
                                               cc.DataMovimentacao,
                                               cc.Valor,
                                               desc,
                                               cc.Saldo,
                                               estorno);
        }
        public static bool TryCastToNotaIRRF(this Merc.ContaCorrente cc,
                                         out ContaCorrente<NotaIRRF> ccNota,
                                         ContaCorrente<NotaIRRF> estorno = null)
        {
            ContaCorrente<NotaIRRF> cast() => CastToNotaIRRF(cc, estorno);
            return GerericTryCast(cast, out ccNota);
        }
        public static bool IsOperacaoTaxaEmprestimoSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsOperacaoTaxaEmprestimo(cc.Descricao) && cc.IsSaida;
        public static bool IsOperacaoTaxaEmprestimoEntrada(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsNotaIRRF(cc.Descricao) && cc.IsEntrada;
        public static ContaCorrente<OperacaoTaxaEmprestimo> CastToOperacaoTaxaEmprestimo(
            this Merc.ContaCorrente cc,
            ContaCorrente<OperacaoTaxaEmprestimo> estorno = null)
        {
            if (estorno != null && cc.IsSaida)
                throw new ArgumentException();

            var desc = ContaCorrentes.Descricao.CastToOperacaoTaxaEmprestimo(cc.Descricao);
            return new ContaCorrente<OperacaoTaxaEmprestimo>(cc.DataLiquidacao,
                                                             cc.DataMovimentacao,
                                                             cc.Valor,
                                                             desc,
                                                             cc.Saldo,
                                                             estorno);
        }
        public static bool TryCastToOperacaoTaxaEmprestimo(this Merc.ContaCorrente cc,
                                         out ContaCorrente<OperacaoTaxaEmprestimo> ccNota,
                                         ContaCorrente<OperacaoTaxaEmprestimo> estorno = null)
        {
            ContaCorrente<OperacaoTaxaEmprestimo> cast() => CastToOperacaoTaxaEmprestimo(cc, estorno);
            return GerericTryCast(cast, out ccNota);
        }
        public static bool IsAcertoContaSaida(this Merc.ContaCorrente cc)
            => ContaCorrentes.Descricao.IsAcertoConta(cc.Descricao) && cc.IsSaida;


    }
}
