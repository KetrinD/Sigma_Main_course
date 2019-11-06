using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Models.Models;
using Main_Lecture_8_MVC_View.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Web.Api.Demo.Controllers
{
    //working version
    public class HomeTaskController : Controller
    {
        private readonly Repository _repository;
        private readonly IConfiguration _configuration;

        public HomeTaskController(Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        //Create
        [HttpGet]
        public IActionResult Create(int courseId)
        {
            ViewData["Action"] = "Create";
            ViewData["CourseId"] = courseId;
            var model = new HomeTask();
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Create(HomeTask homeTask,  int courseId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Create";
                ViewData["CourseId"] = courseId;
                return View("Edit", homeTask);
            }
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", courseId);
            this._repository.CreateHomeTask(homeTask, courseId);
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }


        //Edit Home Tasks
        [HttpGet]
        public IActionResult Edit(int id)
        {
            HomeTask homeTask = _repository.GetHomeTaskById(id);
            ViewData["Action"] = "Edit";
            return View("Edit", homeTask);
        }
        [HttpPost]
        public IActionResult Edit([FromForm] HomeTask homeTaskParameter)
        {
            var homeTask = this._repository.GetHomeTaskById(homeTaskParameter.Id);
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", homeTask.Course.Id);
            this._repository.UpdateHomeTask(homeTaskParameter);
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }

        //Delete
        [HttpGet]
        public IActionResult Delete(int homeTaskId, int courseId)
        {
            this._repository.DeleteHomeTask(homeTaskId);
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", courseId);
            return RedirectToAction("Edit", "Course", routeValueDictionary);
        }

        //Evaluate
        [HttpGet]
        public IActionResult Evaluate(int id)
        {
            var homeTask = this._repository.GetHomeTaskById(id);

            if (homeTask == null)
            {
                return this.NotFound();
            }

            HomeTaskAssessmentViewModel assessmentViewModel =
                new HomeTaskAssessmentViewModel
                {
                    Date = homeTask.Date,
                    Description = homeTask.Description,
                    Title = homeTask.Title,
                    HomeTaskStudents = new List<HomeTaskStudentViewModel>(),
                    HomeTaskId = homeTask.Id
                };

            if (homeTask.HomeTaskAssessments.Any())
            {
                foreach (var homeTaskHomeTaskAssessment in homeTask.HomeTaskAssessments)
                {
                    assessmentViewModel.HomeTaskStudents.Add(new HomeTaskStudentViewModel()
                    {
                        StudentFullName = homeTaskHomeTaskAssessment.Student.Name,
                        StudentId = homeTaskHomeTaskAssessment.Student.Id,
                        IsComplete = homeTaskHomeTaskAssessment.IsComplete,
                        HomeTaskAssessmentId = homeTaskHomeTaskAssessment.Id
                    });
                }
            }
            else
            {
                foreach (var student in homeTask.Course.Students)
                {
                    assessmentViewModel.HomeTaskStudents.Add(new HomeTaskStudentViewModel() { StudentFullName = student.Name, StudentId = student.Id });
                }
            }
            return this.View(assessmentViewModel);
        }

        public IActionResult SaveEvaluation(HomeTaskAssessmentViewModel model)
        {
            var homeTask = this._repository.GetHomeTaskById(model.HomeTaskId);

            if (homeTask == null)
            {
                return this.NotFound();
            }

            if (homeTask.HomeTaskAssessments.Any())
            {
                List<HomeTaskAssessment> assessments = new List<HomeTaskAssessment>();
                foreach (var homeTaskStudent in model.HomeTaskStudents)
                {
                    assessments.Add(new HomeTaskAssessment() { Id = homeTaskStudent.HomeTaskAssessmentId, Date = DateTime.Now, IsComplete = homeTaskStudent.IsComplete });
                }
                this._repository.UpdateHomeTaskAssessments(assessments);
            }
            else
            {
                foreach (var homeTaskStudent in model.HomeTaskStudents)
                {
                    var student = this._repository.GetStudentById(homeTaskStudent.StudentId);
                    homeTask.HomeTaskAssessments.Add(
                        new HomeTaskAssessment
                        {
                            HomeTask = homeTask,
                            IsComplete = homeTaskStudent.IsComplete,
                            Student = student,
                            Date = DateTime.Now

                        });
                }
                this._repository.CreateHomeTaskAssessments(homeTask.HomeTaskAssessments);
            }
            return RedirectToAction("Courses", "Course");
        }
    }
}


