using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using serializer;
using System.Runtime.Serialization.Json;

namespace serializer
{
    [Serializable]
    [DataContract]
    public class Laptop
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Price { get; set; }

        [DataMember]
        public int Date { get; set; }

        [DataMember]
        public string Creator { get; set; }
        public Laptop(string name, int price, string creator, int date)
        {
            Name = name;
            Price = price;
            Creator = creator;
            Date = date;
        }
        public Laptop() { }

        public override string ToString()
        {
            return $"Laptop: {Creator}\nModel: {Name}\nPrice: {Price}\nDate: {Date}\n\n";
        }
    }
}
namespace dz_10._03
{
    class Program
    {
        #region XML_Func
        public static void Add_XML(string temp)
        {
            XDocument x = XDocument.Load(temp);
            XElement e = x.Root;
            e.Add(new XElement("laptop",
                new XAttribute("name", "Legion"),
                new XElement("creator", "Lenovo"),
                new XElement("price", "29999"),
                new XElement("date", "2021")));
            x.Save("File.xml");
        }

        public static void Search_XML(string temp)
        {
            XDocument x = XDocument.Load(temp);
            XElement e = x.Root;
            var search = x.Element("laptop")?
                .Elements("laptop")?
                .FirstOrDefault(p => p.Attribute("name")?.Value == "Legion 5");
            var name_s = search?.Attribute("name")?.Value;
            var company_s = search?.Element("creator")?.Value;
            var price_s = search?.Element("price")?.Value;
            var date_s = search?.Element("date")?.Value;
            Console.WriteLine($"Name: {name_s}  Age: {company_s}  Price: {price_s}  Date: {date_s}");
        }

        public static void Edit_XML(string temp)
        {
            XDocument x = XDocument.Load(temp);
            XElement e = x.Root;
            var tom = x.Element("laptop")?
                .Elements("laptop")
                .FirstOrDefault(p => p.Attribute("name")?.Value == "Legiob 5");

            if (tom != null)
            {
                var name_e = tom.Attribute("name");
                if (name_e != null) name_e.Value = "Legion 5";
                var company_e = tom.Element("creator");
                if (company_e != null) company_e.Value = "Lenovo";
                var price_e = tom.Element("price");
                if (company_e != null) company_e.Value = "39999";
                var date_e = tom.Element("date");
                if (company_e != null) company_e.Value = "2021";
                x.Save(temp);
            }
        }

        public static void Show_XML(string temp)
        {
            XDocument x = XDocument.Load(temp);
            XElement e = x.Root;
            var search = x.Element("laptop")?
                .Elements("laptop")?
                .FirstOrDefault(p => p.Attribute("name")?.Value == "Legion");
            var name_sh = search?.Attribute("name")?.Value;
            var company_sh = search?.Element("creator")?.Value;
            var price_sh = search?.Element("price")?.Value;
            var date_sh = search?.Element("date")?.Value;
            Console.WriteLine($"Name: {name_sh}  Age: {company_sh}  Price: {price_sh}  Date: {date_sh}");
        }

        public static void Delete_XML(string temp)
        {
            XDocument x = XDocument.Load(temp);
            XElement e = x.Root;
            if (e != null)
            {
                var bob = e.Elements("laptop")
                    .FirstOrDefault(p => p.Attribute("name")?.Value == "Legion");
                if (bob != null)
                {
                    bob.Remove();
                    x.Save(temp);
                }
            }
        }
        #endregion

        static void Main(string[] args)
        {
            Laptop l = new Laptop("Legion", 39999, "Lenovo", 2021);
            Laptop l2 = new Laptop("3123", 40000, "12312", 2021);
            List<Laptop> list = new List<Laptop>();
            list.Add(l);
            list.Add(l2);
            FileStream stream = new FileStream("../../data.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Laptop>));
            serializer.Serialize(stream, list);
            stream.Close();
            stream = new FileStream("../../data.xml", FileMode.Open);
            list = (List<Laptop>)serializer.Deserialize(stream);
            string s = String.Empty;
            foreach (Laptop j in list)
                s += j.ToString();
            Console.WriteLine(s);
            stream.Close();

            stream = new FileStream("../../list.json", FileMode.Create);
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Laptop>));
            jsonFormatter.WriteObject(stream, list);
            stream.Close();
            stream = new FileStream("../../list.json", FileMode.Open);
            list = (List<Laptop>)jsonFormatter.ReadObject(stream);
            s = String.Empty;
            foreach (Laptop j in list)
                s += j.ToString();
            Console.WriteLine(s);
            stream.Close();
        }
    }
}