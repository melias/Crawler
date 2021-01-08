using Crawler.CrossCutting;
using Crawler.Service;
using System;
using System.Threading.Tasks;

namespace ExtractWebData
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("- Press 1 to Impic  ETL -");
            Console.WriteLine("- Press 2 to Apemip ETL -");
            Console.WriteLine("-------------------------");

            var key = Console.ReadKey().Key;

            if (key == ConsoleKey.D1)
            {
                var result = await ImpicService.DoWork();
                FileService.SaveReport(items: result, path: @"C:\temp\impic_crawler.csv");
            }
            else if (key == ConsoleKey.D2)
            {
                var result = await ApemipService.DoWork();
                FileService.SaveReport(items: result, path: @"C:\temp\apemip_crawler.csv");
            }
        }
    }
}
