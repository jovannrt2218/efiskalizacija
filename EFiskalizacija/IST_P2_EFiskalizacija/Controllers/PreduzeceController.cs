using IST_P2_EFiskalizacija.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IST_P2_EFiskalizacija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreduzeceController : ControllerBase
    {
      
        static List<Preduzece> preduzeca = new List<Preduzece>()
        {
            new Preduzece{id=1, PIB="100000001", naziv="Planetasport", odgovornoLice="pera petrovic", sediste=" Timočka 14, 11000, Beograd-Vračar", email="Planetasport@gmail.rs", kontaktTelefon="011/0123245"},
            new Preduzece{id=2, PIB="100000002", naziv="Sportvision", odgovornoLice="Mika jovanivic", sediste="franjekluza4", email="Sportvision@gmail.rs", kontaktTelefon="011/987654"},
            new Preduzece{id=3, PIB="100000003", naziv="addidas", odgovornoLice="Zika sarenovic", sediste="Bul. Kralja Aleksandra bb", email="Addidas@gmail.rs", kontaktTelefon="011/342111"},
            new Preduzece{id=4, PIB="100000004", naziv="Nike", odgovornoLice="Marija bodanovic", sediste="Vojvode Stepe bb", email="Nike@gmail.rs", kontaktTelefon="012/532"},
            new Preduzece{id=5, PIB="100000005", naziv="Buzz", odgovornoLice="Jelica Jecic", sediste="", email="buzz@gmail.rs", kontaktTelefon="013/5622908"},
        };

       
        static List<Faktura> fakture = new List<Faktura>()
        {
            new Faktura{id=1, IDIzdavaoc=1, IDPrimaoc=2, datumIzdavanja=new DateTime(2022, 10, 03), datumValute=new DateTime(2022, 10, 03), tip=TipFakture.IZLAZNA,
                stavke = new List<Stavka>(){
                    new Stavka { id = 1, naziv = "patika", jedinicaMere = "komad", kolicina = 10, cena = 500000 },
                    new Stavka { id = 2, naziv = "trenerka", jedinicaMere = "komad", kolicina = 15, cena = 10 0000 },
                    new Stavka { id = 3, naziv = "ranac", jedinicaMere = "komad", kolicina = 5, cena = 90000 }
                } },
            new Faktura{id=2, IDIzdavaoc=2, IDPrimaoc=1, datumIzdavanja=new DateTime(2022, 10, 02), datumValute=new DateTime(2022, 06, 02), tip=TipFakture.ULAZNA,
                stavke = new List<Stavka>(){
                    new Stavka { id = 1, naziv = "Jankna", jedinicaMere = "komad", kolicina = 15, cena = 200000 }
                } },
            new Faktura{id=3, IDIzdavaoc=3, IDPrimaoc=4, datumIzdavanja=new DateTime(2022, 10, 13), datumValute=new DateTime(2022, 05, 13), tip=TipFakture.IZLAZNA,
                stavke = new List<Stavka>(){
                    new Stavka { id = 1, naziv = "cipele", jedinicaMere = "komad", kolicina = 10, cena = 30000 },
                    new Stavka { id = 2, naziv = "cizme", jedinicaMere = "komad", kolicina = 10, cena = 45000 }
                } },
            new Faktura{id=4, IDIzdavaoc=4, IDPrimaoc=5, datumIzdavanja=new DateTime(2022, 10, 11), datumValute=new DateTime(2022, 04, 11), tip=TipFakture.ULAZNA,
                stavke = new List<Stavka>(){
                    new Stavka { id = 1, naziv = "lopta", jedinicaMere = "komad", kolicina = 9, cena = 10000 }
                } }
        };

  
        [HttpGet]
        public IActionResult SvaPreduzeca()
        {
            if (preduzeca == null)
                return NotFound();
            return Ok(preduzeca.OrderBy(p => p.PIB).ThenBy(p => p.naziv));
        }

        [HttpPost]
        public IActionResult DodajNovoPreduzece([FromForm] string naziv, [FromForm] string odgovornoLice, [FromForm] string sediste, [FromForm] string email, [FromForm] string kontaktTelefon) {
    
            Preduzece p = new Preduzece();
            p.id = preduzeca.OrderBy(p => p.id).Last().id + 1;
            p.PIB = (long.Parse(preduzeca.OrderBy(p => p.PIB).Last().PIB) + 1).ToString();
            p.naziv = naziv;
            p.odgovornoLice = odgovornoLice;
            p.sediste = sediste;
            p.email = email;
            p.kontaktTelefon = kontaktTelefon;
            preduzeca.Add(p);
            return Created("/preduzece", p);
        }

        
        [HttpGet("{id}")]
        public IActionResult JednoPreduzece(int id)
        {
            var preduzece = preduzeca.FirstOrDefault(x => x.id == id);

            if (preduzece == null)
                return NotFound(id);

            return Ok(preduzece);
        }


        [HttpPost("{id}")]
        public IActionResult IzmeniPreduzece(int id, [FromForm] string naziv, [FromForm] string odgovornoLice, [FromForm] string sediste, [FromForm] string email, [FromForm] string kontaktTelefon)
        {
            Preduzece p = preduzeca.Find(x => x.id == id);
            if (p == null)
                return NotFound(id);

            if (naziv != null)
                p.naziv = naziv;
            if (odgovornoLice != null)
                p.odgovornoLice = odgovornoLice;
            if (sediste != null)
                p.sediste = sediste;
            if (email != null)
                p.email = email;
            if (kontaktTelefon != null)
                p.kontaktTelefon = telefon;
            return Ok(p);
        }

        [HttpGet("search")]
        public IActionResult Pretrazi(string term)
        {
            var res = preduzeca.Where(p => p.PIB.ToLower().Contains(term.ToLower()) || p.naziv.ToLower().Contains(term.ToLower()))
                               .OrderBy(p => long.Parse(p.PIB))
                               .ThenBy(p => p.naziv)
                               .Select(p => p);
        

            return Ok(res);
        }

   
        [HttpGet("{id}/fakture")]
        public IActionResult FakturePoPreduzecu(int id)
        {
            if (preduzeca.Find(x => x.id == id) == null)
                return NotFound(id);

            List<Faktura> lst = new List<Faktura>();
            foreach (Faktura f in fakture)
                if (f.IDIzdavaoc == id)
                    lst.Add(f);

            if (lst == null)
                return NoContent();

            return Ok(lst);
        }

        [HttpPost("{id}/fakture")]
        public IActionResult DodajFakturuPreduzecu(int id, [FromBody] Faktura faktura)
        {
            faktura.id = fakture.OrderBy(x => x.id).Last().id + 1;
            faktura.IDIzdavaoc = id;
            faktura.datumIzdavanja = DateTime.Now;
            fakture.Add(faktura);
            return Ok(faktura);
        }

        [HttpPost("{id}/fakture/{idf}")]
        public IActionResult IzmeniFakturu(int idf, [FromForm] int IDPrimaoc, [FromForm] DateTime datumIzdavanja, [FromForm] DateTime datumValute, [FromForm] List<Stavka> stavke, [FromForm] TipFakture tip)
        {
            Faktura f = fakture.Find(x => x.id == idf);
            if (f == null)
                return NotFound("Id fakture: " + idf + " ne postoji");

            if (IDPrimaoc != 0)
                f.IDPrimaoc = IDPrimaoc;
            f.datumIzdavanja = datumIzdavanja;
            f.datumValute = datumValute;
            if (stavke != null)
                f.stavke = stavke;
            if (tip == TipFakture.IZLAZNA || tip == TipFakture.ULAZNA)
                f.tip = tip;
            return Ok(f);
        }

        [HttpGet("{id}/bilans")]
        public IActionResult BilansPreduzecaUPeriodu(int id, DateTime from, DateTime to)
        {
          
            Preduzece p = preduzeca.FirstOrDefault(x => x.id == id);
            if (p == null)
                return NotFound(id);
            double prihod = 0;
            double rashod = 0;
            fakture.ForEach(f =>
            {
                if(f.datumIzdavanja >= from && f.datumIzdavanja <= to)
                    if (f.IDIzdavaoc == p.id)
                        prihod += f.zaUplatu();
                    else if (f.IDPrimaoc == p.id)
                        rashod += f.zaUplatu();
            });

            double bilans = prihod - rashod; 
            return Ok(bilans);
        }
    }
}
