using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAssignment
{
    public class MostAppearing
    {
        public void Main()
        {
            Console.WriteLine("Enter a string:");
            string input = Console.ReadLine();

            char mostAppearingChar = GetMostAppearingChar(input);

            Console.WriteLine($"The most appearing character in the entered string is: '{mostAppearingChar}'");
        }

        public char GetMostAppearingChar(string input)
        {
          
            Dictionary<char, int> charFrequencies = new Dictionary<char, int>();

            foreach (char c in input)
            {
                if (charFrequencies.ContainsKey(c))
                {
                    charFrequencies[c]++;
                }
                else
                {
                    charFrequencies[c] = 1;
                }
            }

            char mostAppearingChar = charFrequencies.OrderByDescending(pair => pair.Value).First().Key;

            return mostAppearingChar;
        }
    }

}
