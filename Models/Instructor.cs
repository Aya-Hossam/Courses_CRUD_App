
using System.ComponentModel;
using final_project.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace final_project.Models
{
    public class Instructor
    {
        [Key]
        public int InstId {get; set;}

        [Required(ErrorMessage = "You must enter a name")]
        [MinLength(3,ErrorMessage ="Name must be more than 2 chars")]
        [MaxLength(20, ErrorMessage = "Name must be less than 21 chars")]
        [UniqueName(ErrorMessage = "Name already exists.")]
        public string InstName {get; set;} = "";

        [RegularExpression("[a-zA-Z]{4,25}",ErrorMessage ="Address must be ONLY Characters")]
        public string InstAddress {get; set;} = "";

        [Required(ErrorMessage ="Please upload an image.")]
        public string ImageFileName {get; set;} = "";

        public int InstSalary {get; set;}


        [ForeignKey("Department")]
        public int InstDepId {get; set;}
        public virtual Department Dep { get; set; }
        
        [ForeignKey("Course")]
        public int InstCrsId {get; set;}
        public virtual Course Crs {get; set;}
    }
}