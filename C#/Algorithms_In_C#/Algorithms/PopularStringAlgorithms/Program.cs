namespace PopularStringAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool res = Anagram.IsAnagramEnglish("finder", "Friend");
            var res2 = Anagram.IsAnagramForAllLanguages("пара","Апар");
            Console.WriteLine($"res-{res}, res2-{res2}");
        }
    }
}
