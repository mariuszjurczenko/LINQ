using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _1_Wprowadzenie
{
    class Program
    {
        static void Main(string[] args)
        {
            var sciezka = @"c:\windows";
            PokazDuzePlikiBezLinq(sciezka);

            Console.WriteLine("***************");
            PokazDuzePlikiZLinq(sciezka);
        }

        private static void PokazDuzePlikiZLinq(string sciezka)
        {
            var zapytanie = from plik in new DirectoryInfo(sciezka).GetFiles()
                           orderby plik.Length descending
                           select plik;

            foreach (var plik in zapytanie.Take(5))
            {
                Console.WriteLine($"{plik.Name,-20} : {plik.Length,20:N0}");
            }
        }

        private static void PokazDuzePlikiBezLinq(string sciezka)
        {
            DirectoryInfo katalog = new DirectoryInfo(sciezka);
            FileInfo[] pliki = katalog.GetFiles();

            Array.Sort(pliki, new FileInfoComparer());

            for (var i = 0; i < 5; i++)
            {
                FileInfo plik = pliki[i];
                Console.WriteLine($"{plik.Name, -20} : {plik.Length, 20:N0}");
            }
        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
