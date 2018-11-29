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
                            where samochod.Producent == "Audi" && samochod.Rok == 2018
                            orderby samochod.SpalanieAutostrada descending, samochod.Producent ascending
                            select new
                            {
                                samochod.Producent,
                                samochod.Model,
                                samochod.SpalanieAutostrada
                            };

            foreach (var samochod in zapytanie.Take(10))
            {
                Console.WriteLine(samochod.Producent + " " + samochod.Model + " : " + samochod.SpalanieAutostrada);
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