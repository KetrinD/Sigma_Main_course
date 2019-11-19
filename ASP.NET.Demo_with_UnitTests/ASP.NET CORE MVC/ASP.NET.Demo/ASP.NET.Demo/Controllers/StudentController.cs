using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using ASP.NET.Demo.ViewModels;

    using DataAccess.ADO;

    using Microsoft.AspNetCore.Authorization;

    using Models.Models;
    using Services;

    public class StudentController : Controller
    {
        private readonly StudentService studentService;

        public StudentController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        // GET
        public IActionResult Students()
        {
            return View(studentService.GetAllStudents());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Action"] = "Create";
            var student = new Student();
            return this.View("Edit", student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Student model)
        {
            var students = new List<Student>();
            students = studentService.GetAllStudents();
            foreach (var n in students)
                if (n.Email == model.Email)
                    ModelState.AddModelError("Email", "User with the same email is already registrated");
            if (model == null)
            {
                return this.BadRequest();
            }
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Create";
                return this.View("Edit", model);
            }
            this.studentService.CreateStudent(model);
            return RedirectToAction("Students");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var student = studentService.GetStudentById(id);
            if (student == null)
            {
                return this.NotFound();
            }
            ViewData["Action"] = "Edit";
            return this.View(student);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Student model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Edit";
                return this.View("Edit", model);
            }
            studentService.UpdateStudent(model);
            return RedirectToAction("Students");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            this.studentService.DeleteStudent(id);
            return RedirectToAction("Students");
        }
       
    }
}