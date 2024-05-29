using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IST_P2_EFiskalizacija.Models
{
    public class Stavka
    {
        public int id { get; set; }
        public string naziv { get; set; }
        public string jedinicaMere { get; set; }
        public double cena { get; set; }
        public int kolicina { get; set; }
    }
}
