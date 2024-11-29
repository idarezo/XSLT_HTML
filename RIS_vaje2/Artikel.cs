using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace RIS_vaje2
{
    internal class Artikel
    {
       
        public int id { get; set; }
        public string ime { get; set; }
        public double cena { get; set; }
        public int zaloga { get; set; }
        public int dobaviteljId { get; set; }

        public DateTime datumZadnjeNabave { get; set; }
      

        public bool dobavljiv { get; set; }
        public string tip { get; set; }
        public Artikel()
        {

        }

        public static XNamespace ns = "http://www.example.com/artikliNovo";
        public Artikel(string ime, double cena, int zaloga, int idDob, DateTime datumZadnjeNabave, bool dobavljiv, string tip)
        {        
            this.ime = ime;
            this.cena = cena;
            this.zaloga = zaloga;
            this.dobaviteljId = idDob;
            this.datumZadnjeNabave = datumZadnjeNabave;
            this.dobavljiv = dobavljiv;
            this.tip = tip;
        }
       
      

        public void pisiXML_Artikel(string path, Artikel artikel)
        {

            int id = inicializirajStevec("C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\Vaje5\\artikli.xml");
            artikel.id = id;
            XDocument xdoc;
            XNamespace ns = "http://www.example.com/artikliNovo";


            if (System.IO.File.Exists(path))
            {
                try
                {
                    xdoc = XDocument.Load(path);
                }
                catch (XmlException)
                {
                    xdoc = new XDocument(
                    new XDocumentType("artikli", null, null, null),
                    new XElement(ns + "artikli")
                );
                }
            }
            else
            {
                xdoc = new XDocument(
                new XDocumentType("artikli", null, null, null),
                new XElement(ns + "artikli"));
            }


            XElement newArtikel;
            if (artikel is RazsirjeniArtikel razsirjenArtikel)
            {             
                newArtikel = new XElement(ns + "razsirjenArtikel",
                    new XAttribute("dodatniOpis", razsirjenArtikel.DodatniOpis),
                    new XElement(ns + "id", new XAttribute("idArt", $"_{id}"), id),
                    new XElement(ns + "naziv", razsirjenArtikel.ime),
                    new XElement(ns + "cena", $"{razsirjenArtikel.cena:F2}"),
                    new XElement(ns + "zaloga", razsirjenArtikel.zaloga),
                    new XElement(ns + "dobaviteljId", razsirjenArtikel.dobaviteljId),
                    new XElement(ns + "datum_zadnje_nabave", razsirjenArtikel.datumZadnjeNabave.ToString("yyyy-MM-dd")),
                    new XElement(ns + "noviArtikel", razsirjenArtikel.NoviArtikel),
                    new XElement(ns + "naZalogi", razsirjenArtikel.NaZalogi)
                );
            }
            else
            {
                
                newArtikel = new XElement(ns + "razsirjenArtikel",
                    new XAttribute("dobavljiv", artikel.dobavljiv),
                    new XAttribute("tip", artikel.tip),
                    new XElement(ns + "id", new XAttribute("idArt", $"_{id}"), id),
                    new XElement(ns + "naziv", artikel.ime),
                    new XElement(ns + "cena", $"{artikel.cena:F2}"),
                    new XElement(ns + "zaloga", artikel.zaloga),
                    new XElement(ns + "dobaviteljId", artikel.dobaviteljId),
                    new XElement(ns + "datum_zadnje_nabave", artikel.datumZadnjeNabave.ToString("yyyy-MM-dd")),
                    new XElement(ns + "noviArtikel"),
                    new XElement(ns + "naZalogi")
                );
            }

            xdoc.Root.Add(newArtikel);
            xdoc.Save(path);
            bool isValid =ValidateXml(path).Item1;
            string errorMsg = ValidateXml(path).Item2;

            if (isValid) {

                Console.WriteLine("Valid XML.");
            }
            else
            {
                Odstrani(xdoc,path, newArtikel);
                Console.WriteLine($"{errorMsg}");
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

           
            settings.Schemas.Add(null, "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\Vaje5\\ArtikliPet.xsd");
            settings.Schemas.Add(null, "C:\\Users\\991460\\Desktop\\3Letnik\\RIS\\Vaje5\\artikli_types.xsd");

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


        public static void beriPovprecjeVsotaMaxCen(string path,string filter,string filterDva)
        {
          
            XPathDocument docNav = new XPathDocument(path);
            XPathNavigator nav = docNav.CreateNavigator();

            XmlNamespaceManager nsManager = new XmlNamespaceManager(nav.NameTable);
            nsManager.AddNamespace("ns", "http://www.example.com/artikliNovo");  


            string vsotaCen = nav.Evaluate("sum(//ns:razsirjenArtikel/ns:cena)", nsManager).ToString();      
            string steviloArtiklov = nav.Evaluate("count(//ns:razsirjenArtikel)", nsManager).ToString();


            if (Convert.ToInt32(steviloArtiklov) > 0)
            {

                if (filter == "povprecje")
                {
                    double povprecje = Convert.ToDouble(vsotaCen) / Convert.ToInt32(steviloArtiklov);
                    double povprecjeRounded = Math.Round(povprecje, 2);
                    Console.WriteLine("Povprečna cena vseh artiklov: " + povprecjeRounded);
                }
                else if (filter == "vsota")
                {
                    Console.WriteLine("Vsota cen vseh artiklov je:" + vsotaCen);
                }
                else if (filter == "najdrazji")
                {
                    XPathNodeIterator ceneIterator = nav.Select("//ns:razsirjenArtikel/ns:cena", nsManager);
                    List<double> cene = new List<double>();
                    while (ceneIterator.MoveNext())
                    {
                        if (double.TryParse(ceneIterator.Current.Value, out double price))
                        {
                            cene.Add(price);
                        }
                    }
                    double iskanaCena = cene.Max();
                    List<Artikel> vsiArtikli = beriXML_Artikel(path, "");
                    var vsiArtikliNovo = vsiArtikli.Where(artikel => artikel.cena == iskanaCena);

                    if (vsiArtikliNovo.Count() > 1)
                    {
                        Console.WriteLine("Sledeci artikli imajo najvisjo ceno:");
                        foreach (var artikel in vsiArtikliNovo)
                        {
                            Console.WriteLine(artikel.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sledeci artikel je najdrazji:");
                        foreach (var artikel in vsiArtikliNovo)
                        {
                            Console.WriteLine(artikel.ToString());

                        }
                    }
                }
                else if (filter == "true")
                {
                    XPathNodeIterator naVoljiArtikli = nav.Select("//ns:razsirjenArtikel[@dobavljiv='true']", nsManager);
                    Console.WriteLine("Sledeci artikli so na voljo:");
                    while (naVoljiArtikli.MoveNext())
                    {
                        Console.WriteLine(naVoljiArtikli.Current.SelectSingleNode("ns:naziv", nsManager).Value);
                    }

                }
                else if (filter == "tip")
                {
                    XPathNodeIterator tehnicniArtikli = nav.Select($"//ns:razsirjenArtikel[@tip='{filterDva}']", nsManager);
                    Console.WriteLine($"Artikli tipa {filterDva} so bili najdeni:");
                    while (tehnicniArtikli.MoveNext())
                    {
                        Console.WriteLine( tehnicniArtikli.Current.SelectSingleNode("ns:naziv", nsManager).Value);
                    }
                }
                else if (filter == "maksZaloga")
                {
                    string xpath = $"//ns:razsirjenArtikel[ns:zaloga < {filterDva}]/ns:naziv";
                    XPathNodeIterator artikliIterator = nav.Select(xpath, nsManager);

                    if (artikliIterator.Count > 0)
                    {
                        Console.WriteLine("Artikli z manjso zalogo:");
                        while (artikliIterator.MoveNext())
                        {
                            Console.WriteLine(artikliIterator.Current.Value);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ni artiklov z manjšim številom na zalogi kot " + filterDva);
                    }

                }
                else if (filter == "stArtPoTipu")
                {
                    string xpath = $"count(//ns:razsirjenArtikel[@tip='{filterDva}'])";
                    string stevilo = nav.Evaluate(xpath, nsManager).ToString();

                    Console.WriteLine("Število artiklov tipa " + filterDva + ": " + stevilo);
                }
                else if (filter == "tipNajvecPojav")
                {
                    string xpath = "//ns:razsirjenArtikel/@tip";
                    XPathNodeIterator tipiIterator = nav.Select(xpath, nsManager);

                  
                    Dictionary<string, int> tipCount = new Dictionary<string, int>();
                    while (tipiIterator.MoveNext())
                    {
                        string tip = tipiIterator.Current.Value;

                        if (tipCount.ContainsKey(tip))
                        {
                            tipCount[tip]++;
                        }
                        else
                        {
                            tipCount[tip] = 1;
                        }
                    }

                   
                    string najpogostejsiTip = tipCount.OrderByDescending(t => t.Value).FirstOrDefault().Key;
                    int steviloPojavitev = tipCount[najpogostejsiTip];

                    Console.WriteLine($"Najpogostejši tip artikla je '{najpogostejsiTip}', ki se pojavi {steviloPojavitev} krat.");


                }

            }
            else
            {
                Console.WriteLine("Ni artiklov v seznamu.");
            }

            

        }

        


        public static List<Artikel> beriXML_Artikel(string path,string filter)
        {
                XPathDocument docNav;
                XPathNavigator nav;

              
            try
            {         
                    docNav = new XPathDocument(path);
                    nav = docNav.CreateNavigator();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Napaka pri branju XML datoteke: " + ex.Message);
                    return new List<Artikel>(); 
                }

            XmlNamespaceManager nsManager = new XmlNamespaceManager(nav.NameTable);
            nsManager.AddNamespace("ns", "http://www.example.com/artikliNovo");
            List<Artikel> artikliSeznam = new List<Artikel>();

            if (filter=="")
            {
                string xpathExpression = "//ns:razsirjenArtikel";

                XPathNodeIterator artikliIterator = nav.Select(xpathExpression, nsManager);


                while (artikliIterator.MoveNext())
                {
                    var artikelNode = artikliIterator.Current;

                    Artikel artikel = new Artikel
                    {
                        ime = artikelNode.SelectSingleNode("ns:naziv", nsManager)?.Value,
                        cena = artikelNode.SelectSingleNode("ns:novaCena", nsManager) != null
                         ? popraviCeno(artikelNode.SelectSingleNode("ns:novaCena", nsManager)?.Value)
                         : popraviCeno(artikelNode.SelectSingleNode("ns:cena", nsManager)?.Value),
                        zaloga = int.TryParse(artikelNode.SelectSingleNode("ns:zaloga", nsManager)?.Value, out int zalogaValue) ? zalogaValue : 0,
                        dobaviteljId = int.TryParse(artikelNode.SelectSingleNode("ns:dobaviteljId", nsManager)?.Value, out int dobaviteljIdValue) ? dobaviteljIdValue : 0,
                        datumZadnjeNabave = DateTime.TryParse(artikelNode.SelectSingleNode("ns:datum_zadnje_nabave", nsManager)?.Value, out DateTime datumValue) ? datumValue : DateTime.MinValue,
                        dobavljiv = bool.TryParse(artikelNode.GetAttribute("dobavljiv", ""), out bool dobavljivValue) ? dobavljivValue : false,
                        tip = artikelNode.GetAttribute("tip", "") 
                    };


                    artikliSeznam.Add(artikel);
                }

                return artikliSeznam;
            }
            else
            {
                string xpathExpression = $"//ns:razsirjenArtikel[ns:cena > {filter}]";

                XPathNodeIterator artikelIterator = nav.Select(xpathExpression, nsManager);
                while (artikelIterator.MoveNext())
                {
                    var artikelNode = artikelIterator.Current;

                    Artikel artikel = new Artikel
                    {
                        ime = artikelNode.SelectSingleNode("ns:naziv", nsManager)?.Value,
                        cena = artikelNode.SelectSingleNode("ns:novaCena", nsManager) != null
                         ? popraviCeno(artikelNode.SelectSingleNode("ns:novaCena", nsManager)?.Value)
                         : popraviCeno(artikelNode.SelectSingleNode("ns:cena", nsManager)?.Value),
                        zaloga = int.TryParse(artikelNode.SelectSingleNode("ns:zaloga", nsManager)?.Value, out int zalogaValue) ? zalogaValue : 0,
                        dobaviteljId = int.TryParse(artikelNode.SelectSingleNode("ns:dobaviteljId", nsManager)?.Value, out int dobaviteljIdValue) ? dobaviteljIdValue : 0,
                        datumZadnjeNabave = DateTime.TryParse(artikelNode.SelectSingleNode("ns:datum_zadnje_nabave", nsManager)?.Value, out DateTime datumValue) ? datumValue : DateTime.MinValue,
                        dobavljiv = bool.TryParse(artikelNode.GetAttribute("dobavljiv", ""), out bool dobavljivValue) ? dobavljivValue : false,
                        tip = artikelNode.GetAttribute("tip", "") 
                    };


                    artikliSeznam.Add(artikel);
                }

                return artikliSeznam;
            }

        }

        public override string ToString()
        {
            return $"{ime}";
        }

        public static double popraviCeno(string priceString)
        {

            priceString = priceString.Trim();

            if (priceString.EndsWith(" EUR"))
            {
                priceString = priceString.Substring(0, priceString.Length - 4).Trim();
            }
            priceString = priceString.Replace(",", ".");
            return Double.Parse(priceString, CultureInfo.InvariantCulture);
        }

        private static void Odstrani(XDocument dokument, string path, XElement element)
        {
            XNamespace ns = "http://www.example.com/artikliNovo";

            var artikli = dokument.Descendants(ns + "razsirjenArtikel").ToList();
            if (artikli.Any())
            {

                var iskaniArtikel = artikli.FirstOrDefault(a => a.Element(ns+"naziv").Value == element.Element(ns+"naziv").Value);


                if (iskaniArtikel != null)
                {
                    iskaniArtikel.Remove();
                    dokument.Save(path);
                   
                }
                else
                {
                   
                }
            }
            else
            {
              
            }
        }

        private static int inicializirajStevec(string path)
        {
            XDocument xdoc;
            int idGeneriran = 0;
          
            try
            {
                xdoc = XDocument.Load(path);
            }
            catch (System.IO.FileNotFoundException)
            {
                idGeneriran = 0;
                return idGeneriran;
            }
            catch (System.Xml.XmlException)
            {
                xdoc = new XDocument(
            new XElement( ns + "razsirjenArtikel") 
        );

                
                xdoc.Save(path);
            }

            var maxId = xdoc.Descendants(ns + "razsirjenArtikel")
                            .Select(a => (int?)int.Parse(a.Element(ns + "id").Value))
                            .Max();
            if (maxId==null)
            {
              return 0;
            }
            else
            {
                return (int)maxId + 1;

            }

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
