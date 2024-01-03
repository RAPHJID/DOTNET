using TestingAssignment; 

namespace TestingAssignmentTests
{
    [TestFixture]
    public class ReverseTest
    {
        [Test]
        public void ReverseInt_WhenCalled_ReturnsReversedNumber()
        {
            //instanciate
            reverse rs = new reverse();
            // AAA
            int num = -123;
            int result = rs.ReverseInt(num);
            Assert.That(result, Is.EqualTo(-321)); 
        }
    }
}
