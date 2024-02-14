namespace PopularStringAlgorithms
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            //bool res = Anagram.IsAnagramEnglish("finder", "Friend");
            //var res2 = Anagram.IsAnagramForAllLanguages("пара","Апар");
            //Console.WriteLine($"res-{res}, res2-{res2}");

            string input = "Казот";
            Console.WriteLine($"Строка \"{input}\" {(Palindrome.IsPalindrome(input) ? "является" : "не является")} палиндромом.");
        }
    }
}
