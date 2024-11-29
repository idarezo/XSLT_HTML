// See https://aka.ms/new-console-template for more information
using RIS_vaje2;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;



class program
{
    static void Main()
    {

        List<Artikel> artikli = new List<Artikel>();
        List<Dobavitelj> dobavitelji = new List<Dobavitelj>();

        string filePathXML = "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\vaje2\\Vaje2.xml";
        string filePathXML_Dobavitelji = "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\vaje2\\Dobavitelji.xml";
        string filePathXML_specificniArtikli = "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\vaje2\\vaje2_SpecArt.xml";

       

        Artikel artikel = new Artikel("kemik",9.9,10,0,"feri", new DateTime(2024, 1, 15));
        Artikel artikel1 = new Artikel("skodelica",8.8,3,1,"tus", new DateTime(2023, 7, 4));
        Artikel artikel2 = new Artikel("racunalnik",100,4,2,"bigbang", new DateTime(2022, 12, 31));
        Artikel artikel3 = new Artikel("ponva",15,7,3,"action", new DateTime(2022, 1, 23));
        Artikel artikel4 = new Artikel("okvir",3.5,2,4,"spar", new DateTime(2022, 2, 11));
        artikli.Add(artikel);
        artikli.Add(artikel1);
        artikli.Add(artikel2);
        artikli.Add(artikel3);
        artikli.Add(artikel4);


        Dobavitelj dobavitelj = new Dobavitelj("feri","koroska ulica 93",111111,"040231199","Dobavitelj raznih izdelkov");
        Dobavitelj dobavitelj1 = new Dobavitelj("tus","vojkova ulica 88",111111,"040567892","Dobavitelj specializiran za prehrano");
        Dobavitelj dobavitelj2 = new Dobavitelj("bigbang","dominkuseva ulica 8",111111,"040543178","Dobavitelj specializiran za tehnologijo");
        Dobavitelj dobavitelj3 = new Dobavitelj("action","rozmanova ulica 32",111111,"040888999","Dobavitelj specializiran poceni nakupe");
        Dobavitelj dobavitelj4 = new Dobavitelj("spar","ulica roberta hvalca 79",111111,"040575998","Dobavitelj specializiran za mesne izdelke");
        dobavitelji.Add(dobavitelj);
        dobavitelji.Add(dobavitelj1);
        dobavitelji.Add(dobavitelj2);
        dobavitelji.Add(dobavitelj3);
        dobavitelji.Add(dobavitelj4);


        List<Artikel> artikliSeznam = Artikel.beriXML_Artikel(filePathXML);
        foreach (var art in artikli)
        {

            bool artikelExists = artikliSeznam.Any(artikeltest => art.ime == artikeltest.ime);

            if (!artikelExists)
            {
                Artikel.pisiXML_Artikel(filePathXML, art);

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




        while (true)
        {
            Console.WriteLine("Izberite željeno operacijo :");
            Console.WriteLine("1 - Dodajanje artikla , 2 - Beri vse artikle , 3 - Znizaj ceno , 4 - Išči artikle po dobavitelju, 5 - Dodaj Dobavitelja, 6 - Beri vse dobavitelje, 7 - Spremeni kontaktne informacije 8 - Zapri program");


            string porabniskiVnos = Console.ReadLine();
            
            int izbranaOperacija = 0;

            if (!int.TryParse(porabniskiVnos, out izbranaOperacija) || izbranaOperacija < 1 || izbranaOperacija > 9)
            {
                Console.WriteLine("Napačen vnos! Prosimo, izberite    število med 1 in 4.");
                continue;
            }

            switch (izbranaOperacija)
            {

               
                case 1:                  
                    dodadjArtikel(filePathXML, artikli);
                    break;               
                case 2:
                    beriVse(filePathXML);
                    break;              
                case 3:
                    spremeniCeno(filePathXML);
                    break;
                case 4:
                    isciSpecificneArtikle(filePathXML,filePathXML_specificniArtikli);
                    break;
                case 5:
                    DodajDobavitelja(filePathXML_Dobavitelji);
                    break;
                case 6:
                    beriVseDobavitelje(filePathXML_Dobavitelji);
                    break;
                case 7:
                    spremeniCenoDob(filePathXML_Dobavitelji);
                    break;
                case 8:
                    Console.WriteLine("Program se končuje.");
                    Environment.Exit(0);
                    break;
                case 9:
                    ClearXmlContent(filePathXML, filePathXML_Dobavitelji);
                    break;

                   
            }


        }

        static void DodajDobavitelja(string path)
        {
            Console.WriteLine("Vnesite sledeče podatke");

            Console.WriteLine("Vnesite naziv dobavitelja:");
            string nazivDobavitelja = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(nazivDobavitelja) || !Regex.IsMatch(nazivDobavitelja, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("Naziv dobavitelja ne sme biti prazno. Vnesite naziv dobavitelja:");
                nazivDobavitelja = Console.ReadLine();
            }


            Console.WriteLine("Vnesite naslov dobavitelja:");
            string naslovDobavitelja= Console.ReadLine();
            while (string.IsNullOrWhiteSpace(naslovDobavitelja) || !Regex.IsMatch(naslovDobavitelja, @"^[a-zA-Z\s]+[0-9]{0,3}$"))
            {
                Console.WriteLine("Naslov dobavitelja ne sme biti prazno. Vnesite pravilni naslov dobavitelja:");
                naslovDobavitelja = Console.ReadLine();
            }


            Console.WriteLine("Vnesite davcno dobavitelja");
            int dacvna;
            while (!int.TryParse(Console.ReadLine(), out dacvna) || dacvna < 0)
            {
                Console.WriteLine("Prosimo, vnesite veljavno davcno stevilko:");
            }


            Console.WriteLine("Vnesite kontaktne telefonsko dobavitelja:");
            string kontaktTel = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(kontaktTel) || !Regex.IsMatch(kontaktTel, @"^\d{9}$"))
            {
                Console.WriteLine("Neveljaven kontakt. Vnesite pravilno telefonsko (9 številk):");
                kontaktTel = Console.ReadLine();
            }


            Console.WriteLine("Vnesite opis dobavitelja:");
            string opisDobavitelja = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(opisDobavitelja) || !Regex.IsMatch(opisDobavitelja, @"^[a-zA-Z\s]+$"))
            {
                Console.WriteLine("Naslov dobavitelja ne sme biti prazno. Vnesite pravilni naslov dobavitelja:");
                opisDobavitelja = Console.ReadLine();
            }

            

            Dobavitelj noviDob = new Dobavitelj(nazivDobavitelja, naslovDobavitelja,dacvna,kontaktTel,opisDobavitelja);

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


        static void dodadjArtikel(string path, List<Artikel> artikli)
        {
          //  bool dobObstaja = false;
            Console.WriteLine("Vnesite sledeče podatke");

            Console.WriteLine("Vnesite ime izdelka:");
            string imeIzdelka = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(imeIzdelka) || !Regex.IsMatch(imeIzdelka, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("Ime izdelka ne sme biti prazno. Vnesite ime izdelka:");
                imeIzdelka = Console.ReadLine();
            }

            Console.WriteLine("Vnesite ceno izdelka:");
            double cenaIzdelka;
            while (!double.TryParse(Console.ReadLine(), out cenaIzdelka) || cenaIzdelka < 0)
            {
                Console.WriteLine("Prosimo, vnesite veljavno ceno (število večje ali enako 0):");
            }


            Console.WriteLine("Vnesite koliko izdelkov je na zalogi");
            int zalogaIzdelka;
            while (!int.TryParse(Console.ReadLine(), out zalogaIzdelka) || zalogaIzdelka < 0)
            {
                Console.WriteLine("Prosimo, vnesite veljavno količino na zalogi (celo število večje ali enako 0):");
            }

            Console.WriteLine("Vnesite dobavitelja izdelka");
         
            string nazivDobavitelja = Console.ReadLine(); 
            int idDobavitelja = 0;
            while (string.IsNullOrWhiteSpace(nazivDobavitelja) || !Regex.IsMatch(nazivDobavitelja, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("Ime dobavitelja ne sme biti prazno. Vnesite dobavitelja:");
                nazivDobavitelja = Console.ReadLine();
               
            }
            idDobavitelja = Dobavitelj.preveriDobavitelje(nazivDobavitelja).Item1;
            nazivDobavitelja = Dobavitelj.preveriDobavitelje(nazivDobavitelja).Item2;

            if (idDobavitelja > 0 && nazivDobavitelja !="")
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



                Artikel artikel = new Artikel(imeIzdelka, cenaIzdelka, zalogaIzdelka, idDobavitelja, nazivDobavitelja, datumTime);

                List<Artikel> artikliSeznam = Artikel.beriXML_Artikel(path);
                bool artikelExists = artikliSeznam.Any(artikeltest => artikel.ime == artikeltest.ime);
                if (!artikelExists)
                {
                    Artikel.pisiXML_Artikel(path, artikel);

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

        static void isciSpecificneArtikle(string path, string prefiltriraniArtikli)
        {
            List<Artikel> filtriraniArtikli = new List<Artikel>();
            Console.WriteLine("Vnesite ime dobavitelja:");
            string dobavitelj = Console.ReadLine();

            Console.WriteLine("Vnesite maksimalno število izdelkov na zalogi");
            int maksZaloga = Int32.Parse(Console.ReadLine());


            List<Artikel> artikelList = new List<Artikel>();
            List<Artikel> specArtikli = new List<Artikel>();

            artikelList = Artikel.beriXML_Artikel(path);

            foreach (var art in artikelList)
            {
                if (art.dobavitelj == dobavitelj && art.zaloga <= maksZaloga)
                {
                    Artikel.pisiXML_Artikel(prefiltriraniArtikli, art);
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
            Console.WriteLine("Vnesite ime dobavitelja, kateremu zelite spremeniti kontaktno telefonsko:");
            
            string nazivDobavitelja = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(nazivDobavitelja) || !Regex.IsMatch(nazivDobavitelja, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("Naziv dobavitelja ne sme biti prazno. Vnesite naziv dobavitelja:");
                nazivDobavitelja = Console.ReadLine();
            }

            Console.WriteLine("Vnesite novo kontaktno telefonsko:");
            string kontaktTel = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(kontaktTel) || !Regex.IsMatch(kontaktTel, @"^\d{9}$"))
            {
                Console.WriteLine("Neveljaven kontakt. Vnesite pravilno telefonsko (9 številk):");
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

            var dobaviteljToUpdate = xdoc.Descendants("dobavitelj")
           .FirstOrDefault(artikel => artikel.Element("naziv").Value.Equals(nazivDobavitelja, StringComparison.OrdinalIgnoreCase));

            if (dobaviteljToUpdate != null)
            {
                dobaviteljToUpdate.Element("kontaktTel").Value = kontaktTel;
                xdoc.Save(path);


            }
        }

        static void spremeniCeno(string path)
        {
            Console.WriteLine("Vnesite ime artikla, ki si ga želite znižati:");
            string imeIzdelka = Console.ReadLine();
            int popust = 0;
           
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

            var artikelToUpdate = xdoc.Descendants("artikel")
            .FirstOrDefault(artikel => artikel.Element("naziv").Value.Equals(imeIzdelka, StringComparison.OrdinalIgnoreCase));

            if (artikelToUpdate != null)
            {
                double currentPrice = double.Parse(artikelToUpdate.Element("cena").Value);
                double discountAmount = currentPrice * popust / 100;
                double newPrice = currentPrice - discountAmount;

                artikelToUpdate.Element("cena").Value = newPrice.ToString("F2"); 

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
            artikelList = Artikel.beriXML_Artikel(path);

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
            




        static void ClearXmlContent(string path,string pathDobavitelji)
        {         
            XDocument xdoc = XDocument.Load(path);
            xdoc.Root.RemoveAll();
            xdoc.Save(path);
            XDocument xdocDobavitelji = XDocument.Load(pathDobavitelji);
            xdocDobavitelji.Root.RemoveAll();
            xdocDobavitelji.Save(pathDobavitelji);
        }
    }


}
