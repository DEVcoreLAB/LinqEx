using System.ComponentModel;

namespace Generator
{
    internal class Gen
    {
        static void Main(string[] args)
        {
            KreatorListy.GenerujListe();
            Console.WriteLine();
            
            //linijka do cwiczen
            List<KreatorListy.Dane> ListaLINQ = KreatorListy.GlownaLista.Where(p => p.pelnoletnosc == false).ToList();

            //podmien ListaLINQ na KreatorListy.GlownaLista aby wyswietlic pelna kolekcje
            foreach (var item in ListaLINQ)
            {
                Console.WriteLine($"{item.index} {item.imie} {item.nazwisko}");
                Console.WriteLine($"Płeć: {item.plec}");
                Console.WriteLine($"Data urodzenia: {item.dataUrodzenia}");
                Console.WriteLine($"Pełnoletność: {item.pelnoletnosc}");
                Console.WriteLine($"Miejsce zamieszkania: {item.miasto}");
                Console.WriteLine($"Zatrudnienie: {item.pracownik}");
                foreach (var items in item.listaPracownika)
                {
                    Console.WriteLine($"Zawód: {items.zawod}     Zarobki: {items.zarobki}");
                }
                Console.WriteLine($"Data dodania wpisu: {item.dataDodaniaWpisu}");
                Console.WriteLine();
            }
        }
    }
}