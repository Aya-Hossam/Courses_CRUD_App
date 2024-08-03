using final_project.Services;
using Microsoft.EntityFrameworkCore;
using final_project.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace final_project.Controllers
{
    public class TraineeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public TraineeController(ApplicationDbContext context, IWebHostEnvironment environment){
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            var trainees = context.Trainees.ToList();
            return View(trainees);
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
        public IActionResult Create(TraineeDto traineeDto)
        {
            Console.WriteLine("Inside POST request");
            if(traineeDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The image file is required");
            }
            
            // int? departmentId = traineeDto.DepId; // The department ID from your DTO

            // var department = context.Departments.Find(departmentId);


            if (!ModelState.IsValid)
            {
                Console.WriteLine(traineeDto.DepId);
                var departments = context.Departments.ToList();
                ViewBag.Dep = departments;

                // Debugging ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

            
                return View(traineeDto);
            }

            //save the image file
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(traineeDto.ImageFile!.FileName);

            Console.WriteLine(newFileName);

            string imageFullPath = Path.Combine(environment.WebRootPath, "images", newFileName);

            Console.WriteLine(imageFullPath);
            using( var stream = System.IO.File.Create(imageFullPath))
            {
                traineeDto.ImageFile.CopyTo(stream);
            }

            
            var department = context.Departments.FirstOrDefault(d => d.DepId == traineeDto.DepId);
            // save the new product in the database
            Trainee trainee = new Trainee()
            {
                Name = traineeDto.Name,
                Address = traineeDto.Address,
                ImageFileName = newFileName,
                Grade = traineeDto.Grade,
                DepId = traineeDto.DepId,
                Dep = department
            };



            context.Trainees.Add(trainee);
            context.SaveChanges();

            return RedirectToAction("Index", "Trainee");
        }

        public IActionResult Edit(int id)
        {
            // var trainee = context.Trainees.Find(id).Include(t => t.Dep); // Eagerly load the related Department entity

            var trainee = context.Trainees
            .Include(t => t.Dep) // Use Include on the DbSet to load the related Department entity
            .FirstOrDefault(t => t.Id == id);

            if(trainee == null)
            {
                return RedirectToAction("Index", "Trainee");
            }

            // create traineeDto from trainee
            var TraineeDto = new TraineeDto()
            {
                Name = trainee.Name,
                Address = trainee.Address,
                DepId = trainee.DepId,
                Grade = trainee.Grade
            };

            ViewData["TraineeId"] = trainee.Id;
            ViewData["ImageFileName"] = trainee.ImageFileName;
            ViewData["DepName"] = trainee.Dep.DepName;

            var departments = context.Departments.ToList();
            ViewBag.Dep = departments;


            return View(TraineeDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, TraineeDto traineeDto)
        {
            var trainee = context.Trainees
            .Include(t => t.Dep) // Use Include on the DbSet to load the related Department entity
            .FirstOrDefault(t => t.Id == id);

            if(trainee == null)
            {
                return RedirectToAction("Index", "Trainee");
            }

            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewData["TraineeId"] = trainee.Id;
                ViewData["ImageFileName"] = trainee.ImageFileName;
                ViewData["DepName"] = trainee.Dep.DepName;

                var departments = context.Departments.ToList();
                ViewBag.Dep = departments;

                return View(traineeDto);
            }

            //update the image file if we have a new image file
            string newFileName = trainee.ImageFileName;
            if(traineeDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(traineeDto.ImageFile!.FileName);

                string imageFullPath = Path.Combine(environment.WebRootPath, "images", newFileName);
                using(var stream = System.IO.File.Create(imageFullPath))
                {
                    traineeDto.ImageFile.CopyTo(stream);
                }

                // delete the old image
                string oldImageFullPath = environment.WebRootPath + "/images/" + trainee.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);

                trainee.ImageFileName = newFileName;
            }

            var department = context.Departments.FirstOrDefault(d => d.DepId == traineeDto.DepId);

            // update the trainee in the database
            trainee.Name = traineeDto.Name;
            trainee.Address = traineeDto.Address;
            trainee.Grade = traineeDto.Grade;
            // trainee.DepId = traineeDto.DepId;
            // trainee.Dep = department;


            context.SaveChanges();

            return RedirectToAction("Index", "Trainee");
        }

        public IActionResult Details(int id)
        {
            // var trainee = context.Trainees.Find(id).Include(t => t.Dep); // Eagerly load the related Department entity

            var trainee = context.Trainees
            .Include(t => t.Dep) // Use Include on the DbSet to load the related Department entity
            .FirstOrDefault(t => t.Id == id);

            if(trainee == null)
            {
                return RedirectToAction("Index", "Trainee");
            }

            ViewData["Address"] = trainee.Address;
            ViewData["Grade"] = trainee.Grade;
            ViewData["TraineeId"] = trainee.Id;
            ViewData["ImageFileName"] = trainee.ImageFileName;
            ViewData["DepName"] = trainee.Dep.DepName;


            return View();
        }

        public IActionResult Delete(int id)
        {
            var trainee = context.Trainees
            .Include(t => t.Dep) // Use Include on the DbSet to load the related Department entity
            .FirstOrDefault(t => t.Id == id);

            if (trainee == null)
            {
                return RedirectToAction("Index", "Trainee");
            }

            string imageFullPath = environment.WebRootPath + "/images/" + trainee.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Trainees.Remove(trainee);
            context.SaveChanges(true);

            return RedirectToAction("Index", "Trainee");

        }

    }
}