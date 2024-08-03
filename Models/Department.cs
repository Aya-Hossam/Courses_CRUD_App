using System.ComponentModel.DataAnnotations;

namespace final_project.Models
{
    public class Department
    {
        [Key]
        public int DepId {get; set;}

        [StringLength(50, ErrorMessage = "Department name can't exceed 50 characters")]
        public string DepName {get; set;} = "";

        public string DepManager {get; set;} = "";

        public ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();

        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

        public ICollection<Course> Courses { get; set; } = new List<Course>();

        
    }
}