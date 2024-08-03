using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using final_project.Validators;
using System.ComponentModel.DataAnnotations.Schema;



namespace final_project.Models
{
	public class TraineeDto
	{
        [Required(ErrorMessage = "You must enter a name")]
        [MinLength(3, ErrorMessage = "Name must be more than 2 chars")]
        [MaxLength(20, ErrorMessage = "Name must be less than 21 chars")]
        [UniqueName(ErrorMessage = "Name already exists.")]
        public string Name { get; set; } = "";

        [Required]
        // [RegularExpression("[a-zA-Z]{4,25}", ErrorMessage = "Address must be ONLY Characters")]
        public string Address { get; set; } = "";


        public IFormFile? ImageFile { get; set; }

        [Required]
        [Range(minimum: 0, maximum: 100, ErrorMessage = "Value must be between 0 and 100")]
        public int Grade { get; set; }


        [ForeignKey(nameof(DepId))]
        public int DepId { get; set; }
        // public virtual Department Dep { get; set; }
    }
}
