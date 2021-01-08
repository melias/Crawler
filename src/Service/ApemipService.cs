using Crawler.CrossCutting;
using Crawler.Model;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crawler.Service
{
    public class ApemipService
    {
        private const string _url = "https://associados.apemip.pt/api/customers";
        public static async Task<List<Apemip>> DoWork() 
        {
            var apemipDataList = new List<Apemip>();
            var start = 0;

            for (int pageSearch = 1; pageSearch <= 219; pageSearch++)
            {
                var parameters = GetParameters(page: pageSearch, start: start += 10);
                var jsonString = await HttpService.HttpGet(_url + parameters);
                var json = JObject.Parse(jsonString);

                ParseJsonToApemip(json, ref apemipDataList);
            }

            return apemipDataList;
        }

        private static void ParseJsonToApemip(JObject json, ref List<Apemip> apemipDataList) 
        {
            foreach (var item in json["data"])
            {
                var apemip = new Apemip
                {
                    Empresa = item["empresa"]?.ToString().Replace(";", "-"),
                    Email = item["email_publico"]?.ToString().Replace(";", "-"),
                    Telefone = item["telefone"]?.ToString().Replace(";", "-"),
                    Site = item["website_associado"]?.ToString().Replace(";", "-"),
                    Licenca = item["lic_ami"]?.ToString().Replace(";", "-"),
                    Morada = item["morada"]?.ToString().Replace(";", "-"),
                    CodigoPostal = item["cp"]?.ToString().Split(' ')[0].Replace(";", "-"),
                    Distrito = item["distrito"]?.ToString().Replace(";", "-"),
                    Concelho = item["concelho"]?.ToString().Replace(";", "-"),
                };
                apemipDataList.Add(apemip);
            }
        }

        private static string GetParameters(int page, int start)
        {
            return "?draw=" + page.ToString() + "&columns%5B0%5D%5Bdata%5D=general_info&columns%5B0%5D%5Bname%5D=general_info&columns%5B0%5D%5Bsearchable%5D=false&columns%5B0%5D%5Borderable%5D=true" +
                "&columns%5B0%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B0%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B1%5D%5Bdata%5D=empresa&columns%5B1%5D%5Bname%5D=name&columns%5B1%5D%5Bsearchable%5D=true" +
                "&columns%5B1%5D%5Borderable%5D=true&columns%5B1%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B1%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B2%5D%5Bdata%5D=concelho&columns%5B2%5D%5Bname%5D=county" +
                "&columns%5B2%5D%5Bsearchable%5D=true&columns%5B2%5D%5Borderable%5D=true&columns%5B2%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B2%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B3%5D%5Bdata%5D=email_publico" +
                "&columns%5B3%5D%5Bname%5D=pub_email&columns%5B3%5D%5Bsearchable%5D=false&columns%5B3%5D%5Borderable%5D=true&columns%5B3%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B3%5D%5Bsearch%5D%5Bregex%5D=false" +
                "&columns%5B4%5D%5Bdata%5D=website_associado&columns%5B4%5D%5Bname%5D=pub_website&columns%5B4%5D%5Bsearchable%5D=true&columns%5B4%5D%5Borderable%5D=true&columns%5B4%5D%5Bsearch%5D%5Bvalue%5D=" +
                "&columns%5B4%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B5%5D%5Bdata%5D=apemip&columns%5B5%5D%5Bname%5D=apemip_number&columns%5B5%5D%5Bsearchable%5D=true&columns%5B5%5D%5Borderable%5D=true" +
                "&columns%5B5%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B5%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B6%5D%5Bdata%5D=lic_ami&columns%5B6%5D%5Bname%5D=ami&columns%5B6%5D%5Bsearchable%5D=true" +
                "&columns%5B6%5D%5Borderable%5D=true&columns%5B6%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B6%5D%5Bsearch%5D%5Bregex%5D=false&columns%5B7%5D%5Bdata%5D=cp&columns%5B7%5D%5Bname%5D=postal_code" +
                "&columns%5B7%5D%5Bsearchable%5D=true&columns%5B7%5D%5Borderable%5D=true&columns%5B7%5D%5Bsearch%5D%5Bvalue%5D=&columns%5B7%5D%5Bsearch%5D%5Bregex%5D=false&order%5B0%5D%5Bcolumn%5D=0" +
                "&order%5B0%5D%5Bdir%5D=asc&start=" + start.ToString() + "&length=10&search%5Bvalue%5D=&search%5Bregex%5D=false&_=1610115174757";
        }
    }
}
