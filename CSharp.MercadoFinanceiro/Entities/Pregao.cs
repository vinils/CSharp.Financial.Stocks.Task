namespace CSharp.MercadoFinanceiro.Entities
{
    using System;

    public class Pregao : IPregao
    {
        public DateTime Data { get; }

        public Pregao(DateTime data)
        {
            Data = data;
        }

        //public override bool Equals(object obj)
        //{
        //    return obj is Pregao pregao &&
        //           Data == pregao.Data;
        //}

        //public override int GetHashCode()
        //{
        //    return -301143667 + Data.GetHashCode();
        //}
    }
}
