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


        [HttpGet]
        public IActionResult Create([FromRoute] int courseId)
        {
            ViewData["Action"] = "Create";
            ViewData["CourseId"] = courseId;
            var model = new HomeTask();
            return View("HomeTask", model);
        }

        [HttpPost]
        public IActionResult Create([FromForm] HomeTask homeTask, [FromRoute] int courseId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Action"] = "Create";
                ViewData["CourseId"] = courseId;
                return View("HomeTask", homeTask);
            }
            var routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", courseId);

            this._repository.CreateHomeTask(homeTask, courseId);
            return RedirectToAction("HomeTask", "Course", routeValueDictionary);
        }

    }
}



