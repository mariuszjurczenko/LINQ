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
            var samochody = WczytywanieSamochodu("paliwo.csv");
            var producenci = WczytywanieProducenci("producent.csv");

            var zapytanie = from samochod in samochody
                            group samochod by samochod.Producent into samochodGrupa
                            select new
                            {
                                Nazwa = samochodGrupa.Key,
                                Max = samochodGrupa.Max(s => s.SpalanieAutostrada),
                                Min = samochodGrupa.Min(s => s.SpalanieAutostrada),
                                Sre = samochodGrupa.Average(s => s.SpalanieAutostrada)
                            } into wynik
                            orderby wynik.Max descending
                            select wynik;

            var zapytanie2 = samochody.GroupBy(s => s.Producent)
                                      .Select(g =>
                                      {
                                          return new
                                          {
                                              Nazwa = g.Key,
                                              Max = g.Max(s => s.SpalanieAutostrada),
                                              Min = g.Min(s => s.SpalanieAutostrada),
                                              Sre = g.Average(s => s.SpalanieAutostrada)
                                          };
                                      })
                                      .OrderByDescending(g => g.Max);
                            
            foreach (var wynik in zapytanie)
            {
                Console.WriteLine($"{wynik.Nazwa} ");
                Console.WriteLine($"\t Max: {wynik.Max}");
                Console.WriteLine($"\t Min: {wynik.Min}");
                Console.WriteLine($"\t Sre: {wynik.Sre}");
            }
        }

        private static List<Samochod> WczytywanieSamochodu(string sciezka) 
        {
            var zapytanie = File.ReadAllLines(sciezka)
                                .Skip(1)
                                .Where(l => l.Length > 1)
                                .WSamochod();
                
            return zapytanie.ToList();
        }


        private static List<Producent> WczytywanieProducenci(string sciezka)
        {
            var zapytanie = File.ReadAllLines(sciezka)
                                .Where(l => l.Length > 1)
                                .Select(l =>
                                {
                                    var kolumny = l.Split(',');
                                    return new Producent
                                    {
                                        Nazwa = kolumny[0],
                                        Siedziba = kolumny[1],
                                        Rok = int.Parse(kolumny[2])
                                    };
                                });

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