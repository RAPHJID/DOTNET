using TestingAssignment; 

namespace TestingAssignment.Tests
{
    [TestFixture]
    public class SubArraysTests
    {
        [Test]
        public void ChunkArray_Test()
        {
            // AAA
            SubArrays subArrays = new SubArrays();
            int[] array = { 1, 2, 3, 3, 4, 5, 6, 7 };
            int chunkSize = 3;
            var result = subArrays.ChunkArray(array, chunkSize);
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(3, result[0].Count);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, result[0]);
        }
    }
}
