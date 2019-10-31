using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Models;

namespace Web.Api.Demo.Controllers
{
    public class StudentsController : Controller
    {
        private readonly Repository _repository;

        private readonly IConfiguration _configuration;

        public StudentsController(Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        //MVC.View

        [HttpGet]
        public IActionResult AllStudents()
        {
            //ViewData["myVal"] = "bla bla";
            //ViewBag.myVal = "overriden";

            var allStudents = _repository.GetAllStudents();
            return View("Students", allStudents);
        }

        //Edit Student
        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var student = _repository.GetStudentById(id);
            ViewData["action"] = "EditStudent";
            return View("SaveStudent", student);
        }

        [HttpPost]
        public IActionResult EditStudent([FromForm] Student student)
        {
            _repository.UpdateStudent(student);
            return RedirectToAction("AllStudents");
        }

        //Create Student
        [HttpGet]
        public IActionResult CreateStudent()
        {
            var student = new Student();
            ViewData["action"] = "CreateStudent";
            return View("SaveStudent", student);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromForm] Student student)
        {
            _repository.CreateStudent(student);
            return RedirectToAction("AllStudents");
        }

        //Delete Student
        public IActionResult DeleteStudent(int id)
        {
            _repository.DeleteStudent(id);
            return RedirectToAction("AllStudents");
        }


    }
}
