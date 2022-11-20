using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    internal class KreatorListy
    {
        private static List<string> ImionaMeskie = new List<string>();
        private static List<string> ImionaZenskie = new List<string>();
        private static List<string> Miasta = new List<string>();
        private static List<string> NazwiskaMeskie = new List<string>();
        private static List<string> NazwiskaZenskie = new List<string>();
        private static List<string> Zawody = new List<string>();
        public static List<Dane> GlownaLista = new List<Dane>();


        public class Dane
        {
            public int index { get; set; }
            public string imie { get; set; }
            public string nazwisko { get; set; }
            public string plec { get; set; }
            public DateTime dataUrodzenia { get; set; }
            public bool pelnoletnosc { get; set; }
            public string miasto { get; set; }
            public bool pracownik { get; set; }
            public DateTime dataDodaniaWpisu { get; set; }
            public List<DanePracownika> listaPracownika { get; set; }
        }
        public class DanePracownika
        {
            public string zawod { get; set; }
            public double zarobki { get; set; }
        }

        public static void GenerujListe()
        {
            List<Dane> NowaLista;
            int iloscRekordow;

            Console.Write("Podaj ilość rekordów: ");
            if (Int32.TryParse(Console.ReadLine(), out iloscRekordow))
            {
                
                PrzygotujDaneWejsciowe();
                for (int i = 0; i < iloscRekordow; i++)
                {
                    DoajDaneDoGlownejListy();
                }
            }
            else
            {
                Console.WriteLine("Podane dane są nieprawidłowe. \n " +
                    "Naciśnij dowolny klawisz aby kontynuaować.");
                Console.ReadKey();
                Console.Clear();
                GenerujListe();
            }
        }

        // Na bazie plikow tekstowych z "DaneWejsciowe..." tworzy listy
        //bedace potem wykorzystane w losowaniu danych glownej listy
        private static void PrzygotujDaneWejsciowe()
        {
            using (StreamReader sr = new StreamReader(@"..\..\..\DaneWejsciowe\im400.txt"))
            {
                while(!sr.EndOfStream)
                {
                    ImionaMeskie.Add(sr.ReadLine());
                }
            }
            using (StreamReader sr = new StreamReader(@"..\..\..\DaneWejsciowe\iz400.txt"))
            {
                while (!sr.EndOfStream)
                {
                    ImionaZenskie.Add(sr.ReadLine());
                }
            }
            using (StreamReader sr = new StreamReader(@"..\..\..\DaneWejsciowe\miasta.txt"))
            {
                while (!sr.EndOfStream)
                {
                    Miasta.Add(sr.ReadLine());
                }
            }
            using (StreamReader sr = new StreamReader(@"..\..\..\DaneWejsciowe\nm40.txt"))
            {
                while (!sr.EndOfStream)
                {
                    NazwiskaMeskie.Add(sr.ReadLine());
                }
            }
            using (StreamReader sr = new StreamReader(@"..\..\..\DaneWejsciowe\nz40.txt"))
            {
                while (!sr.EndOfStream)
                {
                    NazwiskaZenskie.Add(sr.ReadLine());
                }
            }
            using (StreamReader sr = new StreamReader(@"..\..\..\DaneWejsciowe\zawody.txt"))
            {
                while (!sr.EndOfStream)
                {
                    Zawody.Add(sr.ReadLine());
                }
            }
        }


        // losuje dane z przygotowanych wczesniej list i dodaje je do GlownaLista
        private static void DoajDaneDoGlownejListy()
        {
            Dane dna = new Dane();
            dna.index = GlownaLista.Count + 1;
            int x = new Random().Next(0,2);
            if (x == 0)
            {
                dna.plec = "Mężczyzna";
                dna.imie = ImionaMeskie[new Random().Next(0,ImionaMeskie.Count)];
                dna.nazwisko = NazwiskaMeskie[new Random().Next(0, NazwiskaMeskie.Count)];
            }
            else if (x == 1)
            {
                dna.plec = "Kobieta";
                dna.imie = ImionaZenskie[new Random().Next(0, ImionaZenskie.Count)];
                dna.nazwisko = NazwiskaZenskie[new Random().Next(0, NazwiskaZenskie.Count)];
            }
            dna.dataUrodzenia = LosujDateUrodzenia();
            dna.pelnoletnosc =  CzyPelnoletni(dna.dataUrodzenia);
            dna.miasto = Miasta[new Random().Next(0, Miasta.Count)];

            string zawod = Zawody[new Random().Next(0, Zawody.Count)];
            double zarobki;
            if (zawod.SequenceEqual("Bezrobotny"))
            {
                zarobki = 0;
                dna.pracownik = false;
            }
            else 
            {
                zarobki = new Random().Next(2039,8659);
                dna.pracownik = true;
            }

            dna.listaPracownika = new List<DanePracownika>()
            {
                new DanePracownika() {zarobki = zarobki, zawod = zawod}
            };

            dna.dataDodaniaWpisu = LosujDateDodania();

            GlownaLista.Add(dna);
        }

        private static DateTime LosujDateUrodzenia()
        {
            int year = new Random().Next(1970, DateTime.Now.Year - 16);
            int month = new Random().Next(1, 13);
            int day = new Random().Next(1, DateTime.DaysInMonth(year, month) + 1);
            var data = new DateTime(year, month, day);
            return data;
        }
        private static bool CzyPelnoletni(DateTime dt)
        {
            bool pelnoletnosc = false;
            DateTime dataUrodzenia = dt;
            DateTime dataPelnoletnosci;
            if (dataUrodzenia.Month == 2 && dataUrodzenia.Day == 29)
            {
                dataPelnoletnosci = new DateTime(dataUrodzenia.Year + 18, dataUrodzenia.Month, dataUrodzenia.Day - 1);
            }
            else
            {
                dataPelnoletnosci = new DateTime(dataUrodzenia.Year + 18, dataUrodzenia.Month, dataUrodzenia.Day);
            }

            if (dataPelnoletnosci > DateTime.Now)
            {
                pelnoletnosc = false;
            }
            else if (dataPelnoletnosci < DateTime.Now)
            {
                pelnoletnosc = true;
            }
            return pelnoletnosc;
        }
        private static DateTime LosujDateDodania()
        {
            int year = new Random().Next(DateTime.Now.Year - 2, DateTime.Now.Year);
            int month = new Random().Next(1, 13);
            int day = new Random().Next(1, DateTime.DaysInMonth(year, month) + 1);
            var data = new DateTime(year, month, day);
            return data;
        }


    }
}
