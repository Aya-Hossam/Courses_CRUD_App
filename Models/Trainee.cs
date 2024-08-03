
using System.ComponentModel;
using final_project.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace final_project.Models
{
    public class Trainee
    {
        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = "You must enter a name")]
        [MinLength(3,ErrorMessage ="Name must be more than 2 chars")]
        [MaxLength(20, ErrorMessage = "Name must be less than 21 chars")]
        [UniqueName(ErrorMessage = "Name already exists.")]
        public string Name {get; set;} = "";

        [RegularExpression("[a-zA-Z]{4,25}",ErrorMessage ="Address must be ONLY Characters")]
        public string Address {get; set;} = "";

        [Required(ErrorMessage ="Please upload an image.")]
        public string ImageFileName {get; set;} = "";

        [Range(minimum: 0, maximum: 100, ErrorMessage = "Value must be between {minimum} and {maximum}")]
        public int Grade {get; set;}


        [ForeignKey(nameof(DepId))]
        public int DepId {get; set;}
        public virtual Department Dep {get; set;}
        

        public ICollection<crsResult> crsResults { get; set; } = new List<crsResult>();

        
    }
}