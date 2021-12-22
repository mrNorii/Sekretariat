using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sekretariat
{
    internal class Pracownik
    {
        public string pracownikImie { get; set; }
        public string pracownikDrugieImie { get; set; }
        public string pracownikNazwisko { get; set; }
        public string pracownikNazwiskoPanienskie { get; set; }
        public string pracownikImionaRodzicow { get; set; }
        public string pracownikDataUrodzenia { get; set; }
        public string pracownikPesel { get; set; }
        public string pracownikPlec { get; set; }
        public string pracownikEtat { get; set; }
        public string pracownikStanowisko { get; set; }
        public string pracownikDataZatrudnienia { get; set; }

        public Pracownik() { }
    }
}
