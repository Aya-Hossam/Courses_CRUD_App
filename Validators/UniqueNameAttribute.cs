using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using final_project.Services;

namespace final_project.Validators
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Ensure value is a string
            if (value is not string name)
            {
                return new ValidationResult("Invalid name format.");
            }

            // Retrieve the ApplicationDbContext from the service provider
            var serviceProvider = (IServiceProvider)validationContext.GetService(typeof(IServiceProvider));
            var context = serviceProvider.GetService<ApplicationDbContext>();

            if (context == null)
            {
                return new ValidationResult("Database context not found.");
            }

            // Check if the name already exists
            var existingEntity = context.Trainees.FirstOrDefault(e => e.Name == name);

            if (existingEntity == null)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Name already exists.");
        }
    }
}
