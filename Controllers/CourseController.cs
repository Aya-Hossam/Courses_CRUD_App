using final_project.Services;
using Microsoft.EntityFrameworkCore;
using final_project.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext context;

        public CourseController(ApplicationDbContext context){
            this.context = context;
        }
        public IActionResult Index()
        {
            var courses = context.Courses.ToList();
            return View(courses);
        }

        public IActionResult Create()
        {

            // Fetching departments from the database
            var departments = context.Departments.ToList();
            if (departments == null)
            {
                departments = new List<Department>();
            }
            // Assigning to ViewBag
            ViewBag.Dep = departments;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            Console.WriteLine(course);
            Console.WriteLine(course.DepId);
            
            course.Dep = context.Departments.Find(course.DepId);
            
            if (!ModelState.IsValid)
            {

                var departments = context.Departments.ToList();
                ViewBag.Dep = departments;

                // Debugging ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

            
                return View(course);
            }

            var department = context.Departments
            .Where(d => d.DepId == course.DepId)
            .FirstOrDefault();

            Console.WriteLine(department.DepName);

            // save the new product in the database
            Course newCourse = new Course()
            {
                CourseName = course.CourseName,
                minDegree = course.minDegree,
                DepId = course.DepId,
                Dep = department
            };


            context.Courses.Add(course);
            context.SaveChanges();

            return RedirectToAction("Index", "Course");
        }
    }
}