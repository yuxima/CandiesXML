namespace CandiesXML.Core
{
    public class CandyComparer : IComparer<Candy>
    {
        public int Compare(Candy? x, Candy? y)
        {
            return string.Compare(x!.Name, y!.Name);
        }
    }
}
