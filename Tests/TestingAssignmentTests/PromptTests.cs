using System;
using TestingAssignment; 

namespace TestingAssignmentTests
{
    [TestFixture]
    public class PromptTest
    {
        [Test]
        public void CountVowels_WhenCalled_ReturnsVowelCount()
        {
            //instanciate
            Prompt pt = new Prompt();
            // AAA
            string input = "Hello World";
            int result = pt.CountVowels(input);
            Assert.That(result, Is.EqualTo(3)); 
        }
    }
}
