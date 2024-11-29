// See https://aka.ms/new-console-template for more information
using RIS_vaje2;
using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;




List<Artikel> artikli = new List<Artikel>();
List<Dobavitelj> dobavitelji = new List<Dobavitelj>();

string filePathXML = "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\Vaje5\\artikli.xml";
string filePathXML_Dobavitelji = "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\Vaje5\\DobaviteljiPet.xml";
string filePathXML_specificniArtikli = "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\vaje2\\vaje2_SpecArt.xml";
string xsltPath = "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\vajeSedem.xslt";
string xsltPathDva = "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\vajeSedemDva.xslt";
string outputHtmlPath = "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\output.html";
string outputHtmlPathDva = "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\outputDva.html";



Artikel.TransHTML(filePathXML, xsltPath, outputHtmlPath);
Dobavitelj.TransHTML(filePathXML_Dobavitelji, xsltPathDva, outputHtmlPathDva);


Artikel artikel = new Artikel("kemik", 9.9, 10, 0, new DateTime(2024, 1, 15),true,"hisni");
Artikel artikel1 = new Artikel("skodelica", 8.8, 3, 1, new DateTime(2023, 7, 4), true, "tehnicni");
Artikel artikel2 = new Artikel("racunalnik", 100, 4, 2, new DateTime(2022, 12, 31),false,"hisni");
Artikel artikel3 = new Artikel("ponva", 15, 7, 3, new DateTime(2022, 1, 23),true,"ekoloski");
Artikel artikel4 = new Artikel("okvir", 3.5, 2, 4, new DateTime(2022, 2, 11), true, "hisni");
artikli.Add(artikel);
artikli.Add(artikel1);
artikli.Add(artikel2);
artikli.Add(artikel3);
artikli.Add(artikel4);


Dobavitelj dobavitelj = new Dobavitelj("feri", "koroska ulica 93", 111111, "040231199", "Dobavitelj raznih izdelkov", 1,"srednje", "luna.bennett92@example.com");
Dobavitelj dobavitelj1 = new Dobavitelj("tus", "vojkova ulica 88", 111111, "040567892", "Dobavitelj specializiran za prehrano", 1,"kratko", "ryan.jones89@testmail.com");
Dobavitelj dobavitelj2 = new Dobavitelj("bigbang", "dominkuseva ulica 8", 111111, "040543178", "Dobavitelj specializiran za tehnologijo", 1,"kratko", "mason.lee203@demoemail.org");
Dobavitelj dobavitelj3 = new Dobavitelj("action", "rozmanova ulica 32", 111111, "040888999", "Dobavitelj specializiran poceni nakupe", 1,"dolgo", "sophia.martin07@placeholder.net");
Dobavitelj dobavitelj4 = new Dobavitelj("spar", "ulica roberta hvalca 79", 111111, "040575998", "Dobavitelj specializiran za mesne izdelke", 1,"srednje", "alex.foster44@samplemail.com");
Dobavitelj dobavitelj5 = new Dobavitelj("novi", "test", 111111, "040575998", "Dobavitelj specializiran za mesne izdelke", 1, "srednje", "alex.foster44@samplemail.com");
Dobavitelj dobavitelj6 = new Dobavitelj("zagovor", "test", 111111, "040575998", "Dobavitelj specializiran za mesne izdelke", 1, "srednje", "alex.foster44@samplemail.com");
Dobavitelj dobavitelj7 = new Dobavitelj("zagovorDva", "test", 111111, "040575998", "Dobavitelj specializiran za mesne izdelke", 1, "srednje", "alex.foster44@samplemail.com");

dobavitelji.Add(dobavitelj);
dobavitelji.Add(dobavitelj1);
dobavitelji.Add(dobavitelj2);
dobavitelji.Add(dobavitelj3);
dobavitelji.Add(dobavitelj4);




