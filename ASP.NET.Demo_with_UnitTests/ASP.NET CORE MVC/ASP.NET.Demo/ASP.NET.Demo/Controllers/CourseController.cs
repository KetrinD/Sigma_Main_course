﻿using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using ASP.NET.Demo.ViewModels;

    using Microsoft.AspNetCore.Authorization;

    using Models.Models;
    using Services;

    public class CourseController : Controller
    {
        private readonly CourseService courseService;
        private readonly StudentService studentService;
        private readonly LecturerService lecturerService;

        public CourseController(CourseService courseService, StudentService studentService, LecturerService lecturerService)
        {
            this.courseService = courseService;
            this.studentService = studentService;
            this.lecturerService = lecturerService;
        }

        // GET
        public IActionResult Courses()
        {
            return View(this.courseService.GetAllCourses());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Course courseParameter)
        {
            if (courseParameter == null)
            {
                return this.BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewData["action"] = nameof(this.Create);
                return this.View("Edit", courseParameter);
            }

            this.courseService.CreateCourse(courseParameter);
            return this.RedirectToAction(nameof(Courses));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["action"] = nameof(this.Create);
            return this.View("Edit", new Course());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Course course = this.courseService.GetCourse(id);
            if (course == null)
            {
                return this.NotFound();
            }
            ViewData["action"] = nameof(this.Edit);
            return this.View(course);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Course courseParameter)
        {
            if (courseParameter == null)
            {
                return this.BadRequest();
            }
            courseService.UpdateCourse(courseParameter);
            return this.RedirectToAction(nameof(Courses));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            this.courseService.DeleteCourse(id);

            return RedirectToAction("Courses");
        }

        [Authorize(Roles = "Admin,Lecturer")]
        [HttpGet]
        public IActionResult AssignStudents(int id)
        {
            Course course = this.courseService.GetCourse(id);
            if (course == null)
            {
                return this.NotFound();
            }
            var allStudents = this.studentService.GetAllStudents();
            CourseStudentAssignmentViewModel model = new CourseStudentAssignmentViewModel();

            model.Id = id;
            model.EndDate = course.EndDate;
            model.Name = course.Name;
            model.StartDate = course.StartDate;
            model.PassCredits = course.PassCredits;
            model.Students = new List<StudentViewModel>();

            foreach (var student in allStudents)
            {
                bool isAssigned = course.Students.Any(p => p.StudentId == student.Id);
                model.Students.Add(new StudentViewModel() { StudentId = student.Id, StudentFullName = student.Name, IsAssigned = isAssigned });
            }
            return this.View(model);
        }

        [Authorize(Roles = "Admin,Lecturer")]
        [HttpPost]
        public IActionResult AssignStudents(CourseStudentAssignmentViewModel assignmentViewModel)
        {
            if (assignmentViewModel == null)
            {
                return this.BadRequest();
            }
            this.courseService.SetStudentsToCourse(assignmentViewModel.Id, assignmentViewModel.Students.Where(p => p.IsAssigned).Select(student => student.StudentId));

            return RedirectToAction("Courses");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Lecturer")]
        public IActionResult AssignLecturers(int id)
        {
            var course = this.courseService.GetCourse(id);
            if (course == null)
            {
                return this.NotFound();
            }
            var allLecturers = this.lecturerService.GetAllLecturers();

            CourseLecturerAssignmentViewModel model = new CourseLecturerAssignmentViewModel();
            model.Id = id;
            model.EndDate = course.EndDate;
            model.Name = course.Name;
            model.StartDate = course.StartDate;
            model.PassCredits = course.PassCredits;
            model.Lecturers = new List<LecturersViewModel>();

            foreach (var lecturer in allLecturers)
            {
                bool isAssigned = course.Lecturers.Any(p => p.LecturerId == lecturer.Id);
                model.Lecturers.Add(new LecturersViewModel() { LecturerId = lecturer.Id, Name = lecturer.Name, IsAssigned = isAssigned });
            }

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Lecturer")]
        public IActionResult AssignLecturers(CourseLecturerAssignmentViewModel assignmentViewModel)
        {
            if (assignmentViewModel == null)
            {
                return this.BadRequest();
            }
            this.courseService.SetLecturersToCourse(assignmentViewModel.Id, assignmentViewModel.Lecturers.Where(p => p.IsAssigned).Select(lecturer => lecturer.LecturerId));

            return RedirectToAction("Courses");
        }
    }
}