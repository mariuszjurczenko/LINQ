using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_Funkcje
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Pracownik> programisci = new Pracownik[]
            {
                new Pracownik { Id = 1, Imie = "Marcin", Nazwisko = "Nowak"},
                new Pracownik { Id = 2, Imie = "Tomek", Nazwisko = "Kowal"},
                new Pracownik { Id = 3, Imie = "Jacek", Nazwisko = "Sobota"},
                new Pracownik { Id = 4, Imie = "Adam", Nazwisko = "Wrona"}
            };

            IEnumerable<Pracownik> kierowcy = new List<Pracownik>()
            {
                new Pracownik { Id = 5, Imie = "Olek", Nazwisko = "Sroka"},
                new Pracownik { Id = 6, Imie = "Pawel", Nazwisko = "Wrobel"},
                new Pracownik { Id = 7, Imie = "Marek", Nazwisko = "Piatek"}
            };

            IEnumerator<Pracownik> enumerator = kierowcy.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Imie);
            }
        }
    }
}
