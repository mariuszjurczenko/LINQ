using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Samochody
{
    class Program
    {
        static void Main(string[] args)
        {
            var samochody = WczytywaniePliku("paliwo.csv");

            var zapytanie = samochody
                                     .OrderByDescending(s => s.SpalanieAutostrada)
                                     .ThenBy(s => s.Producent)
                                     .Select(s => s)
                                     .FirstOrDefault(s => s.Producent == "ccc" && s.Rok == 2018);

            var zapytanie2 = samochody.Any(s => s.Producent == "BMW");

            Console.WriteLine(zapytanie2);

            //if (zapytanie != null)
            //{
            //    Console.WriteLine(zapytanie.Producent + " " + zapytanie.Model);
            //}
          

            //foreach (var samochod in zapytanie2.Take(10))
            //{
            //    Console.WriteLine(samochod.Producent + " " +samochod.Model + " : " + samochod.SpalanieAutostrada);
            //}
        }

        private static List<Samochod> WczytywaniePliku2(string sciezka) 
        {
            var zapytanie = (from linia in File.ReadAllLines(sciezka).Skip(1)
                            where linia.Length > 1
                            select Samochod.ParsujCSV(linia));

            return zapytanie.ToList();
        }

        private static List<Samochod> WczytywaniePliku(string sciezka)
        {
            return File.ReadAllLines(sciezka)
                       .Skip(1)
                       .Where(linia => linia.Length > 1)
                       .Select(Samochod.ParsujCSV).ToList();
        }
    }
}
