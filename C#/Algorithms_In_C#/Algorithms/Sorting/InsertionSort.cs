namespace Sorting
{
    public class InsertionSort
    {
        public static IEnumerable<int> Sort(int[] sut)
        {
            var array = sut.ToArray();

            for(var i = 1; i < array.Length; i++)
                for(var j = i; j > 0 && array[j - 1] > array[j]; j--)
                {
                    var tmp = array[j - 1];
                    array[j - 1] = array[j];
                    array[j] = tmp;
                }
            return array;
        }
    }
}
