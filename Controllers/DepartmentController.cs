using final_project.Services;
using Microsoft.EntityFrameworkCore;
using final_project.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext context;

        public DepartmentController(ApplicationDbContext context){
            this.context = context;
        }
        public IActionResult Index()
        {
            var departments = context.Departments.ToList();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                // Debugging ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {

                    Console.WriteLine(error.ErrorMessage); 
                }

                // Return the view with the current department object to show validation errors
                return View(department);
            }

            var newDepartment = new Department
            {
                DepName = department.DepName,
                DepManager = department.DepManager

            };

            context.Departments.Add(department);
            context.SaveChangesAsync();

            // Redirect to the Index action of the Department controller
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var department = context.Departments
            .Include(d => d.Trainees)
            .FirstOrDefault(d => d.DepId == id);


            if(department == null)
            {
                return RedirectToAction("Index", "Department");
            }

            ViewData["DepName"] = department.DepName;
            ViewData["DepManager"] = department.DepManager;
            ViewData["DepId"] = department.DepId;


            return View();
        }

        public IActionResult Delete(int id)
        {
            var department = context.Departments
            .Include(d => d.Trainees)
            .FirstOrDefault(d => d.DepId == id);

            if (department == null)
            {
                return RedirectToAction("Index");
            }

            foreach (var trainee in department.Trainees)
            {
                trainee.DepId = 7;
            }
            context.UpdateRange(department.Trainees);

            context.Departments.Remove(department);
            context.SaveChanges(true);

            return RedirectToAction("Index");

        }

        public IActionResult Edit(int id)
        {
            var department = context.Departments.Find(id);

            if(department == null)
            {
                return RedirectToAction("Index");
            }

            var Department = new Department()
            {
                DepName = department.DepName,
                DepManager = department.DepManager,
                DepId = department.DepId,
            };

            ViewData["DepId"] = department.DepId;
            ViewData["DepManager"] = department.DepManager;
            ViewData["DepName"] = department.DepName;


            return View(Department);
        }

        [HttpPost]
        public IActionResult Edit(int id, Department department)
        {
            var editDepartment = context.Departments.Find(id);

            if(editDepartment == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewData["DepId"] = department.DepId;
                ViewData["DepManager"] = department.DepManager;
                ViewData["DepName"] = department.DepName;

                return View(department);
            }

            editDepartment.DepName = department.DepName;
            editDepartment.DepManager = department.DepManager;


            context.SaveChanges();

            return RedirectToAction("Index");
        }

    }

}