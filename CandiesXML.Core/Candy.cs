namespace CandiesXML.Core
{
    public class Candy
    {
        public string Id { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string Production { get; set; } = String.Empty;
        public string[] Ingradients { get; set; } = Array.Empty<string>();
        public int Energy { get; set; }
        public CandyValue CandyValue { get; set; } = new CandyValue();
        public CandyType[] CandyType { get; set; } = Array.Empty<CandyType>();
    }
}