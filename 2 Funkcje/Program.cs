using System;
using System.Collections.Generic;
using System.Linq;

namespace _2_Funkcje
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int> potegowanie = x => x * x;

            Func<int, int, int> dodawanie = (int a, int b) =>
            {
                var temp = a + b;
                return temp;
            };

            Action<int> wypisz = x => Console.WriteLine(x);

            wypisz(potegowanie(dodawanie(2,1)));



            var programisci = new Pracownik[]
            {
                new Pracownik { Id = 1, Imie = "Marcin", Nazwisko = "Nowak"},
                new Pracownik { Id = 2, Imie = "Tomek", Nazwisko = "Kowal"},
                new Pracownik { Id = 3, Imie = "Jacek", Nazwisko = "Sobota"},
                new Pracownik { Id = 4, Imie = "Adam", Nazwisko = "Wrona"}
            };

            var kierowcy = new List<Pracownik>()
            {
                new Pracownik { Id = 5, Imie = "Olek", Nazwisko = "Sroka"},
                new Pracownik { Id = 6, Imie = "Pawel", Nazwisko = "Wrobel"},
                new Pracownik { Id = 7, Imie = "Marek", Nazwisko = "Piatek"}
            };

            var zapytanie = programisci.Where(p => p.Imie.Length == 5)
                                       .OrderByDescending(p => p.Imie)
                                       .Select(p => p);

            var zapytanie2 = from programista in programisci
                             where programista.Imie.Length == 5
                             orderby programista.Imie descending
                             select programista;

            var ilosc = zapytanie2.Count(); 

            foreach (var osoba in zapytanie2)
            {
                Console.WriteLine(osoba.Imie);
            }
        }

        private static bool RozpoczynaNaM(Pracownik pracownik)
        {
            return pracownik.Imie.StartsWith("M");
        }
    }
}