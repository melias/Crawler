using System;

namespace Crawler.CrossCutting
{
    public class Tools
    {
        public static string LoadingProgressBar(int currently, double onePorcent)
        {
            var itens = new string[] { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
            var currentlyPorcent = currently / onePorcent;
            var oneChar = "█";
            for (int i = 0; i < currentlyPorcent; i++)
            {
                if (i == 0)
                {
                    itens[0] = oneChar;
                }
                else
                if (i % 10 == 0)
                {
                    var index = i / 10;
                    itens[index] = oneChar;
                }
            }
            return $"{string.Join("", itens)} {currentlyPorcent:000}%";
        }
        public static string LoadingCircle(int currently)
        {
            if (currently == 0)
                return " | ";
            switch (currently)
            {
                case 1: return " / ";
                case 2: return " - ";
                case 3: return " \\ ";
                case 4: return " | ";
                case 5: return " / ";
                case 6: return " - ";
                case 7: return " \\ ";
            }
            return string.Empty;
        }

        public static void ClearLines()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }
    }
}
