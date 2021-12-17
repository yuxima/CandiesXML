using CandiesXML.Console;

var candies = XmlHelper.ProcessXml();
foreach (var candy in candies)
{
    Console.WriteLine($"Parsed candy with id {candy.Id} and name {candy.Name}");
}

Console.WriteLine("\nXML validation result:");
XmlHelper.ValidateXml();

Console.WriteLine("\nXML -> JSON:");
XmlHelper.XmlToJson();
