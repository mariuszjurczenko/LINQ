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

            var zapytanie = from samochod in samochody
                            where samochod.Producent == "Audi" && samochod.Rok == 2018
                            orderby samochod.SpalanieAutostrada descending, samochod.Producent ascending
                            select new
                            {
                                samochod.Producent,
                                samochod.Model,
                                samochod.SpalanieAutostrada
                            };

            var zapytanie2 = samochody.Where(s => s.Producent == "Audi" && s.Rok == 2018)
                             .OrderByDescending(s => s.SpalanieAutostrada)
                             .ThenBy(s => s.Producent)
                             .Select(s => new { s.Producent, s.Model, s.SpalanieAutostrada });      

            foreach (var samochod in zapytanie.Take(10))
            {
                Console.WriteLine(samochod.Producent + " " + samochod.Model + " : " + samochod.SpalanieAutostrada);
            }
        }

        private static List<Samochod> WczytywaniePliku(string sciezka) 
        {
            var zapytanie = File.ReadAllLines(sciezka)
                                .Skip(1)
                                .Where(l => l.Length > 1)
                                .WSamochod();
                
            return zapytanie.ToList();
        }
    }

    public static class SamochodRozszerzenie
    {
        public static IEnumerable<Samochod> WSamochod(this IEnumerable<string> zrodlo)
        {
            foreach (var linia in zrodlo)
            {
                var kolumny = linia.Split(','); 

                yield return new Samochod
                {
                    Rok = int.Parse(kolumny[0]),
                    Producent = kolumny[1],
                    Model = kolumny[2],
                    Pojemnosc = double.Parse(kolumny[3]),
                    IloscCylindrow = int.Parse(kolumny[4]),
                    SpalanieMiasto = int.Parse(kolumny[5]),
                    SpalanieAutostrada = int.Parse(kolumny[6]),
                    SpalanieMieszane = int.Parse(kolumny[7])
                };
            }
        }
    }
}