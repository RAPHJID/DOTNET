using System;
using TestingAssignment; 

namespace TestingAssignmentTests
{
    [TestFixture]
    public class MostAppearingTest
    {
        [Test]
        public void GetMostAppearingChar_WhenCalled_ReturnsMostAppearingChar()
        {
            // TripleA
            MostAppearing ma = new MostAppearing();
            string input = "abcaabb";
            char result = ma.GetMostAppearingChar(input);
            Assert.That(result, Is.EqualTo('a'));
        }
    }
}

