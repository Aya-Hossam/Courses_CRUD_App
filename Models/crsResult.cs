using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace final_project.Models
{
    public class crsResult
    {
        [Key]
        public int ResId {get; set;}

        public int ResDegree {get; set;}


        [ForeignKey("Course")]
        public int CrsId {get; set;}
        public virtual Course Crs {get; set;}


        [ForeignKey("Trainee")]
        public int TraineeId {get; set;}
        public virtual Trainee Trainee {get; set;}
    }
}