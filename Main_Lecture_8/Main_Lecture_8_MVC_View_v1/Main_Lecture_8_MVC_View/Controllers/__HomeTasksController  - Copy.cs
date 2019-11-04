//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Main_Lecture_8_MVC_View.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.Extensions.Configuration;
//using Models.Models;

//namespace Web.Api.Demo.Controllers
//{
//    public class HomeTasksController : Controller
//    {
//        private readonly Repository _repository;
//        private readonly IConfiguration _configuration;

//        public HomeTasksController(Repository repository, IConfiguration configuration)
//        {
//            _repository = repository;
//            _configuration = configuration;
//        }


//        //MVC.View

//        //[HttpGet]
//        //public IActionResult AllHomeTasksByCourse(int courseId)
//        //{
//        //    var allHomeTasksByCourse = _repository.GetHomeTaskById(courseId);
//        //    ViewData["courseId"] = courseId;
//        //    return View("HomeTasks", allHomeTasksByCourse);
//        //}


//        //Create HomeTask
//        [HttpGet]
//        public IActionResult CreateHomeTask(int courseId)
//        {
//            var homeTask = new HomeTask();
//            ViewData["action"] = "CreateHomeTask";
//            ViewData["courseId"] = courseId;
//            return View("HomeTask", homeTask);
//        }

//        [HttpPost]
//        public IActionResult CreateHomeTask(HomeTask homeTask, int courseId)
//        {
//            //if (!ModelState.IsValid)
//            //{
//            //    ViewData["action"] = "CreateHomeTask";
//            //    ViewData["courseId"] = courseId;
//            //    return View("SaveHomeTask", homeTask);
//            //}
//            var routeValueDictionary = new RouteValueDictionary();
//            routeValueDictionary.Add("id", courseId);

//            this._repository.CreateHomeTask(homeTask, courseId);
//            return RedirectToAction("HomeTask", "Courses", routeValueDictionary);
//        }

//    }

//}



