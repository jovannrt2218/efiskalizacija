using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IST_P2_EFiskalizacija.Models
{
    public enum TipFakture
    {
        ULAZNA,
        IZLAZNA
    }
    public class Faktura
    {
        public int id { get; set; }
        public int IDPrimaoc { get; set; }
        public int IDIzdavaoc { get; set; }
        public DateTime datumIzdavanja { get; set; }
        public DateTime datumValute { get; set; }
        public List<Stavka> stavke { get; set; }
        public TipFakture tip { get; set; }
        public double zaPlacanje()
        {
            double suma = 0;
            if (stavke != null)
            {
                foreach (Stavka s in stavke)
                {
                    suma += (s.cena * s.kolicina);
                }
            }
            return suma;
        }
    }
}
