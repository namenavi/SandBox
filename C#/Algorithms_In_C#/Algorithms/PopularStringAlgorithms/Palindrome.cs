using System.Text.RegularExpressions;

namespace PopularStringAlgorithms
{
    internal partial class Program
    {
        /// <summary>
        /// Палиндром - это слово, фраза или последовательность символов, которая одинаково читается как в прямом, 
        /// так и в обратном направлении. Например, число 101; слова «топот» в русском языке
        /// </summary>
        class Palindrome
        {
            /// <summary>
            /// Функция, которая проверяет, является ли строка палиндромом
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public static bool IsPalindrome(string s)
            {
                // Удаляем из строки все символы, кроме букв и цифр, и приводим ее к нижнему регистру
                s = Regex.Replace(s, "[^a-zа-яё0-9]", "", RegexOptions.IgnoreCase).ToLower();

                return s == Reverse(s);
            }

            /// <summary>
            /// Метод, который переворачивает строку
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            static string Reverse(string s)
            {
                char[] chars = s.ToCharArray();

                // Меняем местами символы в массиве с начала и с конца
                for(int i = 0; i < chars.Length / 2; i++)
                {
                    char temp = chars[i];
                    chars[i] = chars[chars.Length - i - 1];
                    chars[chars.Length - i - 1] = temp;
                }

                return new string(chars);
            }
        }
    }
}
