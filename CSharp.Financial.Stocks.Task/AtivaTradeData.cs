namespace CSharp.Financial.Stocks.Task
{
    using Data;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public enum TipoValor
    {
        Estornos,
        Estornados,
        Quantidade,
        ValorPorAcao,
        ValorTotal
    }
    public static class GroupPath
    {
        public static string[] Transferencias(TipoValor tipoValor)
            => new string[] { "Transferências", "Default", tipoValor.ToString()};
        public static string[] DespesasGerais(string tipoDespesa, TipoValor tipoValor)
            => new string[] { "DespesasGerais", tipoDespesa, tipoValor.ToString() };
        public static string[] Margens(TipoValor tipoValor, string codigoAcao)
            => new string[] { "ChamadaMargem", "Default", tipoValor.ToString(), codigoAcao };
        public static string[] PendenciasCompras(TipoValor tipoValor, string codigoAcao)
            => new string[] { "PendênciaCompra", "Default", tipoValor.ToString(), codigoAcao };
        public static string[] Despesas(string tipoDespesa, TipoValor tipoValor, string codigoAcao) 
            => new string[] { "Despesas", tipoDespesa, tipoValor.ToString(), codigoAcao };
        public static string[] Operacoes(TipoValor tipoValor, string codigoAcao)
            => new string[] { "Operações", "Default", tipoValor.ToString(), codigoAcao };
        public static string[] Emolumentos(ContaCorrentes.Descricoes.EmolumentoTipo tipo, TipoValor tipoValor, string codigoAcao)
            => new string[] { "Emolumentos", tipo.ToString().ToUpper(), tipoValor.ToString(), codigoAcao };
    }
    public class GroupNameTree
    {
        public string Name { get; set; }
        public List<GroupNameTree> Childs = new List<GroupNameTree>();

        public GroupNameTree(string name)
        {
            this.Name = name;
        }

        public static explicit operator GroupNameTree(DictionaryTree<string, string> dictionaryTree)
        {
            var ret = new GroupNameTree(dictionaryTree.Data);

            foreach (var child in dictionaryTree)
            {
                ret.Childs.Add((GroupNameTree)child.Value);
            }

            return ret;
        }
    }
    public class AtivaTradeData
    {
        private static readonly string[] rootPath = new string[] { "AtivaTrade" };
        private static readonly Guid AtivaTradeGroupId = new Guid("63DBE169-40F3-49E8-9DA7-8A4A192BEDB3");
        private static string TreatIndex(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC)
                .ToUpper();
        }
        private static void GroupBulkInsertByName(DictionaryTree<string, string> groups)
        {
            if (!groups.Any())
                return;

            var request = new RestRequest(Method.POST)
            {
                Timeout = int.MaxValue,
                RequestFormat = DataFormat.Json
            };
            var body = new { NewGroups = (GroupNameTree)groups, RootPath = rootPath };

            request.AddJsonBody(body);

            var url = "http://localhost:58994/odata/v4/groups/BulkInsertByName";
            //var url = "http://192.168.15.250/data/odata/v4/groups/BulkInsertByName";
            var restClient = new RestClient(url);
            var response = restClient
                .Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.Content);

            //LogRequest(restClient, request, response, 0);
        }
        private static void DataBulkInsert(List<AtivaTradeData> datas, Func<string, string> treatIndex)
        {
            if (!datas.Any())
                return;

            //var dataUriStr = "http://192.168.15.250/data/odata/v4";
            var dataUriStr = "http://localhost:58994/odata/v4";
            var dataUri = new Uri(dataUriStr);
            var container = new Default.Container(dataUri)
            {
                Timeout = int.MaxValue
            };

            var groupsDbDictionary = container.Groups.ToDictionaryTree(g => treatIndex(g.Name), AtivaTradeGroupId);
            var datas2 = datas
                .GroupBy(e => string.Join("/", e.Group.Key) + "/" + e.Date.ToString())
                .Select(eg =>
                    new Data.Models.DataDecimal()
                    {
                        CollectionDate = eg.First().Date,
                        GroupId = groupsDbDictionary[eg.First().Group.Key].Data.Id,
                        DecimalValue = eg.Sum(e => e.Value)
                    })
                .ToList<Data.Models.Data>();

            var bulkInsert = Default.ExtensionMethods.BulkInsert(container.Datas, datas2);
            bulkInsert.Execute();
        }
        //private static void LogRequest(RestClient restClient, IRestRequest request, IRestResponse response, long durationMs)
        //{
        //    var requestToLog = new
        //    {
        //        resource = request.Resource,
        //        // Parameters are custom anonymous objects in order to have the parameter type as a nice string
        //        // otherwise it will just show the enum value
        //        parameters = request.Parameters.Select(parameter => new
        //        {
        //            name = parameter.Name,
        //            value = parameter.Value,
        //            type = parameter.Type.ToString()
        //        }),
        //        // ToString() here to have the method as a nice string otherwise it will just show the enum value
        //        method = request.Method.ToString(),
        //        // This will generate the actual Uri used in the request
        //        uri = restClient.BuildUri(request),
        //    };

        //    var responseToLog = new
        //    {
        //        statusCode = response.StatusCode,
        //        content = response.Content,
        //        headers = response.Headers,
        //        // The Uri that actually responded (could be different from the requestUri if a redirection occurred)
        //        responseUri = response.ResponseUri,
        //        errorMessage = response.ErrorMessage,
        //    };

        //    System.Diagnostics.Debug.WriteLine(string.Format("Request completed in {0} ms, Request: {1}, Response: {2}",
        //            durationMs,
        //            JsonConvert.SerializeObject(requestToLog),
        //            JsonConvert.SerializeObject(responseToLog)));
        //}
        public static void BulkInsert()
        {
            GroupBulkInsertByName(Groups);
            DataBulkInsert(Datas, TreatIndex);
        }

        private static DictionaryTree<string, string> Groups = new DictionaryTree<string, string>(s => TreatIndex(s), "AtivaTrade2");
        public static List<AtivaTradeData> Datas = new List<AtivaTradeData>();
        private static void Add(string[] groupPath, DateTime date, decimal value)
        {
            var group = Groups.AddIfNew(groupPath);
            var newData = new AtivaTradeData(date, group, value);
            Datas.Add(newData);
        }
        public static void AddDespesa(string groupName, DateTime date, string codigoAcao, decimal quantidade, decimal value)
        {
            void add(TipoValor tipoValor, decimal valor) 
                => Add(GroupPath.Despesas(groupName, tipoValor, codigoAcao), date, Math.Abs(valor) * -1);
            var valorPorAcao = value / quantidade;

            add(TipoValor.ValorPorAcao, valorPorAcao);
            add(TipoValor.ValorTotal, value);
        }
        public static void AddTransferencia(DateTime date, decimal value, bool? hasEstorno, bool? hasEstornado)
        {
            void add(TipoValor tipoValor, decimal valor)
                => Add(GroupPath.Transferencias(tipoValor), date, valor);

            add(TipoValor.ValorTotal, value);

            if (hasEstorno.HasValue)
                add(TipoValor.Estornos, hasEstorno == true ? 1 : 0);

            if (hasEstornado.HasValue)
                add(TipoValor.Estornados, hasEstornado == true ? 1 : 0);
        }
        public static void AddEmolumentos(DateTime date, string codigoAcao, ContaCorrentes.Descricoes.EmolumentoTipo tipoEmolumento, decimal quantidade, decimal value, bool? hasEstorno, bool? hasEstornado)
        {
            void add(TipoValor tipoValor, decimal valor) 
                => Add(GroupPath.Emolumentos(tipoEmolumento, tipoValor, codigoAcao), date, valor);
            var valorPorAcao = value / quantidade;

            add(TipoValor.ValorPorAcao, valorPorAcao);
            add(TipoValor.ValorTotal, value);

            if (hasEstorno.HasValue && hasEstorno.Value)
                add(TipoValor.Estornos, hasEstorno == true ? 1 : 0);

            if (hasEstornado.HasValue && hasEstornado.Value)
                add(TipoValor.Estornados, hasEstornado == true ? 1 : 0);
        }
        public static void AddOperacao(DateTime date, string codigoAcao, decimal quantidade, decimal value)
        {
            void add(TipoValor tipoValor, decimal valor)
                => Add(GroupPath.Operacoes(tipoValor, codigoAcao), date, valor);

            add(TipoValor.Quantidade, quantidade);
            add(TipoValor.ValorTotal, value);
        }
        public static void AddMargem(DateTime date, string codigoAcao, decimal quantidade, decimal value)
        {
            void add(TipoValor tipoValor, decimal valor)
                => Add(GroupPath.Margens(tipoValor, codigoAcao), date, valor);

            add(TipoValor.Quantidade, quantidade);
            add(TipoValor.ValorTotal, value);
        }
        public static void AddPendenciaCompra(DateTime date, string codigoAcao, decimal quantidade, decimal value)
        {
            void add(TipoValor tipoValor, decimal valor)
                => Add(GroupPath.PendenciasCompras(tipoValor, codigoAcao), date, valor);
            var qtd = Math.Abs(quantidade);

            if (value > 0)
                qtd *= -1;

            add(TipoValor.Quantidade, qtd);
            add(TipoValor.ValorTotal, value);
        }
        public static void AddDespesaGeral(string tipoDespesaGeral, DateTime date, decimal value, bool? hasEstorno, bool? hasEstornado)
        {
            void add(TipoValor tipoValor, decimal valor)
                => Add(GroupPath.DespesasGerais(tipoDespesaGeral, tipoValor), date, valor);

            add(TipoValor.ValorTotal, value);

            if (hasEstorno.HasValue && hasEstorno.Value)
                add(TipoValor.Estornos, hasEstorno == true ? 1 : 0);

            if (hasEstornado.HasValue && hasEstornado.Value)
                add(TipoValor.Estornados, hasEstornado == true ? 1 : 0);
        }

        public DateTime Date { get; set; }
        public DictionaryTree<string, string> Group { get; set; }
        public decimal Value { get; set; }

        private AtivaTradeData(DateTime date, DictionaryTree<string, string> group, decimal value)
        {
            Date = date;
            Group = group;
            Value = value;
        }
    }
}
