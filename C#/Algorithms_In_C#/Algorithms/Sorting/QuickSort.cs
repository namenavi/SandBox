namespace Sorting
{
    public class QuickSort
    {
        public static int[] Sort(int[] sut)
        {
            var arr = sut.ToArray();
            SortImpl(arr, 0, arr.Length - 1);

            return arr;
        }

        private static void SortImpl(int[] arr, int lo, int hi)
        {
            if(lo >= hi)
            {
                return;
            }

            var p = Partition(arr, lo, hi);

            SortImpl(arr, lo, p);
            SortImpl(arr, p + 1, hi);
        }

        private static int Partition(int[] arr, int lo, int hi)
        {
            var pivot = arr[lo];
            var i = lo - 1;
            var j = hi + 1;

            while(true)
            {
                do
                {
                    i++;
                } while(arr[i] < pivot);

                do
                {
                    j--;
                } while(arr[j] > pivot);

                if(i >= j)
                {
                    return j;
                }

                var tmp = arr[i];
                arr[i] = arr[j];
                arr[j] = tmp;
            }
        }
    }
}