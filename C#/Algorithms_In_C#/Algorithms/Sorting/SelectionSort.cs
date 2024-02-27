
namespace Sorting
{
    public class SelectionSort
    {
        public static int[] Sort(int[] sut)
        {
            var array = sut.ToArray();
            var upperBound = array.Length;

            for(var i = 0; i < upperBound; i++)
            {
                var minIndex = i;
                var maxIndex = upperBound - 1;

                for(var j = i + 1; j < upperBound; j++)
                {
                    if(array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                    else if(array[j] > array[maxIndex])
                    {
                        maxIndex = j;
                    }
                }

                if(minIndex != i)
                {
                    var tmp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = tmp;
                }

                if(maxIndex != upperBound - 1)
                {
                    var tmp = array[upperBound - 1];
                    array[upperBound - 1] = array[maxIndex];
                    array[maxIndex] = tmp;
                }

                upperBound--;
            }

            return array;
        }
    }
}