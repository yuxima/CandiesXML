using CandiesXML.Core;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Schema;

namespace CandiesXML.Console
{
    public static class XmlHelper
    {
        const string XmlCandiesXmlPath = "./candies.xml";
        const string XmlCandyXsdPath = "./candy.xsd";
        const string XmlInvalidCandyXmlPath = "./invalidCandy.xml";

        public static void XmlToJson()
        {
            var xDoc = new XmlDocument();
            xDoc.Load(XmlCandiesXmlPath);
            var xRoot = xDoc.DocumentElement;

            var childNodes = xRoot!.SelectNodes("*")!;
            foreach (XmlNode n in childNodes)
                System.Console.WriteLine(JsonConvert.SerializeXmlNode(n, Newtonsoft.Json.Formatting.Indented, false));
        }

        public static Candy[] ProcessXml()
        {
            var candies = new List<Candy>();

            var xDoc = new XmlDocument();
            xDoc.Load(XmlCandiesXmlPath);
            var xRoot = xDoc.DocumentElement;

            var childNodes = xRoot!.SelectNodes("*")!;
            foreach (XmlNode n in childNodes)
            {
                var candy = new Candy
                {
                    Id = n.SelectSingleNode("@Id")?.Value ?? "",
                    Name = n.SelectSingleNode("Name")?.InnerText ?? "",
                    Production = n.SelectSingleNode("Production")?.InnerText ?? "",
                    Energy = Convert.ToInt32(n.SelectSingleNode("Energy")?.InnerText),

                };

                var candyValueNode = n.SelectSingleNode("CandyValue")!;
                candy.CandyValue = new CandyValue
                {
                    Fat = Convert.ToInt32(candyValueNode.SelectSingleNode("Fats")?.InnerText),
                    Carbohydrate = Convert.ToInt32(candyValueNode.SelectSingleNode("Carbohydrates")?.InnerText),
                    Protein = Convert.ToInt32(candyValueNode.SelectSingleNode("Proteins")?.InnerText),
                };

                var ingredientNodes = n.SelectSingleNode("Ingredients")!.ChildNodes;
                candy.Ingradients = ingredientNodes
                    .Cast<XmlNode>()
                    .Select(ingredientNode => ingredientNode.InnerText)
                    .ToArray();

                var candyTypeNodes = n.SelectSingleNode("CandyTypes")!.ChildNodes;
                candy.CandyType = candyTypeNodes
                    .Cast<XmlNode>()
                    .Select(candyTypeNode => Enum.Parse<CandyType>(candyTypeNode.InnerText))
                    .ToArray();

                candies.Add(candy);
            }

            candies.Sort(new CandyComparer());

            return candies.ToArray();
        }

        public static void ValidateXml()
        {
            var schema = new XmlSchemaSet();
            schema.Add(string.Empty, XmlCandyXsdPath);

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlInvalidCandyXmlPath);
            xmlDoc.Schemas.Add(schema);
            xmlDoc.Validate(ValidationEventHandler!);
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error)
                System.Console.WriteLine(e.Message);
        }
    }
}
