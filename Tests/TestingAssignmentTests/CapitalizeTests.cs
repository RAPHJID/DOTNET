using System;
using TestingAssignment; 

namespace TestingAssignmentTests
{
    [TestFixture]
    public class CapitalizeTest
    {

        [Test]
        public void CapitalizeWords_WhenCalled_CapitalizeFirstLetterOfEachWord()
        {

            //instanciate
            Capitalize cw = new Capitalize();
            //TripleA 
            string input = "hello there";//Arrange
            string result = cw.CapitalizeWords(input);//Act
            Assert.That(result, Is.EqualTo("Hello There"));//Assert
        }
    }
}
