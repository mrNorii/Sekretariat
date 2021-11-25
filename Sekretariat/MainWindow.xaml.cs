using System;
using System.Collections.Generic;
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
        - Stworzenie GUI w zakladce nauczyciel
        
        25.11.2021
        -Naprawa Kodu bo przy klonowaniu repetytorium usunela GUI ucznia XD?
        -Stworzenie GUI w zakladce pracownicy
        -Rename nazw na krotsze (U-uczen, N-nauczyciel, P-pracownik)
        -Wstepne komentarze co gdzie sie znajduje
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
        }
    }
}
