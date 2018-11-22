using System;
using System.Collections.Generic;

namespace _3_Zapytania
{
    public static class NaszLinq
    {
        public static IEnumerable<T> Filtr<T>(this IEnumerable<T> zrodlo, Func<T, bool> predicate)
        {
           foreach (var item in zrodlo)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}