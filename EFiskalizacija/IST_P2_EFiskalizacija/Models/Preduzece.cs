using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IST_P2_EFiskalizacija.Models
{
    public class Preduzece
    {
        public int id { get; set; }
        public string naziv { get; set; }
        public string odgovornoLice { get; set; }
        public string kontaktTelefon { get; set; }
        public string email { get; set; }
        public string sediste { get; set; }
        public string PIB { get; set; }
    }
}
