using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAssignment
{
    

     public class Prompt
    {
        public void Main()
        {
            Console.WriteLine("Enter a string:");
            string input = Console.ReadLine();

            int vowelCount = CountVowels(input);

            Console.WriteLine($"The number of vowels in the entered string is: {vowelCount}");
        }

        public int CountVowels(string input)
        {
            
            string lowercaseInput = input.ToLower();

            int count = 0;
            foreach (char c in lowercaseInput)
            {
                
                if ("aeiou".Contains(c))
                {
                    count++;
                }
            }

            return count;
        }
    }

}
