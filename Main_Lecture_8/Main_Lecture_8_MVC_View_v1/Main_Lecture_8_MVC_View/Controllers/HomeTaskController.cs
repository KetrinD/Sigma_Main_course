using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main_Lecture_8_MVC_View.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Models.Models;

namespace Web.Api.Demo.Controllers
{
    public class HomeTaskController : Controller
    {
        private readonly Repository _repository;
        private readonly IConfiguration _configuration;

        public HomeTaskController(Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        //All Home Tasks
        [HttpGet]
        public IActionResult AllHomeTasks()
        {
            var allCourseHomeTasks = _repository.GetAllHomeTasks();
            return View("HomeTasks", allCourseHomeTasks);
        }

        //Create
        [HttpGet]
        public IActionResult Create([FromRoute] int id)
        {
            ViewData["Action"] = "Create";
            ViewData["CourseId"] = id;
            var model = new HomeTask();
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Create([FromForm] HomeTask homeTask, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Create";
                ViewData["CourseId"] = id;
                return View("Edit", homeTask);
            }
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", id);

            this._repository.CreateHomeTask(homeTask, id);
            return RedirectToAction("EditCourse", "Courses", routeValueDictionary);
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
        public IActionResult Edit([FromForm] HomeTask homeTask)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["Action"] = "Edit";
            //    return View(homeTaskParameter);
            //}
            //var homeTask = this._repository.GetHomeTaskById(homeTaskParameter.Id);
            //var routeValueDictionary = new RouteValueDictionary();
            //this._repository.UpdateHomeTask(homeTaskParameter);
            //routeValueDictionary.Add("id", homeTask.Course.Id);
            //return RedirectToAction("EditCourse", "Courses", routeValueDictionary);
            _repository.UpdateHomeTask(homeTask);
            return RedirectToAction("AllHomeTasks");

        }

        ////Delete Home Tasks
        //[HttpGet]
        //public IActionResult Delete(int homeTaskId, int id)
        //{
        //    this._repository.DeleteHomeTask(homeTaskId);
        //    var routeValueDictionary = new RouteValueDictionary();
        //    routeValueDictionary.Add("id", id);
        //    return RedirectToAction("EditCourse", "Courses", routeValueDictionary);
        //}


        //Delete Home Tasks
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _repository.DeleteHomeTask(id);
            return RedirectToAction("AllHomeTasks");
        }

    }
}


////[HttpGet]
//public IActionResult Edit(int id)
//{
//    HomeTask homeTask = this._repository.GetHomeTaskById(id);
//    if (homeTask == null)
//        return this.NotFound();
//    ViewData["Action"] = "Edit";
//    return View(homeTask);
//}
//[HttpPost]
//public IActionResult Edit(HomeTask homeTaskParameter)
//{
//    if (!ModelState.IsValid)
//    {
//        ViewData["Action"] = "Edit";
//        return View(homeTaskParameter);
//    }
//    var homeTask = this._repository.GetHomeTaskById(homeTaskParameter.Id);
//    var routeValueDictionary = new RouteValueDictionary();
//    this._repository.UpdateHomeTask(homeTaskParameter);
//    routeValueDictionary.Add("id", homeTask.Course.Id);
//    return RedirectToAction("EditCourse", "Courses", routeValueDictionary);
//}


//[HttpGet]
//public IActionResult Delete(int homeTaskId, int id)
//{
//    this._repository.DeleteHomeTask(homeTaskId);
//    var routeValueDictionary = new RouteValueDictionary();
//    routeValueDictionary.Add("id", id);
//    return RedirectToAction("EditCourse", "Course", routeValueDictionary);
//}


