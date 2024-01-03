using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingAssignment
{
    public class Trainee
    {

        private List<string> _studentList = new List<string>();
        public int student_count => _studentList.Count;

        public void addStudent(string studentName)
        {
            if (string.IsNullOrWhiteSpace(studentName))
                throw new ArgumentNullException("name is required");

            var student = _studentList.Find(x => x == studentName);
            if (student == null)
            {
                _studentList.Add(studentName);
                return;
            }
            throw new InvalidOperationException("You cant add the name twice");
        }

        public void removeStudent(string studentName)
        {
            if (string.IsNullOrWhiteSpace(studentName))
                throw new ArgumentNullException("name is required");

            var student = _studentList.Find(x => x == studentName);
            if (student != null)
            {
                _studentList.Remove(studentName);
                Console.WriteLine("Successfully removed");
                return;
            }

            throw new InvalidOperationException("name does not exist");
        }

        public string SearchStudent(string studentName)
        {
            if (string.IsNullOrWhiteSpace(studentName))
                throw new ArgumentNullException("name is required");

            var name = _studentList.Find(x => x == studentName);
            if (name != null)
            {
                return name;
            }
            return string.Empty;
        }
    }
}