using final_project.Services;
using Microsoft.EntityFrameworkCore;
using final_project.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers
{
    public class crsResultController : Controller
    {
        private readonly ApplicationDbContext context;

        public crsResultController(ApplicationDbContext context){
            this.context = context;
        }
        public IActionResult Index()
        {
            var results = context.crsResults.ToList();
            return View(results);
        }

        public IActionResult Create()
        {

            // Fetching courses from the database
            var courses = context.Courses.ToList();
            if (courses == null)
            {
                courses = new List<Course>();
            }
            // Assigning to ViewBag
            ViewBag.Crs = courses;

            // Fetching trainees from the database
            var trainees = context.Trainees.ToList();
            if (trainees == null)
            {
                trainees = new List<Trainee>();
            }
            // Assigning to ViewBag
            ViewBag.Trainee = trainees;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(crsResult crsResultForm)
        {

            var crsResult = await context.crsResults
            .Include(cr => cr.Crs)
            .Include(cr => cr.Trainee)
            .FirstOrDefaultAsync(cr => cr.ResId == crsResultForm.ResId);
            
            // int? departmentId = traineeDto.DepId; // The department ID from your DTO

            // var department = context.Departments.Find(departmentId);


            if (!ModelState.IsValid)
            {

                // Fetching courses from the database
                var courses = context.Courses.ToList();
                if (courses == null)
                {
                    courses = new List<Course>();
                }
                // Assigning to ViewBag
                ViewBag.Crs = courses;

                // Fetching trainees from the database
                var trainees = context.Trainees.ToList();
                if (trainees == null)
                {
                    trainees = new List<Trainee>();
                }
                // Assigning to ViewBag
                ViewBag.Trainee = trainees;

                // Debugging ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

            
                return View(crsResultForm);
            }

            var traineeTable = await context.Trainees.FindAsync(crsResultForm.TraineeId);
            var coursesTable = await context.Courses.FindAsync(crsResultForm.CrsId);

            if (traineeTable == null)
            {
                ModelState.AddModelError("TraineeId", "The selected trainee does not exist.");
            }

            if (coursesTable == null)
            {
                ModelState.AddModelError("CrsId", "The selected course does not exist.");
            }

            // save the new result in the database
            crsResult crsResult = new crsResult()
            {
                ResDegree = crsResultForm.ResDegree,
                ResId = crsResultForm.ResId,
                Crs = coursesTable,
                Trainee = traineeTable,
            };


            context.crsResults.Add(crsResult);
            context.SaveChanges();

            return RedirectToAction("Index", "crsResult");
        }

    }
}