List<Artikel> artikliSeznam = Artikel.beriXML_Artikel(filePathXML,"");
foreach (var art in artikli)
{

    bool artikelExists = artikliSeznam.Any(artikeltest => art.ime == artikeltest.ime);

    if (!artikelExists)
    {
        art.pisiXML_Artikel(filePathXML, art);

    }
}

List<Dobavitelj> dobaviteljSeznam = Dobavitelj.beriXML_Dob(filePathXML_Dobavitelji);
foreach (var dob in dobavitelji)
{
    bool doblExists = dobaviteljSeznam.Any(dobtest => dob.naziv == dobtest.naziv);

    if (!doblExists)
    {
        Dobavitelj.pisiXML_Dobavitelj(filePathXML_Dobavitelji, dob);

    }


}

if (true)
{

    while (true)
    {
        Console.WriteLine("Izberite željeno operacijo :");
        Console.WriteLine("1 - Beri vse artikle , 2 - Artikli drazji od specificne cene , 3 - Vsota cen vseh artikli , 4 - Povprecje cen vseh artiklov, 5 - Prikazi najdrazji artikel, 6 - Prikazi dobavljive artikle, 7 - Isci po specificnem tipu, 8 - Prikazi maksimum stevilo artiklov na zalogi,  9 - Stevilo artiklov po tipu, 10 - Tip artikla, ki se najveckrat pojavi");


        string porabniskiVnos = Console.ReadLine();

        int izbranaOperacija = 0;

        if (!int.TryParse(porabniskiVnos, out izbranaOperacija) || izbranaOperacija < 1 || izbranaOperacija > 101)
        {
            Console.WriteLine("Napačen vnos! Prosimo, izberite    število med 1 in 8.");
            continue;
        }

        switch (izbranaOperacija)
        {


            case 1:
                beriVse(filePathXML);
                break;
            case 2:
                PrikaziMansjeOd(filePathXML);
                break;
            case 3:
                PrikaziVsotoVsehCen(filePathXML);
                break;
            case 4:
                PrikaziPovprecjeCen(filePathXML); 
                break;
            case 5:
                PrikaziNajdrazjiArtikel(filePathXML);
                break;
            case 6:
                prikaziDobavljive(filePathXML);
                break;
            case 7:
                prikaziPoTipu(filePathXML);
                break;
            case 8:
                prikaziMaksZaloga(filePathXML);
                break;
            case 9:
                stArtiklovPoTipu(filePathXML);
                break;
            case 10:
                prikaziArtiklePoDatumu(filePathXML);
                break;
            case 100:
                Console.WriteLine("Program se končuje.");
                Environment.Exit(0);
                break;

        }


    }
}


    static void PrikaziVsotoVsehCen(string path) 
    {
        Artikel.beriPovprecjeVsotaMaxCen(path, "vsota","");
    }

    static void prikaziArtiklePoDatumu(string path)
    {
   
    Artikel.beriPovprecjeVsotaMaxCen(path, "tipNajvecPojav", "");
    }

    static void stArtiklovPoTipu(string path)
    {
    Console.WriteLine("Vnesite tip artikla za katerega zelite vedeti stevilo artikov: hisni - tehnicni - ekoloski");
    string tip = Console.ReadLine();
    Artikel.beriPovprecjeVsotaMaxCen(path, "stArtPoTipu", tip);
    }

    static void prikaziMaksZaloga(string path)
    {
        Console.WriteLine("Vnesite maksimalno stevilo izdelkov na zalogi:");
        string userInput = Console.ReadLine();   
        int maksStevilo;

   
        if (Int32.TryParse(userInput, out maksStevilo))
        {  
            Artikel.beriPovprecjeVsotaMaxCen(path, "maksZaloga", maksStevilo.ToString());
        }
        else
        {
            Console.WriteLine("Vnesena vrednost ni veljavna. Prosimo vnesite številko.");
        }
    }

    static void PrikaziPovprecjeCen(string path)
    {
       Artikel.beriPovprecjeVsotaMaxCen(path,"povprecje","");
        
    }
    static void PrikaziNajdrazjiArtikel(string path)
    {
        Artikel.beriPovprecjeVsotaMaxCen(path, "najdrazji","");

    }
    static void prikaziDobavljive(string path)
    {
        Artikel.beriPovprecjeVsotaMaxCen(path, "true","");
    }
    static void prikaziPoTipu(string path)
    {
        Console.WriteLine("Vnesite tip artikla: hisni - tehnicni - ekoloski");
        string tip = Console.ReadLine();
        Artikel.beriPovprecjeVsotaMaxCen(path, "tip", tip);
    }

    static void PrikaziMansjeOd(string path)
        {
            Console.WriteLine("Vnesite minimalno ceno zeljenih artiklov");
            int minCena = Int32.Parse(Console.ReadLine());
            string minCenaString =  minCena.ToString();
            List<Artikel> artikliSeznam= Artikel.beriXML_Artikel(path, minCenaString);
            foreach (var artikelTrenutni in artikliSeznam)
            {
                Console.WriteLine(artikelTrenutni.ToString());
            }
    

        }


    static void DodajDobavitelja(string path)
    {
        string tipSodelovanja = "";
        int dolgoRocnoSodelovanje = 0;
        Console.WriteLine("Vnesite sledeče podatke");

        Console.WriteLine("Vnesite naziv dobavitelja:");
        string nazivDobavitelja = Console.ReadLine();
    


        Console.WriteLine("Vnesite naslov dobavitelja:");
        string naslovDobavitelja = Console.ReadLine();
   


        Console.WriteLine("Vnesite davcno dobavitelja");
        int dacvna=Int32.Parse(Console.ReadLine());
  


        Console.WriteLine("Vnesite kontaktne telefonsko dobavitelja:");
        string kontaktTel = Console.ReadLine();
   

        Console.WriteLine("Vnesite elektronski naslov dobavitelja:");
        string gmail = Console.ReadLine();
   

        Console.WriteLine("Vnesite opis dobavitelja:");
        string opisDobavitelja = Console.ReadLine();

        Console.WriteLine("Ali imate namen dolgorocno sodelovanje s tem dobaviteljom: 0 - NE, 1 - DA ");
        dolgoRocnoSodelovanje = Int32.Parse(Console.ReadLine());
        


  

        if ( dolgoRocnoSodelovanje == 1)
        {

            Console.WriteLine("Izberite tip sodelovanja: kratko, srednje, dolgo.");
            tipSodelovanja = Console.ReadLine();
   
        }


  

        Dobavitelj noviDob = new Dobavitelj();
    

         noviDob = new Dobavitelj(nazivDobavitelja, naslovDobavitelja, dacvna, kontaktTel, opisDobavitelja, dolgoRocnoSodelovanje, tipSodelovanja,gmail);


  




        List<Dobavitelj> dobaviteljiSeznam = Dobavitelj.beriXML_Dob(path);
        bool dobExist = dobaviteljiSeznam.Any(artikeltest => noviDob.naziv == artikeltest.naziv);

        if (!dobExist)
        {
            Dobavitelj.pisiXML_Dobavitelj(path, noviDob);

        }
        else
        {
            Console.WriteLine("Dobavitelj z tem nazivom ze obstaja");
        }
    }


    void dodadjArtikel(string path, List<Artikel> artikli)
    {
   
        bool naVoljo = false;
       // int dobavljiv = 1000;
        Console.WriteLine("Vnesite sledeče podatke");

        Console.WriteLine("Vnesite ime izdelka:");
        string imeIzdelka = Console.ReadLine();

        Console.WriteLine("Vnesite ceno izdelka:");
        double cenaIzdelka;

        while (true) 
        {
            string input = Console.ReadLine(); 
            if (Double.TryParse(input, out cenaIzdelka) && cenaIzdelka >= 0)
            {
                break;
            }
            Console.WriteLine("Prosimo, vnesite veljavno ceno (število 0 ali več).");
        }



        Console.WriteLine("Vnesite koliko izdelkov je na zalogi");
        int zalogaIzdelka;

        while (true) 
        {
       
            string input = Console.ReadLine();
            if (Int32.TryParse(input, out zalogaIzdelka) && zalogaIzdelka >= 0)
            {   
                break;
            }
            Console.WriteLine("Prosimo, vnesite veljavno število, ki je 0 ali več.");
        }



        Console.WriteLine("Vnesite dobavitelja izdelka");

        string nazivDobavitelja = Console.ReadLine();
        int idDobavitelja = 0;
   

        Console.WriteLine("Vnesite tip artikla: hisni - tehnicni - ekoloski");
        string tip = Console.ReadLine();
    

        Console.WriteLine("Ali je izdelek dobavljiv: 0 - NE, 1 - DA ");
        int dobavljiv = Int32.Parse(Console.ReadLine());




        idDobavitelja = Dobavitelj.preveriDobavitelje(nazivDobavitelja).Item1;
        nazivDobavitelja = Dobavitelj.preveriDobavitelje(nazivDobavitelja).Item2;

        if (idDobavitelja >= 0 && nazivDobavitelja != "")
        {
            string datumString;
            DateTime datumTime;
            while (true)
            {
                Console.WriteLine("Vnesite datum zadnje nabave");
                datumString = Console.ReadLine();


                if (DateTime.TryParse(datumString, out datumTime))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Neveljavni format datuma. Prosim poskusite ponovno.");
                }
            }


        
        
            Artikel artikel = new Artikel(imeIzdelka, cenaIzdelka, zalogaIzdelka, idDobavitelja, datumTime,false, tip);

        

            List<Artikel> artikliSeznam = Artikel.beriXML_Artikel(path,"");
            bool artikelExists = artikliSeznam.Any(artikeltest => artikel.ime == artikeltest.ime);
            if (!artikelExists)
            {
                artikel.pisiXML_Artikel(path, artikel);

            }
            else
            {
                Console.WriteLine("Artikel z tem imenom ze obstaja");
            }

        }
        else
        {
            Console.WriteLine("Narobnega dobavitelja ste vnesli. Vnesite ponovno vse podatke.");
        }
    }

    void isciSpecificneArtikle(string path, string prefiltriraniArtikli)
    {
        XNamespace ns = "http://www.example.com/dobavitelji";
        List<Artikel> filtriraniArtikli = new List<Artikel>();
        Console.WriteLine("Vnesite ime dobavitelja:");
        string dobaviteljVnos = Console.ReadLine();

        Console.WriteLine("Vnesite maksimalno število izdelkov na zalogi");
        int maksZaloga = Int32.Parse(Console.ReadLine());


        List<Artikel> artikelList = new List<Artikel>();
        List<Artikel> specArtikli = new List<Artikel>();
        List<Dobavitelj> dobaviteljList = new List<Dobavitelj>();

        artikelList = Artikel.beriXML_Artikel(path,"");
        dobaviteljList = Dobavitelj.beriXML_Dob("C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\vaje5\\DobaviteljiPet.xml");

        var dobavitelj = dobaviteljList.Where( dobavitelj=> dobavitelj.naziv == dobaviteljVnos).First();
        foreach (var art in artikelList)
        {
            if (art.dobaviteljId == dobavitelj.id && art.zaloga <= maksZaloga)
            {
                art.pisiXML_Artikel(prefiltriraniArtikli, art);
                filtriraniArtikli.Add(art);

            }
        }

        if (filtriraniArtikli.Count() > 0)
        {
            Console.WriteLine("Sledeči artikli so bili najdeni:");

            foreach (var art in filtriraniArtikli)
            {
                Console.WriteLine(art.ToString());
            }

        }


    }


    static void spremeniCenoDob(string path)
    {
        XNamespace ns = "http://www.example.com/dobaviteljiNovo";
        Console.WriteLine("Vnesite ime dobavitelja, kateremu zelite spremeniti  opis:");

        string nazivDobavitelja = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(nazivDobavitelja) || !Regex.IsMatch(nazivDobavitelja, @"^[a-zA-Z]+$"))
        {
            Console.WriteLine("Naziv dobavitelja ne sme biti prazno. Vnesite naziv dobavitelja:");
            nazivDobavitelja = Console.ReadLine();
        }

        Console.WriteLine("Vnesite novi opis:");
        string kontaktTel = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(kontaktTel) )
        {
            Console.WriteLine("Neveljaven opis. Vnesite besedilo:");
            kontaktTel = Console.ReadLine();
        }


        XDocument xdoc;
        try
        {
            xdoc = XDocument.Load(path);
        }
        catch (Exception ex)
        {
            xdoc = new XDocument(new XElement("dobavitelji"));
            Console.WriteLine($"Error loading XML: {ex.Message}");

        }
        var dobaviteljToUpdate = xdoc.Descendants(ns + "dobaviteljNovi")
            .FirstOrDefault(artikel => artikel.Element(ns + "naziv").Value.Equals(nazivDobavitelja, StringComparison.OrdinalIgnoreCase));


        if (dobaviteljToUpdate != null)
        {
            var noviOpisElement = new XElement(ns + "opisNova", kontaktTel);
            var opisElement = dobaviteljToUpdate.Element(ns + "opis");
            if (opisElement != null)
            {
                opisElement.ReplaceWith(noviOpisElement);
            }
            else
            {
           
                dobaviteljToUpdate.Add(noviOpisElement);
            }
            xdoc.Save(path);
            Console.WriteLine("Opis je bil uspešno spremenjen.");


        }
    }

    static void spremeniCeno(string path)
    {
        Console.WriteLine("Vnesite ime artikla, ki si ga želite znižati:");
        string imeIzdelka = Console.ReadLine();
        int popust = 0;
        XNamespace ns = "http://www.example.com/artikliNovo";

        while (true)
        {
            Console.WriteLine("Vnesite izbran odstotek popust(%):");
            string popustInput = Console.ReadLine();


            if (int.TryParse(popustInput, out popust) && popust >= 0 && popust <= 100)
            {
                break;
            }
            else
            {
                Console.WriteLine("Prosimo, vnesite veljavno število med 0 in 100.");
            }
        }

        XDocument xdoc;
        try
        {
            xdoc = XDocument.Load(path);
        }
        catch (Exception ex)
        {
            xdoc = new XDocument(new XElement("artikli"));
            Console.WriteLine($"Error loading XML: {ex.Message}");

        }

        var artikelToUpdate = xdoc.Descendants( ns + "razsirjenArtikel")
        .FirstOrDefault(artikel => artikel.Element(ns + "naziv").Value.Equals(imeIzdelka, StringComparison.OrdinalIgnoreCase));

        if (artikelToUpdate != null)
        {
            double currentPrice = Artikel.popraviCeno(artikelToUpdate.Element(ns + "cena").Value);
            double discountAmount = currentPrice * popust / 100;
            double newPrice = currentPrice - discountAmount;

            var novaCenaElement = new XElement(ns + "novaCena", newPrice.ToString("F2"));
            artikelToUpdate.Element(ns + "cena").ReplaceWith(novaCenaElement);


            xdoc.Save(path);
            Console.WriteLine($"Cena artikla '{imeIzdelka}' je bila znižana na {newPrice:F2}.");
        }
        else
        {
            Console.WriteLine($"Artikel z imenom '{imeIzdelka}' ni bil najden.");
        }

    }


    static void beriVse(string path)
    {
        List<Artikel> artikelList = new List<Artikel>();
        artikelList = Artikel.beriXML_Artikel(path, "");

        foreach (var art in artikelList)
        {
            Console.WriteLine(art.ToString());
        }

    }

    static void beriVseDobavitelje(string path)
    {
        List<Dobavitelj> dobaviteljlList = new List<Dobavitelj>();
        dobaviteljlList = Dobavitelj.beriXML_Dob(path);
        foreach (var dob in dobaviteljlList)
        {
            Console.WriteLine(dob.ToString());
        }

    }


