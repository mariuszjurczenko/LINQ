using System.Collections.Generic;

namespace _2_Funkcje.Linq
{
    public static class NaszLinq
    {
        public static int Count<T>(this IEnumerable<T> kolekcja)
        {
            var licznik = 0;
            foreach (var item in kolekcja)
            {
                licznik++;
            }
            return licznik;
        }
    }
}
