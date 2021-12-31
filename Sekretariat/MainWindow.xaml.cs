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
using CsvHelper;
using Microsoft.Win32;

/*
    Zaprojektuj aplikację Sekretariat szkoły

    1. Przeznaczenie:
    Aplikacja ma umożliwiać wprowadzanie danych uczniów, nauczycieli i pracowników obsługi. 

    *Informacje o uczniu muszą zawierać: imię, drugie imię, nazwisko, nazwisko panieńskie, imiona rodziców, datę urodzenia, pesel, zdjęcie, płeć, 
        przynależność do klasy, przynależność do grup (np. językowych) – również międzyklasowych
    *Informacje o nauczycielu muszą zawierać: imię, drugie imię(jeśli jest), nazwisko, nazwisko panieńskie, imiona rodziców, datę urodzenia, pesel, zdjęcie, 
        płeć, wychowawstwo (jeśli jest), przedmioty nauczane, klasy w których uczy z godzinami, data zatrudnienia
    *Informacje o pracownikach obsługi muszą zawierać: imię, drugie imię, nazwisko, nazwisko panieńskie, imiona rodziców, datę urodzenia, pesel, zdjęcie, 
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

        22.12.2021
        - Pracownik + Nauczyciel DataGrid
        - Aktualizacja przyciskow Pracownik + Nauczyciel

        25.12.2021
        - Dalsze walki z ladowaniem bazy z pliku
        - Dodanie Komentarzy i uporzadkowanie kodu

        28.12.2021
        - Dalsze proby z zaladowaniem bazy
        - Sortowanie proby

        29.12.2021
        - mierne proby zapisu bazy do pliku
    
        30.12.2021
        - Filtrowanie za pomoca imienia, proby 
        - Zaladowanie bazy poprawne!!
        - Dodanie kilka opcji w menu

        Podsumowujac, staralem sie przynajmiej sprobowac kazdy podpunkt ale czesto wychodzilo to miernie. Ale mam nadzieje ze coś z tego bedzie :D
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
            Uczen KamilSabiron = new Uczen();

            KamilSabiron.uczenImie = "Kamil";
            KamilSabiron.uczenDrugieImie = "Michal";
            KamilSabiron.uczenNazwisko = "Sabiron";
            KamilSabiron.uczenNazwiskoPanienskie = "-";
            KamilSabiron.uczenImionaRodzicow = "Mariusz Czeslawa";
            KamilSabiron.uczenDataUrodzenia = "29.09.2003";
            KamilSabiron.uczenPesel = "12345678997";
            KamilSabiron.uczenPlec = "Mezczyzna";
            KamilSabiron.uczenKlasa = "3PR";
            KamilSabiron.uczenGrupa = "2";

            datagridUczen.Items.Add(KamilSabiron);
        }

        //------------------UCZEN----------------------
        //Dodawanie danych z formularza do bazy
        private void dodajDaneU(object sender, RoutedEventArgs e)
        {
            if(imieU.Text.Length <1)
                MessageBox.Show("Wprowadz dane!");
            else
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

        //Załadowanie Bazy Ucznia z pliku CSV (Excel) 
        private void zaladujBazeU(object sender, RoutedEventArgs e)
        {
            /*OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();

            Uczen StudentInfo = new Uczen();
            string[] StudentArray;

            DataTable dt = new DataTable();
            dt.Columns.Add("uczenImie", typeof(string));
            dt.Columns.Add("uczenDrugieImie", typeof(string));
            dt.Columns.Add("uczenNazwisko", typeof(string));
            dt.Columns.Add("uczenNazwiskoPanienskie", typeof(string));
            dt.Columns.Add("uczenImionaRodzicow", typeof(string));
            dt.Columns.Add("uczenDataUrodzenia", typeof(string));
            dt.Columns.Add("uczenPesel", typeof(string));
            dt.Columns.Add("uczenPlec", typeof(string));
            dt.Columns.Add("uczenKlasa", typeof(string));
            dt.Columns.Add("uczenGrupa", typeof(string));

            using (StreamReader sr = new StreamReader(ofd.FileName))
            {
                while (!sr.EndOfStream)
                {
                    StudentArray = sr.ReadLine().Split(";");

                    StudentInfo.uczenImie = StudentArray[0];
                    StudentInfo.uczenDrugieImie = StudentArray[1];
                    StudentInfo.uczenNazwisko = StudentArray[2];
                    StudentInfo.uczenNazwiskoPanienskie = StudentArray[3];
                    StudentInfo.uczenImionaRodzicow = StudentArray[4];
                    StudentInfo.uczenDataUrodzenia = StudentArray[5];
                    StudentInfo.uczenPesel = StudentArray[6];
                    StudentInfo.uczenPlec = StudentArray[7];
                    StudentInfo.uczenKlasa = StudentArray[8];
                    StudentInfo.uczenGrupa = StudentArray[9];

                    dt.Rows.Add(StudentArray);
                }
                DataView dv = new DataView(dt);
                datagridUczen.ItemsSource = dv;
            }*/

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".csv";
            ofd.Filter = "CSV|*.csv";

            if(ofd.ShowDialog()==true)
            {
                var filename = ofd.FileName;
                var lines = File.ReadAllLines(filename);
                var list = new List<Uczen>();
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i].Split(',');
                    var uczen = new Uczen()
                    {
                        uczenImie = line[0],
                        uczenDrugieImie = line[1],
                        uczenNazwisko = line[2],
                        uczenNazwiskoPanienskie = line[3],
                        uczenImionaRodzicow = line[4],
                        uczenDataUrodzenia = line[5],
                        uczenPesel = line[6],
                        uczenPlec = line[7],
                        uczenKlasa = line[8],
                        uczenGrupa = line[9]
                    };
                    //list.Add(uczen);
                    datagridUczen.Items.Add(uczen);
                }
            }
        }
        

        //Zapisanie bazy Uczen do pliku CSV (EXCEL)
        private void bazaZapiszU(object sender, RoutedEventArgs e)
        {
            //proby jakies
            /*DataTable dt = (DataTable)datagridUczen.ItemsSource;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV|*.csv";
            sfd.ShowDialog();
            sfd.ValidateNames = true;
            sfd.CheckFileExists = true;
            sfd.CheckPathExists = true;
            dt.WriteXml(sfd.FileName);*/
            
        }

        //------------------KONIEC UCZEN----------------------

        //------------------NAUCZYCIEL----------------------

        //Dodawanie danych z formularza do bazy
        private void dodajDaneN(object sender, RoutedEventArgs e)
        {
            if (imieN.Text.Length < 1)
                MessageBox.Show("Wprowadz dane!");
            else
            {
                nauczyciel tempNauczyciel = new nauczyciel();
                tempNauczyciel.nauczycielImie = imieN.Text;
                tempNauczyciel.nauczycielDrugieImie = dimieN.Text;
                tempNauczyciel.nauczycielNazwisko = nazwiskoN.Text;
                tempNauczyciel.nauczycielNazwiskoPanienskie = nazwiskoPanN.Text;
                tempNauczyciel.nauczycielImionaRodzicow = imionarodzN.Text;
                tempNauczyciel.nauczycielDataUrodzenia = dataurN.Text;
                tempNauczyciel.nauczycielPesel = peselN.Text;
                tempNauczyciel.nauczycielPlec = plecN.Text;
                tempNauczyciel.nauczycielWychowawstwo = wychowawstwoN.Text;
                tempNauczyciel.nauczycielPrzedmiotyNauczania = przedmiotynauczaniaN.Text;
                tempNauczyciel.nauczycielKlasyNauczania = klasagodzinyN.Text;
                tempNauczyciel.nauczycielDataZatr = datazatrN.Text;

                datagridNauczyciel.Items.Add(tempNauczyciel);
            }
        }

        //Wczytanie zdjecia Nauczyciel
        private void wczytajZdjecieN(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                imgDynamicN.Source = new BitmapImage(fileUri);
            }
        }

        //Załadowanie Bazy Nauczyciel z pliku CSV (Excel) 
        private void zaladujBazeN(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".csv";
            ofd.Filter = "CSV|*.csv";

            if (ofd.ShowDialog() == true)
            {
                var filename = ofd.FileName;
                var lines = File.ReadAllLines(filename);
                var list = new List<nauczyciel>();
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i].Split(',');
                    var nauczyciel = new nauczyciel()
                    {
                        nauczycielImie = line[0],
                        nauczycielDrugieImie = line[1],
                        nauczycielNazwisko = line[2],
                        nauczycielNazwiskoPanienskie = line[3],
                        nauczycielImionaRodzicow = line[4],
                        nauczycielDataUrodzenia = line[5],
                        nauczycielPesel = line[6],
                        nauczycielPlec = line[7],
                        nauczycielWychowawstwo = line[8],
                        nauczycielPrzedmiotyNauczania = line[9],
                        nauczycielKlasyNauczania = line[10],
                        nauczycielDataZatr = line[11]
                    };
                    //list.Add(nauczyciel);
                    datagridUczen.Items.Add(nauczyciel);
                }
            }
        }

        //Zapisanie bazy Nauczyciel do pliku CSV (EXCEL)
        private void bazaZapiszN(object sender, RoutedEventArgs e)
        {

        }
        //-----------------KONIEC NAUCZYCIEL----------------------

        //------------------PRACOWNIK----------------------
        //Dodawanie danych z formularza do bazy
        private void dodajDaneP(object sender, RoutedEventArgs e)
        {
            if (imieP.Text.Length < 1)
                MessageBox.Show("Wprowadz dane!");
            else
            {
                Pracownik tempPracownik = new Pracownik();
                tempPracownik.pracownikImie = imieP.Text;
                tempPracownik.pracownikDrugieImie = dimieP.Text;
                tempPracownik.pracownikNazwisko = nazwiskoP.Text;
                tempPracownik.pracownikNazwiskoPanienskie = nazwiskopaP.Text;
                tempPracownik.pracownikImionaRodzicow = imionarodzP.Text;
                tempPracownik.pracownikDataUrodzenia = dataurP.Text;
                tempPracownik.pracownikPesel = peselP.Text;
                tempPracownik.pracownikPlec = plecP.Text;
                tempPracownik.pracownikEtat = etatP.Text;
                tempPracownik.pracownikStanowisko = stanowiskoP.Text;
                tempPracownik.pracownikDataZatrudnienia = datazatP.Text;

                datagridPracownik.Items.Add(tempPracownik);
            }
        }

        //Wczytanie zdjecia Pracownika
        private void wczytajZdjecieP(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                imgDynamicP.Source = new BitmapImage(fileUri);
            }
        }

        //Załadowanie Bazy Pracownik z pliku CSV (Excel) 
        private void zaladujBazeP(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".csv";
            ofd.Filter = "CSV|*.csv";

            if (ofd.ShowDialog() == true)
            {
                var filename = ofd.FileName;
                var lines = File.ReadAllLines(filename);
                var list = new List<Pracownik>();
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i].Split(',');
                    var pracownik = new Pracownik()
                    {
                        pracownikImie = line[0],
                        pracownikDrugieImie = line[1],
                        pracownikNazwisko = line[2],
                        pracownikNazwiskoPanienskie = line[3],
                        pracownikImionaRodzicow = line[4],
                        pracownikDataUrodzenia = line[5],
                        pracownikPesel = line[6],
                        pracownikPlec = line[7],
                        pracownikEtat = line[8],
                        pracownikStanowisko = line[9],
                        pracownikDataZatrudnienia = line[10]
                    };
                    //list.Add(pracownik);
                    datagridUczen.Items.Add(pracownik);
                }
            }
        }

        //Zapisanie bazy Pracownik do pliku CSV (EXCEL)
        private void bazaZapiszP(object sender, RoutedEventArgs e)
        {

        }
        
        //-----------------KONIEC PRACOWNIK----------------------

        //------------------------MENU---------------------------
        private void menuClickU(object sender, RoutedEventArgs e)
        {
            tabUczen.IsSelected = true;
        }

        private void menuClickN(object sender, RoutedEventArgs e)
        {
            tabNauczyciel.IsSelected = true;
        }

        private void menuClickP(object sender, RoutedEventArgs e)
        {
            tabPracownik.IsSelected = true;
        }

        //----------------Searching---------------------------------
        //Wywala błąd
        //Uczen
        private void TextBox_TextChangedU(object sender, TextChangedEventArgs e)
        {
            string tempString = searchUcz.Text.ToLower().ToString();
            List<Uczen> uczniowie = new List<Uczen>();
            var zfiltrowanie = uczniowie.Where(x => x.uczenImie.ToLower().Contains(tempString));
            datagridUczen.ItemsSource = null;
            datagridUczen.ItemsSource = zfiltrowanie;
            datagridUczen.Items.Refresh();
        }

        //Nauczyciel
        private void TextBox_TextChangedN(object sender, TextChangedEventArgs e)
        {
            string tempString = searchN.Text.ToLower().ToString();
            List<nauczyciel> nauczyciele = new List<nauczyciel>();
            var zfiltrowanie = nauczyciele.Where(x => x.nauczycielImie.ToLower().Contains(tempString));
            datagridNauczyciel.ItemsSource = null;
            datagridNauczyciel.ItemsSource = zfiltrowanie;
            datagridNauczyciel.Items.Refresh();
        }

        //Pracownik
        private void TextBox_TextChangedP(object sender, TextChangedEventArgs e)
        {
            string tempString = searchP.Text.ToLower().ToString();
            List<Pracownik> pracownicy = new List<Pracownik>();
            var zfiltrowanie = pracownicy.Where(x => x.pracownikImie.ToLower().Contains(tempString));
            datagridPracownik.ItemsSource = null;
            datagridPracownik.ItemsSource = zfiltrowanie;
            datagridPracownik.Items.Refresh();
        }

        //Dodatkowe rzeczy w menu
        private void menuClickAuthor(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Author: Kamil Sabiron");
        }

        private void menuClickVersion(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Version: Alpha");
        }

        private void menuClickLanguage(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Languege: Polish");
        }

        private void menuClickHelp(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Function is in progress...");
        }
    }
}
