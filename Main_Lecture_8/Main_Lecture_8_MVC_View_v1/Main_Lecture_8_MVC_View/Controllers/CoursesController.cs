﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main_Lecture_8_MVC_View.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Models;

namespace Web.Api.Demo.Controllers
{
    public class CoursesController : Controller
    {
        private readonly Repository _repository;
        private readonly IConfiguration _configuration;

        public CoursesController(Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        //MVC.View

        [HttpGet]
        public IActionResult AllCourses()
        {
            var allCourses = _repository.GetAllCourses();
            return View("Courses", allCourses);
        }


        //Edit Course
        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            var course = _repository.GetCourse(id);
            ViewData["action"] = "EditCourse";
            return View("SaveCourse", course);
        }

        [HttpPost]
        public IActionResult EditCourse([FromForm] Course course)
        {
            _repository.UpdateCourse(course);
            return RedirectToAction("AllCourses");
        }

        //Create Course
        [HttpGet]
        public IActionResult CreateCourse()
        {
            var course = new Course();
            ViewData["action"] = "CreateCourse";
            return View("SaveCourse", course);
        }

        [HttpPost]
        public IActionResult CreateCourse([FromForm] Course course)
        {
            _repository.CreateCourse(course);
            return RedirectToAction("AllCourses");
        }

        //Delete Course
        public IActionResult DeleteCourse(int id)
        {
            _repository.DeleteCourse(id);
            return RedirectToAction("AllCourses");
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
            return RedirectToAction("AssignStudents");

        }

        //Assign Lecturer to Course
        [HttpGet]
        public IActionResult AssignLecturers(int id)
        {
            var course = _repository.GetCourse(id);
            CourseLecturersAssignmentViewModel model = new CourseLecturersAssignmentViewModel()
            {
                Name = course.Name,
                Id = id,
                EndDate = course.EndDate,
                PassCredits = course.PassCredits,
                StartDate = course.StartDate,
                Lecturers = new List<LectureViewModel>()
            };

            var allLecturers = _repository.GetAllLecturers();
            foreach (var lecturer in allLecturers)
            {
                bool isAssigned = course.Lecturers.Any(p => p.Id == lecturer.Id);
                model.Lecturers.Add(new LectureViewModel()
                {
                    IsAssigned = isAssigned,
                    LectureFullName = lecturer.Name,
                    LectureId = lecturer.Id
                });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult AssignLecturers([FromForm] CourseLecturersAssignmentViewModel model)
        {
            _repository.SetLecturersToCourse(model.Id, model.Lecturers.Where(p => p.IsAssigned).Select(p => p.LectureId));
            return RedirectToAction("AssignLecturers");

        }

        //HomeTasksByCourseId
        [HttpGet]
        public IActionResult HomeTasksByCourseId(int id)
        {
            var course = _repository.GetCourse(id);
            CourseHomeTasksViewModel model = new CourseHomeTasksViewModel()
            {
                Name = course.Name,
                Id = id,
                EndDate = course.EndDate,
                PassCredits = course.PassCredits,
                StartDate = course.StartDate,
                HomeTasks = new List<HomeTasksViewModel>()
            };

            var allHomeTasks = _repository.GetHomeTasksByCourseId(id);
            foreach (var homeTask in allHomeTasks)
            {
                model.HomeTasks.Add(new HomeTasksViewModel()
                {
                    HomeTasksTitle = homeTask.Title,
                    HomeTasksDescription = homeTask.Description,
                    HomeTasksNumber = homeTask.Number,
                    HomeTasksDate = homeTask.Date,
                    HomeTasksId = homeTask.Id
                });
            }
            return View("HomeTasksByCourse", model);
        }
    }

}






