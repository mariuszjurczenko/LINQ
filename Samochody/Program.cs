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

            var zapytanie = from producent in producenci
                            join samochod in samochody
                            on producent.Nazwa equals samochod.Producent into samochodGrupa
                            orderby producent.Siedziba
                            select new
                            {
                                Producent = producent,
                                Samochody = samochodGrupa
                            } into wynik
                            group wynik by wynik.Producent.Siedziba;


            var zapytanie2 = producenci.GroupJoin(samochody, p => p.Nazwa, s => s.Producent,
                                                    (p, g) =>
                                                            new
                                                            {
                                                                Producent = p,
                                                                Samochody = g
                                                            }).OrderBy(p => p.Producent.Siedziba)
                                                            .GroupBy(s => s.Producent.Siedziba);
                            

            foreach (var grupa in zapytanie2)
            {
                Console.WriteLine($"{grupa.Key} ");

                foreach (var samochod in grupa.SelectMany(g => g.Samochody)
                                              .OrderByDescending(s => s.SpalanieAutostrada)
                                              .Take(3)) 
                {
                    Console.WriteLine($"\t {samochod.Model} : {samochod.SpalanieAutostrada}");
                }
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