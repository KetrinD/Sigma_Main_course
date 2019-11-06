using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Models.Models;
using Microsoft.Extensions.Configuration;

namespace Web.Api.Demo.Controllers
{
    //worked version
    public class StudentController : Controller
    {
        private readonly Repository _repository;

        private readonly IConfiguration _configuration;

        public StudentController(Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        //MVC.View

        [HttpGet]
        public IActionResult Students()
        {
            //ViewData["myVal"] = "bla bla";
            //ViewBag.myVal = "overriden";

            var allStudents = _repository.GetAllStudents();
            return View("Students", allStudents);
        }

        //Edit Student
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _repository.GetStudentById(id);
            ViewData["action"] = "Edit";
            return View("Edit", student);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Student student)
        {
            _repository.UpdateStudent(student);
            return RedirectToAction("Students");
        }

        //Create Student
        [HttpGet]
        public IActionResult Create()
        {
            var student = new Student();
            ViewData["action"] = "Create";
            return View("Edit", student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _repository.CreateStudent(student);
            return RedirectToAction("Students");
        }

        //Delete Student
        public IActionResult Delete(int id)
        {
            _repository.DeleteStudent(id);
            return RedirectToAction("Students");
        }
    }
}
