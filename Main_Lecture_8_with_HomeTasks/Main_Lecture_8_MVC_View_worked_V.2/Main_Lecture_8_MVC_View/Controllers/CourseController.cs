using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Models;
using Main_Lecture_8_MVC_View.ViewModels;

namespace Web.Api.Demo.Controllers
{
    //worked version
    public class CourseController : Controller
    {
        private readonly Repository _repository;
        private readonly IConfiguration _configuration;

        public CourseController(Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        //MVC.View
        [HttpGet]
        public IActionResult Courses()
        {
            var allCourses = _repository.GetAllCourses();
            return View("Courses", allCourses);
        }

        //Edit Course
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _repository.GetCourse(id);
            ViewData["action"] = "Edit";
            return View("Edit", course);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Course course)
        {
            _repository.UpdateCourse(course);
            return RedirectToAction("Courses");
        }

        //Create Course
        [HttpGet]
        public IActionResult Create()
        {
            var course = new Course();
            ViewData["action"] = "Create";
            return View("Edit", course);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Course course)
        {
            _repository.CreateCourse(course);
            return RedirectToAction("Courses");
        }

        //Delete Course
        public IActionResult Delete(int id)
        {
            _repository.DeleteCourse(id);
            return RedirectToAction("Courses");
        }

        //Assign Students to Course
        [HttpGet]
        public IActionResult AssignStudents(int id)
        {
            var course = _repository.GetCourse(id);
            CourseStudentAssignmentViewModel model = new CourseStudentAssignmentViewModel()
            {
                Name = course.Name,
                Id = id,
                EndDate = course.EndDate,
                PassCredits = course.PassCredits,
                StartDate = course.StartDate,
                Students = new List<StudentViewModel>()
            };

            var allstudents = _repository.GetAllStudents();
            foreach (var studeunt in allstudents)
            {
                bool isAssigned = course.Students.Any(p => p.Id == studeunt.Id);
                model.Students.Add(new StudentViewModel()
                {
                    IsAssigned = isAssigned,
                    StudentFullName = studeunt.Name,
                    StudentId = studeunt.Id
                });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult AssignStudents([FromForm] CourseStudentAssignmentViewModel model)
        {
            _repository.SetStudentsToCourse(model.Id, model.Students.Where(p => p.IsAssigned).Select(p => p.StudentId));
            return RedirectToAction("Courses");

        }

        //Assign Lecturer to Course
        [HttpGet]
        public IActionResult AssignLecturers(int id)
        {
            var course = _repository.GetCourse(id);
            CourseLecturerAssignmentViewModel model = new CourseLecturerAssignmentViewModel()
            {
                Name = course.Name,
                Id = id,
                EndDate = course.EndDate,
                PassCredits = course.PassCredits,
                StartDate = course.StartDate,
                Lecturers = new List<LecturersViewModel>()
            };

            var allLecturers = _repository.GetAllLecturers();
            foreach (var lecturer in allLecturers)
            {
                bool isAssigned = course.Lecturers.Any(p => p.Id == lecturer.Id);
                model.Lecturers.Add(new LecturersViewModel()
                {
                    IsAssigned = isAssigned,
                    Name = lecturer.Name,
                    LecturerId = lecturer.Id
                });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult AssignLecturers([FromForm] CourseLecturerAssignmentViewModel model)
        {
            _repository.SetLecturersToCourse(model.Id, model.Lecturers.Where(p => p.IsAssigned).Select(p => p.LecturerId));
            return RedirectToAction("Courses");

        }
    }

}
