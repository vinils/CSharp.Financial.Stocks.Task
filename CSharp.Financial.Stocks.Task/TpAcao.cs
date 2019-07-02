namespace CSharp.Financial.Stocks.Task
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Acao2
    {
        private readonly List<TpAcao> _acoes = new List<TpAcao>();
        public TpAcao this[string acaoStr]
        {
            get => _acoes.Where(acao => acao.Equals(acaoStr)).FirstOrDefault();
        }
        public bool TryGet(string acaoStr, out TpAcao acao)
        {
            acao = this[acaoStr];
            return acao != null;
        }
        public void Add(TpAcao acao)
            => _acoes.Add(acao);
    }

    public class TpAcao
    {
        //private static string RemoveLastFCharIfExists(string str)
        //    => str.ToLower().EndsWith("f") ? str.Substring(0, str.Length - 2) : str;

        private const int _MaxLenght = 5;
        private static readonly Acao2 acoes = new Acao2();
        public static TpAcao NewOrExistent(string acaoStr)
        {
            if (acoes.TryGet(acaoStr, out TpAcao ret))
                return ret;
            else
            {
                var newAcao = new TpAcao(acaoStr);
                acoes.Add(newAcao);
                return newAcao;
            }
        }

        static TpAcao()
        {
            var itub4 = new TpAcao("ITUB4", "ITAU4");
            acoes.Add(itub4);
        }

        private readonly string _acao;
        private readonly string[] _sinonimous;
        private TpAcao(string acao, string[] sinonimous = null)
        {
            if (string.IsNullOrEmpty(acao) || acao.Length < _MaxLenght)
                throw new ArgumentNullException();

            _acao = acao.Substring(0, _MaxLenght);

            var length = sinonimous == null ? 1 : sinonimous.Length + 1;
            _sinonimous = new string[length];
            if(sinonimous != null)
                sinonimous.CopyTo(_sinonimous, 0);
            _sinonimous[length - 1] = acao;
        }

        private TpAcao(string acao, string sinonimous)
            : this(acao, new string[] { sinonimous })
        { }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            bool isEqualsString() => obj is string acao
            && acao.Length >= _MaxLenght
            && (acao.Substring(0,_MaxLenght) == _acao || _sinonimous.Contains(acao.Substring(0, _MaxLenght)));

            bool isEqualsAcao() => obj is TpAcao acao
            && (acao._acao == _acao || _sinonimous.Contains(acao._acao));

            return isEqualsString()
                || isEqualsAcao();
        }

        public override int GetHashCode()
            => _acao.GetHashCode();
        public override string ToString()
            => _acao.ToString();
        public static implicit operator string(TpAcao tpAcao)
            => tpAcao?._acao;
        public static implicit operator TpAcao(string acao)
            => NewOrExistent(acao);
        public static bool operator ==(string acaoStr, TpAcao acao)
            => (acao is null && acaoStr is null)
            || !(acao is null) && acao.Equals(acaoStr);
        public static bool operator !=(string acaoStr, TpAcao acao)
            => !(acaoStr == acao);
        public static bool operator ==(TpAcao acao, string acaoStr)
            => acaoStr == acao;
        public static bool operator !=(TpAcao acao, string acaoStr)
            => acaoStr != acao;
    }
}
