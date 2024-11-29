using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RIS_vaje2
{
    internal class Artikel
    {
        private static int stevec = 0;
        public int id { get; set; }
        public string ime { get; set; }
        public double cena { get; set; }
        public int zaloga { get; set; }
        public int dobaviteljId { get; set; }

        public string dobavitelj { get; set; }

        public DateTime datumZadnjeNabave { get; set; }


        public Artikel()
        {

        }

        public Artikel(string ime, double cena, int zaloga,int idDob, string dobavitelj, DateTime datumZadnjeNabave)
        {
            IdGenerator();
            this.ime = ime;
            this.cena = cena;
            this.zaloga = zaloga;
            this.dobaviteljId = idDob;  
            this.dobavitelj = dobavitelj;
            this.datumZadnjeNabave = datumZadnjeNabave; 
           
        }

        public void IdGenerator()
        {
            this.id = stevec;
            ++stevec;

        }

        public static void pisiXML_Artikel(string path, Artikel artikel)
        {
            //List<Artikel> artikliSeznam = Artikel.beriXML_Artikel(path);

            
           // bool artikliZeNalozeni = false;

           // bool artikelExists = artikliSeznam.Any(art => art.id == artikel.id);


            
                XDocument xdoc;

                if (System.IO.File.Exists(path))
                {
                    try
                    {
                        xdoc = XDocument.Load(path);
                    }
                    catch (XmlException)
                    {
                        xdoc = new XDocument(new XElement("artikli"));
                    }
                }
                else
                {
                    xdoc = new XDocument(new XElement("artikli"));
                }

                XElement newArtikel = new XElement("artikel",
                    new XElement("id", artikel.id),
                    new XElement("naziv", artikel.ime),
                    new XElement("cena", artikel.cena.ToString("F2")),
                    new XElement("zaloga", artikel.zaloga),
                    new XElement("dobavitelj", new XAttribute("id", artikel.dobaviteljId), artikel.dobavitelj),
                    new XElement("datum_zadnje_nabave", artikel.datumZadnjeNabave.ToString("yyyy-MM-dd"))
                );

                xdoc.Root.Add(newArtikel);
                xdoc.Save(path);

              

            

        }



        public static List<Artikel> beriXML_Artikel(string path)
        {
            XDocument xdoc;
            try
            {
                xdoc = XDocument.Load(path);
            }
            catch (XmlException)
            {
                xdoc = new XDocument(new XElement("artikli"));
            }
               
            
            List<Artikel> artikliSeznam= new List<Artikel>();          
            var artikli = from artikelVsi in xdoc.Document.Descendants("artikel")
                          select new Artikel
                          {
                              
                              ime = artikelVsi.Element("naziv").Value,
                              cena = Double.Parse(artikelVsi.Element("cena").Value),
                              zaloga = Int32.Parse(artikelVsi.Element("zaloga").Value),
                              dobaviteljId = Int32.Parse(artikelVsi.Element("dobavitelj").Attribute("id").Value),
                              dobavitelj = artikelVsi.Element("dobavitelj").Value,
                              datumZadnjeNabave = DateTime.Parse(artikelVsi.Element("datum_zadnje_nabave").Value)
                          };

            foreach (var art in artikli)
            {
                artikliSeznam.Add(art);
            }
            return artikliSeznam;
        }

        public override string ToString()
        {
            return $"{ime} - {cena} - {zaloga} - {dobaviteljId}- {dobavitelj}- {datumZadnjeNabave};";
        }

    }
}
