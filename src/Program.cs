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
            string path;
            if (key == ConsoleKey.D1)
            {
                var result = await ImpicService.DoWork();
                path = @"C:\temp\impic_crawler.csv";
                FileService.SaveReport(items: result, path: path);
            }
            else if (key == ConsoleKey.D2)
            {
                var result = await ApemipService.DoWork();
                path = @"C:\temp\apemip_crawler.csv";
                FileService.SaveReport(items: result, path: path);
            }
            else
            {
                throw new NotImplementedException();
            }

            Tools.ClearLines();
            Console.WriteLine($"finished work, go to: {path}");
        }
    }
}
