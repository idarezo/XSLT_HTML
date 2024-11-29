using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace RIS_vaje2
{
    internal class Dobavitelj
    {

        private static List<Dobavitelj> dobavitelji = new List<Dobavitelj>();
        public static int stevec = 0;
        public int id { get; set; }
        public string naziv { get; set; }
        public string naslov { get; set; }
        public int davčnaŠtevilka { get; set; }
        public string kontaktTel { get; set; }
        public string gmail { get; set; }
        public string opis { get; set; }
        private int dolgorocnoSod { get; set; }
        public string tipSodelovanja { get; set; }

      

        public static XNamespace ns = "http://www.example.com/dobaviteljiNovo";


        public Dobavitelj()
        {

        }



        public Dobavitelj(string naziv, string naslov, int davcna, string kontakt, string opis, int dolgorocnoSod,string tipSodelovanja,string gmail)
        {
         
            this.naziv = naziv;
            this.naslov = naslov;
            this.davčnaŠtevilka = davcna;
            this.kontaktTel = kontakt;
            this.opis = opis;
            dobavitelji.Add(this);
            this.dolgorocnoSod = dolgorocnoSod;
            this.tipSodelovanja = tipSodelovanja;
            this.gmail = gmail;
            
        }

     
        public static (int, string) preveriDobavitelje(string dobaviteljNaz)
        {

            List<Dobavitelj> dobavitelji = beriXML_Dob("C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\Vaje5\\DobaviteljiPet.xml");

            var dobavitelj = dobavitelji
                     .FirstOrDefault(d => d.naziv == dobaviteljNaz);

        
            if (dobavitelj != null)
            {
                return (dobavitelj.id, dobavitelj.naziv);
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


            List<Dobavitelj> dobaviteljiSeznamLocal = new List<Dobavitelj>();
         
           

            var dobaviteljNovi = from dobaviteljVsi in xdoc.Document.Descendants(ns + "dobaviteljNovi")
                             select new Dobavitelj
                             {
                                 naziv = dobaviteljVsi.Element(ns + "naziv").Value,
                                 naslov = dobaviteljVsi.Element(ns + "naslov").Value,
                                 davčnaŠtevilka = Int32.Parse(dobaviteljVsi.Element(ns + "davčnaŠtevilka").Value),
                                 kontaktTel = dobaviteljVsi.Element(ns + "kontakt").Value,
                                 opis = dobaviteljVsi.Element(ns + "opis")?.Value ??
                                dobaviteljVsi.Element(ns + "opisNova")?.Value 



                             };

            foreach (var dob in dobaviteljNovi)
            {
                dobaviteljiSeznamLocal.Add(dob);
            }


            return dobaviteljiSeznamLocal;
        }


        public static void pisiXML_Dobavitelj(string path, Dobavitelj dobavitelj)
        {
           
            int id=  inicializirajStevec("C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\vaje5\\DobaviteljiPet.xml");
            dobavitelj.id = id;
            string errorMessage = "";
            bool xDocument = false;
            string tel = dobavitelj.kontaktTel.Substring(1);

           

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
                    xdoc = new XDocument(
                    new XDocumentType("dobavitelji", null, null, null),
                    new XElement(ns + "razsirjeniDobavitelj"));
                }

            }
            else
            {
                xdoc = new XDocument(
                    new XDocumentType("dobavitelji", null, null, null),
                    new XElement("dobaviteljNovi"));
            }

            if (dobavitelj.dolgorocnoSod == 1)
            {
                XElement newArtikel = new XElement(ns + "dobaviteljNovi",
                 new XElement(ns + "id", new XAttribute("idDob", $"_{id}"), id),
                 new XElement(ns + "naziv", dobavitelj.naziv),
                 new XElement(ns + "naslov", dobavitelj.naslov),
                 new XElement(ns + "davčnaŠtevilka", dobavitelj.davčnaŠtevilka),
                 new XElement(ns + "kontakt", new XElement(ns + "tel", $"+386 {dobavitelj.kontaktTel} "), new XElement(ns + "gmail", dobavitelj.gmail)),
                 new XElement(ns + "opis", dobavitelj.opis)     ,
                 new XElement(ns + "dolgotrajnoSodelovanje", new XAttribute("nivo", dobavitelj.tipSodelovanja))

                 );

                xdoc.Root.Add(newArtikel);
                xdoc.Save(path);
                xDocument = ValidateXml(path).Item1;
                errorMessage = ValidateXml(path).Item2;

                if (xDocument)
                {

                }
                else
                {
                    Odstrani(xdoc, path, newArtikel);
                    Console.WriteLine("Novi dobavitelj ni bil vnesen.");
                    Console.WriteLine(errorMessage);
                }
            }
            else 
            {
                XElement newArtikel = new XElement(ns + "dobaviteljNovi",
                 new XElement(ns + "id", new XAttribute("idDob", $"_{id}"), id),
                 new XElement(ns + "naziv", dobavitelj.naziv),
                 new XElement(ns + "naslov", dobavitelj.naslov),
                 new XElement(ns + "davčnaŠtevilka", dobavitelj.davčnaŠtevilka),
                 new XElement(ns + "kontakt", new XElement(ns + "tel", $"+386 {dobavitelj.kontaktTel} "), new XElement(ns + "gmail", dobavitelj.gmail)),
               new XElement(ns + "opis", string.IsNullOrEmpty(dobavitelj.opis) ? dobavitelj.opis : dobavitelj.opis)

                 );

                xdoc.Root.Add(newArtikel);
                xdoc.Save(path);
                xDocument = ValidateXml(path).Item1;
                errorMessage = ValidateXml(path).Item2;

                if (xDocument)
                {

                }
                else
                {
                    Odstrani(xdoc, path, newArtikel);
                    Console.WriteLine("Novi dobavitelj ni bil vnesen.");
                    Console.WriteLine(errorMessage);
                }
            }
            
            

            }

        public override string ToString()
        {
            return $"{naziv} - {naziv} - {davčnaŠtevilka} - {kontaktTel}- {opis};";
        }

        private static void Odstrani(XDocument dokument, string path, XElement element)
        {
            Dobavitelj.stevec -= 1;
            var dobavitelji = dokument.Descendants(ns + "dobaviteljNovi")
                              .Concat(dokument.Descendants(ns + "localSupplier"))
                              .ToList();
            if (dobavitelji.Any())
            {

                var iskaniDob = dobavitelji.FirstOrDefault(a => a.Element(ns + "id").Value == element.Element(ns + "id").Value);


                if (iskaniDob != null)
                {
                    iskaniDob.Remove();
                    dokument.Save(path);
                 
                }
                else
                {
                    Console.WriteLine("Dobavitelj not found.");
                }
            }
            else
            {
                
            }
        }

        private static (bool, string) ValidateXml(string filePath)
        {
            var messages = new StringBuilder();
            var errorMessages = "";
            bool isValid = true;

            var settings = new XmlReaderSettings
            {

                ValidationType = ValidationType.Schema
            };

            settings.Schemas.Add("http://www.example.com/dobaviteljiNovo", "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\Vaje5\\DobaviteljiPet.xsd" );
            settings.Schemas.Add("http://www.example.com/dobaviteljiNovo/types", "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\Vaje5\\Dobavitelji_types.xsd");


            settings.ValidationEventHandler += (sender, args) =>
            {
                isValid = false;
                messages.AppendLine(args.Message);
            };


            using (var reader = XmlReader.Create(filePath, settings))
            {
                try
                {
                    while (reader.Read()) { }

                }
                catch (XmlException ex)
                {
                    isValid = false;

                }
            }




            errorMessages = messages.ToString();

            return (isValid, errorMessages);

        }



        private static int inicializirajStevec(string path)
        {
            XDocument xdoc = XDocument.Load(path);
            var maxId = xdoc.Descendants()
                     .Where(e => e.Name == ns + "dobaviteljNovi")
                     .Select(a => (int?)int.Parse(a.Element(ns + "id").Value))
                     .Max();

            if (maxId == null)
            {
                return 0;
            }
            else
            {

            }
            return (int)maxId + 1;
        }

        public static void TransHTML(string xmlPath, string xsltPath, string outputHtmlPath)
        {
            try
            {
                //Nalozimo XML file
                XPathDocument myXPathDoc = new XPathDocument(xmlPath);

                // Nalozimo XSLT file
                XslCompiledTransform myXslTrans = new XslCompiledTransform();
                myXslTrans.Load(xsltPath);

                //Ustvarjanje izhodnega dokumenta
                using (XmlTextWriter myWriter = new XmlTextWriter(outputHtmlPath, Encoding.UTF8))
                {
                    myXslTrans.Transform(myXPathDoc, null, myWriter);
                }

                //Odpiranje generiranega HTML file-a
                Process process = new Process();
                process.StartInfo.FileName = outputHtmlPath;
                process.StartInfo.UseShellExecute = true; // Ensures the default program (browser) opens the file
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Napaka pri pretvorbi v HTML!" + e.Message);
            }
        }

    }
}
