using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

/*
    Zaprojektuj aplikację Sekretariat szkoły

    1. Przeznaczenie:
    Aplikacja ma umożliwiać wprowadzanie danych uczniów, nauczycieli i pracowników obsługi. 

    Informacje o uczniu muszą zawierać: imię, drugie imię, nazwisko, nazwisko panieńskie, imiona rodziców, datę urodzenia, pesel, zdjęcie, płeć, 
        przynależność do klasy, przynależność do grup (np. językowych) – również międzyklasowych
    Informacje o nauczycielu muszą zawierać: imię, drugie imię(jeśli jest), nazwisko, nazwisko panieńskie, imiona rodziców, datę urodzenia, pesel, zdjęcie, 
        płeć, wychowawstwo (jeśli jest), przedmioty nauczane, klasy w których uczy z godzinami, data zatrudnienia
    Informacje o pracownikach obsługi muszą zawierać: imię, drugie imię, nazwisko, nazwisko panieńskie, imiona rodziców, datę urodzenia, pesel, zdjęcie, 
        płeć, informacje o etacie (cały, pół etatu itp.), opis stanowiska, data zatrudnienia
    
    2. Wymagane funkcjonalności:
    Wprowadzanie i modyfikacja danych z formularza, wczytywanie danych z pliku tekstowego (z wyjątkiem zdjęć), wczytywanie zdjęć z pliku , 
    Wyszukiwanie osób według wszystkich pól (np. klasa, ilość godzin zatrudnienia większa/mniejsza niż, osoby urodzone przed/po), sortowanie wyników, 
    generowanie raportów z wyszukiwania, zapisywanie raportów do pliku tekstowego (bez zdjęć). Wymagane jest w pełni funkcjonalne menu, pasek zadań 
    i edytowalne skróty klawiaturowe.
    
    3. Etapy pracy:
    Wyszukanie istniejących rozwiązań i ich porównanie, wybór własnych rozwiązań i ich implementacja, testowanie i nanoszenie poprawek
    
    4. Ocena:
    Ocenie podlega projekt (1 ocena), GUI(menu – 2 ocena) oraz główna funkcjonalność (3 ocena), harmonogram testowania i lista poprawek (4 ocena)
________________________________________________________________________________________________________________________________________________________________________________

    Kolejnosc pracy: 
        24.11.2021
        - Stworzenie TabControl
        - Stworzenie GUI w zakladce uczen
        - Wstepne menu
        - Stworzenie GUI w zakladce nauczyciel
        
        25.11.2021
        - Naprawa Kodu bo przy klonowaniu repetytorium usunela GUI ucznia XD?
        - Stworzenie GUI w zakladce pracownicy
        - Rename nazw na krotsze (U-uczen, N-nauczyciel, P-pracownik)
        - Wstepne komentarze co gdzie sie znajduje
        - Lekkie zmiany w menu
        - Poczatek DataGrid'u
        - Stworzenie klasy uczen
        - Sprawdzenie jak dziala dodanie nowego obiektu do bazy

        02.12.2021
        - Wczytywanie zdjecia (U + N + P).
        - Przygotowanie do skrotow edytowalnych

        06.12.2021
        - Stworzenie osobnego pliku dla klasy uczen
        - Stworzenie przycisku ktory zaladuje baze danych dla uczniow 
        - Tworzenie pliku CSV w celu sprawdzenia czy wczytanie bazy danych dziala
        - Efekt - System.IndexOutOfRangeException: „Index was outside the bounds of the array.”

        
*/

namespace Sekretariat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();


            //Stworzenia Ucznia i dodanie informacji o nim
            //https://www.youtube.com/watch?v=dOZYOnFb56Q
            Uczen KamilSabiron = new Uczen();

            KamilSabiron.uczenImie = "Kamil";
            KamilSabiron.uczenDrugieImie = "Michal";
            KamilSabiron.uczenNazwisko = "Sabiron";
            KamilSabiron.uczenNazwiskoPanienskie = "Jastrzebski";
            KamilSabiron.uczenImionaRodzicow = "Mariusz Czeslawa";
            KamilSabiron.uczenDataUrodzenia = "29.09.2003";
            KamilSabiron.uczenPesel = "12345678997";
            KamilSabiron.uczenPlec = "Mezczyzna";
            KamilSabiron.uczenKlasa = "3PR";
            KamilSabiron.uczenGrupa = "2";

            datagridUczen.Items.Add(KamilSabiron);
        }

        //Dodawanie danych z formularza do bazy
        private void dodajDane(object sender, RoutedEventArgs e)
        {
            Uczen tempUczen = new Uczen();
            tempUczen.uczenImie = imieU.Text;
            tempUczen.uczenDrugieImie = dimieU.Text;
            tempUczen.uczenNazwisko = nazwiskoU.Text;
            tempUczen.uczenNazwiskoPanienskie = nazwiskoPanU.Text;
            tempUczen.uczenImionaRodzicow = imionarodzU.Text;
            tempUczen.uczenDataUrodzenia = dataurU.Text;
            tempUczen.uczenPesel = peselU.Text;
            tempUczen.uczenPlec = plecU.Text;
            tempUczen.uczenKlasa = klasaU.Text;
            tempUczen.uczenGrupa = grupaU.Text;

            datagridUczen.Items.Add(tempUczen);

        }

        //Wczytanie Zdjecia Uczen
        private void wczytajZdjecieU(object sender, RoutedEventArgs e)
        {         
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                imgDynamicU.Source = new BitmapImage(fileUri);
            }
        }

        //Wczytanie zdjecia Nauczyciel
        private void wczytajZdjecieN(object sender, RoutedEventArgs e)
        {
            tekstZdjecieN.Content = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                imgDynamicN.Source = new BitmapImage(fileUri);
            }
        }

        //Wczytanie zdjecia Pracownika
        private void wczytajZdjecieP(object sender, RoutedEventArgs e)
        {
            tekstZdjecieP.Content = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                imgDynamicP.Source = new BitmapImage(fileUri);
            }
        }

        //Załadowanie Bazy Ucznia z pliku CSV (Excel) 
        private void zaladujBazeU(object sender, RoutedEventArgs e)
        {
            //https://www.youtube.com/watch?v=aIsMwEAiOKs

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            Uczen StudentInfo = new Uczen();
            string[] StudentArray;

            DataTable dt = new DataTable();
            dt.Columns.Add("uczenImie", typeof(string));
            dt.Columns.Add("uczenDrugieImie", typeof(string));
            dt.Columns.Add("uczenNazwisko", typeof(string));

            using (StreamReader sr = new StreamReader(ofd.FileName))
            {
                while (!sr.EndOfStream)
                {
                    StudentArray = sr.ReadLine().Split(";");

                    StudentInfo.uczenImie = StudentArray[0];
                    StudentInfo.uczenDrugieImie = StudentArray[1];
                    StudentInfo.uczenNazwisko = StudentArray[2];

                    dt.Rows.Add(StudentArray);
                }
                DataView dv = new DataView(dt);
                datagridUczen.ItemsSource = dv;
            }
        }
    }
}
