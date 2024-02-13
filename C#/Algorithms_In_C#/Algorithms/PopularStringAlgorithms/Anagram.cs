using System.Globalization;

namespace PopularStringAlgorithms
{
    /// <summary>
    /// Анаграмма - это слово или фраза, которая образуется путем перестановки букв другого слова или фразы. 
    /// Например, слова “кот” и “ток” являются анаграммами, 
    /// потому что они состоят из одних и тех же букв, но в разном порядке. 
    /// Анаграммы часто используются в кроссвордах, ребусах, шарадах и других головоломках.
    /// </summary>
    public class Anagram
    {
        /// <summary>
        /// Метод, который возвращает true, если две строки являются анаграммами, и false в противном случае
        /// Этот алгоритм работает за линейное время O(n), 
        /// где n - длина строк, и использует константное пространство O(1),
        /// предполагая, что строки состоят из символов ASCII. 
        /// Это один из самых эффективных алгоритмов для этой задачи.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool IsAnagramEnglish(string s1, string s2)
        {
            // Если длины строк разные, то они не могут быть анаграммами
            if(s1.Length != s2.Length)
                return false;

            // Создаем массив из 256 элементов для хранения количества вхождений каждого символа
            int[] count = new int[256];

            // Проходим по первой строке и увеличиваем счетчик для каждого символа
            foreach(char c in s1.ToLower())
                count[c]++;

            // Проходим по второй строке и уменьшаем счетчик для каждого символа
            foreach(char c in s2.ToLower())
                count[c]--;

            // Если в массиве есть ненулевые элементы, то строки не являются анаграммами
            for(int i = 0; i < 256; i++)
                if(count[i] != 0)
                    return false;

            // В противном случае они являются анаграммами
            return true;
        }

        /// <summary>
        /// Метод, который возвращает true, если две строки являются анаграммами для всех языков, и false в противном случае
        /// Этот алгоритм работает за время O(n log n), 
        /// где n - длина строк, и использует дополнительное пространство O(n) для хранения отсортированных строк. 
        /// Это один из универсальных алгоритмов для этой задачи, который подходит для разных языков и культур.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool IsAnagramForAllLanguages(string s1, string s2)
        {
            // Если длины строк разные, то они не могут быть анаграммами
            if(s1.Length != s2.Length)
                return false;

            // Создаем объект StringComparer с учетом культуры и регистра
            // Вы можете изменить параметры в зависимости от ваших требований
            StringComparer comparer = StringComparer.Create(CultureInfo.CurrentCulture, true);

            // Сортируем символы в строках с учетом культуры
            string sortedS1 = string.Concat(s1.ToLower().OrderBy(c => c));
            string sortedS2 = string.Concat(s2.ToLower().OrderBy(c => c));

            // Сравниваем отсортированные строки с помощью StringComparer
            return comparer.Equals(sortedS1, sortedS2);
        }
    }
}
