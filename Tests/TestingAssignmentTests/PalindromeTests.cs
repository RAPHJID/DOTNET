using System;
using TestingAssignment; 

namespace TestingAssignmentTests
{
    [TestFixture]
    public class PalindromeTest
    {
        [Test]
        public void IsPalindrome_WhenCalledPalindrome_ReturnTrue()
        {
            //instanciate
            Palindrome pd = new Palindrome();
            // AAA
            string input = "A man, a plan, a canal, Panama";
            bool result = pd.IsPalindrome(input);
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsPalindrome_WhenCalledAndIsNotPalindrome_ReturnFalse()
        {
            //instanciate
            Palindrome pd = new Palindrome();
            // AAA
            string input = "Hello World";
            bool result = pd.IsPalindrome(input);
            Assert.That(result, Is.False);
        }
    }
}
