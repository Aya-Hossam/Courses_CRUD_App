using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace final_project.Models
{
    public class Course
    {
        [Key]
        public int CourseId {get; set;}

        [MinLength(5, ErrorMessage = "Course name can't be less than 5 characters")]
        public string CourseName {get; set;} = "";
        
        public int minDegree {get; set;}

        [ForeignKey("Department")]
        public int DepId {get; set;}
        public virtual Department Dep {get; set;}

        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public ICollection<crsResult> crsResults { get; set; } = new List<crsResult>();
    }
}