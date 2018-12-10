using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Samochody
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SamochodDB>());
            WstawDane();
            ZapytanieDane();
        }

        private static void WstawDane()
        {
            var samochody = WczytywanieSamochodu("paliwo.csv");
            var db = new SamochodDB();
            db.Database.Log = Console.WriteLine;

            if (!db.Samochody.Any())
            {
                foreach (var samochod in samochody)
                {
                    db.Samochody.Add(samochod);
                }
                db.SaveChanges();
            }
        }

        private static void ZapytanieDane()
        {
            var db = new SamochodDB();
            db.Database.Log = Console.WriteLine;

            var zapytanie = from samochod in db.Samochody
                            group samochod by samochod.Producent into producent
                            select new
                            {
                                Model = producent.Key,
                                Samochody = (from samochod in producent
                                            orderby samochod.SpalanieAutostrada descending
                                            select samochod).Take(2)
                            };

            var zapytanie2 = db.Samochody.GroupBy(s => s.Producent)
                                         .Select(g => new
                                         {
                                             Model = g.Key,
                                             Samochody = g.OrderByDescending(s => s.SpalanieAutostrada).Take(2)
                                         });

            foreach (var grupa in zapytanie)
            {
                Console.WriteLine(grupa.Model);

                foreach (var samochod in grupa.Samochody)
                {
                    Console.WriteLine($"\t{samochod.Model} : {samochod.SpalanieAutostrada}");
                }
            }          
        }

        private static void ZapytanieXML()
        {
            XNamespace ns = "http://dev-hobby.pl/Samochody/2018";
            XNamespace ex = "http://dev-hobby.pl/Samochody/2018/ex";

            var dokument = XDocument.Load("paliwo.xml");
            var zapytanie = from element in dokument.Element(ns + "Samochody")?.Elements(ex + "Samochod") ?? Enumerable.Empty<XElement>()
                            where element.Attribute("Producent")?.Value == "Ferrari"
                            select new
                            {
                                model = element.Attribute("Model").Value,
                                producent = element.Attribute("Producent").Value
                            }; 

            foreach (var samochod in zapytanie)
            {
                Console.WriteLine(samochod.producent + " " + samochod.model);
            }

        }

        private static void TworzenieXML()
        {
            XNamespace ns = "http://dev-hobby.pl/Samochody/2018";
            XNamespace ex = "http://dev-hobby.pl/Samochody/2018/ex";

            var rekordy = WczytywanieSamochodu("paliwo.csv");

            var dokument = new XDocument();
            var samochody = new XElement(ns + "Samochody", from rekord in rekordy
                                                      select new XElement(ex + "Samochod",
                                                                           new XAttribute("Rok", rekord.Rok),
                                                                           new XAttribute("Producent", rekord.Producent),
                                                                           new XAttribute("Model", rekord.Model),
                                                                           new XAttribute("SpalanieAutostrada", rekord.SpalanieAutostrada),
                                                                           new XAttribute("SpalanieMiasto", rekord.SpalanieMiasto),
                                                                           new XAttribute("SpalanieMieszane", rekord.SpalanieMieszane)));

            samochody.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));

            dokument.Add(samochody);
            dokument.Save("paliwo.xml");
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