# LinqEx - Projekt do ćwiczeń z LINQ. Generuje randomową kolekcję danych zawierających typu List<> zawierającą:

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
