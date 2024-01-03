using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAssignment
{
    public class Palindrome
    {
        
        public void Main()
        {
            Console.WriteLine("Enter a palindrome string:");
            string input = Console.ReadLine();

            if (IsPalindrome(input))
            {
                Console.WriteLine($"{input} yes it is a palindrome! ");
            }
            else
            {
                Console.WriteLine($"{input} NOT a palindrome X.");
            }
        }

        public bool IsPalindrome(string input)
        {
            string Input = new string(Array.FindAll(input.ToCharArray(), char.IsLetterOrDigit)).ToLower();
            string reversed = new string(Input.ToCharArray().Reverse().ToArray());

            return Input == reversed;
        }
    }

}

