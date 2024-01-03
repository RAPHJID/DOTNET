/*// See https://aka.ms/new-console-template for more information
using TestAssignment;

Console.WriteLine("Hello, World!");
Reverse reverse = new();
reverse.Main();*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class Reverse
    {
        static void Main()
        {
            int num = 123;
            int reverse = ReverseInt(num);
            Console.WriteLine($"The reverse of {num} is {reverse}");
        }
            static int ReverseInt(int num)
            {
                int reversed = 0;
                while (num != 0)
                {
                    int digit = num % 10;
                    reversed = reversed * 10 + digit;
                    num /= 10;
                }
                return reversed;

            
        }
    }
}
