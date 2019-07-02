//namespace CSharp.Financial.Stocks.Task.ContaCorrentes
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Text;
//    using System.Threading.Tasks;
//    using MercEntities = MercadoFinanceiro.Entities;

//    public class Pendencia : MercEntities.ContaCorrente, MercEntities.IPregao
//    {
//        public Acao Acao { get; }
//        public DateTime Pregao { get; }

//        DateTime MercEntities.IPregao.Data => Pregao;

//        public Pendencia(Acao acao, DateTime dataLiquidacao, DateTime dataMovimentacao, decimal valor, string descricao, DateTime pregao) 
//            : base(dataLiquidacao, dataMovimentacao, valor, descricao)
//        {
//            Acao = acao;
//            Pregao = pregao;
//        }
//    }
//}
