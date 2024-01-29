using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Components.Models
{
    public class Student
    {
        [Required] 
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [Range(14,23)]
        public int Age { get; set; }
        //if present or not
        [Required]
        public string Status { get; set; } = string.Empty;
        [Required]
        public List<GradeAndMark> gradeAndMarks { get; set; } = new List<GradeAndMark>();
    }

    public class GradeAndMark
    {
        public int Id { get; set; }
        [Required]
        public string Grade { get; set; } = string.Empty;
        [Required]
        public int Mark { get; set; }
    }
}
