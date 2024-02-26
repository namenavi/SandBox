using Sorting;

namespace Algorithms.Test
{
    public class SortingTests
    {
        private static readonly int[] Sut;
        private static readonly int[] Expected;
        static SortingTests()
        {
            Sut = File
              .ReadAllBytes("rand.bin")
              .Select(Convert.ToInt32)
              .ToArray();
            Expected = Sut.OrderBy(x => x).ToArray();
        }

        [Fact]
        public void BubbleSortTest()
        {
            var sorted = BubbleSort.Sort(Sut);
            sorted.Should().ContainInOrder(Expected);
        }

        [Fact]
        public void InsertionSortTest()
        {
            var sorted = InsertionSort.Sort(Sut);
            sorted.Should().ContainInOrder(Expected);
        }
        [Fact]
        public void QuickSortTest()
        {
            var sorted = QuickSort.Sort(Sut);
            sorted.Should().ContainInOrder(Expected);
        }

        [Fact]
        public void SelectionSortTest()
        {
            var sorted = SelectionSort.Sort(Sut);
            sorted.Should().ContainInOrder(Expected);
        }
    }
}