using System;

namespace Samochody
{
    public class Samochod
    {
        public int Rok { get; set; }
        public string Producent { get; set; }
        public string Model { get; set; }
        public double Pojemnosc { get; set; }
        public int IloscCylindrow { get; set; }
        public int SpalanieMiasto { get; set; }
        public int SpalanieAutostrada { get; set; }
        public int SpalanieMieszane { get; set; }

        internal static Samochod ParsujCSV(string linia)
        {
            var kolumny = linia.Split(',');

            return new Samochod
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
