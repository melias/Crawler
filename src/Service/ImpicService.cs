using Crawler.CrossCutting;
using Crawler.Extensions;
using Crawler.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crawler.Service
{
    public class ImpicService
    {
        private const string _url = "http://www.impic.pt/impic/ajax/call/impic_api/consultar/ajax/43";
        public static async Task<List<Impic>> DoWork() 
        {
            var impicDataList = new List<Impic>();

            for (int pageSearch = 1; pageSearch <= 365; pageSearch++)
            {
                var parameters = GetParameters(pageSearch);
                var html = await HttpService.HttpPostWithParameters(_url, parameters);
                html = GetBodyHtml(html);
                html = html.RemoveAllTagsHtml();
                EtlImpic(html, ref impicDataList);
            }

            return impicDataList;
        }

        private static Dictionary<string, string> GetParameters(int pageSearch)
        {
            return new Dictionary<string, string> { { "id_type", "8" }, { "id_object", "25" }, { "pesquisar", "true" }, { "loadTable", "1" }, { "pageSearch", pageSearch.ToString() } };
        }

        private static string GetBodyHtml(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//tbody");
            return node.OuterHtml;
        }

        public static void EtlImpic(string html, ref List<Impic> impicDataList)
        {
            var data = html.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            for (var i = 0; i < data.Length; i += 5)
            {
                var impicData = new Impic
                {
                    Licenca = Convert.ToInt32(data[i].Trim()),
                    Npic = data[i + 1].DecodeFromUtf8(),
                    Name = data[i + 2].DecodeFromUtf8(),
                    Address = data[i + 3].DecodeFromUtf8()
                };
                impicDataList.Add(impicData);
            }
        }
    }
}
