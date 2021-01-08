using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Crawler.CrossCutting
{
    public class FileService
    {
        public static void SaveReport<T>(IEnumerable<T> items, string path)
        {
            if (!File.Exists(path))
                File.Create(path).Close();

            WriteCSV(items, path);
        }

        private static void WriteCSV<T>(IEnumerable<T> items, string path)
        {
            Type itemType = typeof(T);
            var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            using var writer = new StreamWriter(path: path);

            writer.WriteLine(string.Join("; ", props.Select(p => p.Name)));

            foreach (var item in items)
            {
                writer.WriteLine(string.Join("; ", props.Select(p => p.GetValue(item, null))));
            }
        }
    }
}
