using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingAssignment;

namespace TestingAssignmentTests
{
   
       
        [TestFixture]
        public class TraineeTests
        {
            [Test]
            public void AddStudent_ValidName_Success()
            {
              
                // AAA
                Trainee trainee = new Trainee();
                trainee.addStudent("John Doe");
                Assert.AreEqual(1, trainee.student_count);
            }

            [Test]
            public void AddStudent_DuplicateName_ThrowsException()
            {
                //AAA
                Trainee trainee = new Trainee();
                trainee.addStudent("John Doe");
                Assert.Throws<InvalidOperationException>(() => trainee.addStudent("John Doe"));
            }

            [Test]
            public void RemoveStudent_ExistingName_Success()
            {
                // AAA
                Trainee trainee = new Trainee();
                trainee.addStudent("John Doe");
                trainee.removeStudent("John Doe");
                Assert.AreEqual(0, trainee.student_count);
            }

            [Test]
            public void RemoveStudent_NotExisting_ThrowsException()
            {
                // AAA
                Trainee trainee = new Trainee();
                Assert.Throws<InvalidOperationException>(() => trainee.removeStudent("Nonexistent Name"));
            }

            [Test]
            public void SearchStudent_ExistingName_ReturnsName()
            {
                // AAA
                Trainee trainee = new Trainee();
                trainee.addStudent("John Doe");
                var result = trainee.SearchStudent("John Doe");
                Assert.AreEqual("John Doe", result);
            }

            [Test]
            public void SearchStudent_NotExist_ReturnsEmptyString()
            {
                // AAA
                Trainee trainee = new Trainee();
                var result = trainee.SearchStudent("Nonexistent Name");
                Assert.AreEqual(string.Empty, result);
            }
        }
    }
