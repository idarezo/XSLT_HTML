using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RIS_vaje2
{
    internal class Dobavitelj
    {
        private static List<Dobavitelj>dobavitelji = new List<Dobavitelj> ();
        private static int stevec = 0 ;
        private int id { get; set; }
        public string naziv { get; set; }
        public string naslov { get; set; }

        public int davčnaŠtevilka { get; set; }
        public string kontaktTel { get; set; }
        public string opis { get; set; }
        


        public Dobavitelj()
        {

        }

        public Dobavitelj(string naziv,string naslov, int davcna, string kontakt, string opis)
        {
            IdGenerator();
            this.naziv = naziv;
            this.naslov = naslov;
            this.davčnaŠtevilka = davcna;
            this.kontaktTel = kontakt;
            this.opis = opis;
            dobavitelji.Add(this);
           
        }

        public Dobavitelj(int id , string naziv, string naslov, int davcna, string kontakt, string opis)
        {
            this.id = id;
            this.naziv = naziv;
            this.naslov = naslov;
            this.davčnaŠtevilka = davcna;
            this.kontaktTel = kontakt;
            this.opis = opis;
            dobavitelji.Add(this);

        }

        public void IdGenerator()
        {
            this.id=stevec;
            ++stevec;
          
        }


        public static (int,string) preveriDobavitelje(string dobaviteljNaz)
        {
            

            var dobaviteljNajden= dobavitelji.FirstOrDefault(dobavitelji=>dobavitelji.naziv== dobaviteljNaz);
            if (dobaviteljNajden != null)
            {
                return (dobaviteljNajden.id, dobaviteljNajden.naziv);
            }
            else
            {
              
                return (0, "");
            }
        }



        public static List<Dobavitelj> beriXML_Dob(string path)
        {
            XDocument xdoc;
            try
            {
                xdoc = XDocument.Load(path);
            }
            catch (XmlException)
            {
                xdoc = new XDocument(new XElement("dobavitelji"));
            }


            List<Dobavitelj> dobaviteljiSeznam = new List<Dobavitelj>();
            var dobavitelj = from dobaviteljVsi in xdoc.Document.Descendants("dobavitelj")
                          select new Dobavitelj
                          {
                             
                              naziv = dobaviteljVsi.Element("naziv").Value,
                              naslov = dobaviteljVsi.Element("naslov").Value,
                              davčnaŠtevilka = Int32.Parse(dobaviteljVsi.Element("davčnaŠtevilka").Value),
                              kontaktTel = dobaviteljVsi.Element("kontaktTel").Value,
                              opis = dobaviteljVsi.Element("opis").Value
                             
                          };

            foreach (var dob in dobavitelj)
            {
                dobaviteljiSeznam.Add(dob);
            }
            return dobaviteljiSeznam;
        }


        public static void pisiXML_Dobavitelj(string path, Dobavitelj dobavitelj)
        {

            XDocument xdoc;
            if (System.IO.File.Exists(path))
            {
                
                try
                {
                   
                    xdoc = XDocument.Load(path);
                }
                catch (XmlException)
                {
                   
                    Console.WriteLine("Neveljavni XML file.");
                    xdoc = new XDocument(new XElement("dobavitelji")); 
                }
               
            }
            else
            {
                xdoc = new XDocument(new XElement("dobavitelji"));
            }

            XElement newArtikel = new XElement("dobavitelj",
            new XElement("id", dobavitelj.id),
            new XElement("naziv", dobavitelj.naziv),
            new XElement("naslov", dobavitelj.naslov),
            new XElement("davčnaŠtevilka", dobavitelj.davčnaŠtevilka),
            new XElement("kontaktTel", dobavitelj.kontaktTel),
            new XElement("opis", dobavitelj.opis)
            );

            xdoc.Root.Add(newArtikel);
            xdoc.Save(path);


        }

        public override string ToString()
        {
            return $"{naziv} - {naziv} - {davčnaŠtevilka} - {kontaktTel}- {opis};";
        }

    }
}
