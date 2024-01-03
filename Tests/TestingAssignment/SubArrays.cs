using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAssignment
{
    public class SubArrays
    {
        public void Main()
        {
            int[] array = { 1, 2, 3, 3, 4, 5, 6, 7 };
            int chunkSize = 3;

            List<List<int>> result = ChunkArray(array, chunkSize);

            Console.WriteLine("Original Array: [" + string.Join(", ", array) + "]");
            Console.WriteLine($"Chunk Size: {chunkSize}");
            Console.WriteLine("Result:");
            foreach (var subarray in result)
            {
                Console.WriteLine("[" + string.Join(", ", subarray) + "]");
            }
        }

        public List<List<T>> ChunkArray<T>(T[] array, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentException("Chunk size must be greater than 0.");
            }

            int arrayLength = array.Length;
            int numberOfChunks = (int)Math.Ceiling((double)arrayLength / chunkSize);

            List<List<T>> result = new List<List<T>>();

            for (int i = 0; i < numberOfChunks; i++)
            {
                int start = i * chunkSize;
                int end = Math.Min((i + 1) * chunkSize, arrayLength);
                result.Add(array.Skip(start).Take(end - start).ToList());
            }

            return result;
        }
    }

}
