using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main_Lecture_8_MVC_View.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Models;

namespace Web.Api.Demo.Controllers
{
    public class LecturersController : Controller
    {
        private readonly Repository _repository;
        private readonly IConfiguration _configuration;

        public LecturersController(Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        //MVC.View

        [HttpGet]
        public IActionResult AllLecturers()
        {
            var allLecturers = _repository.GetAllLecturers();
            return View("Lecturers", allLecturers);
        }

        //Edit Lecturer
        [HttpGet]
        public IActionResult EditLecturer(int id)
        {
            var lecturer = _repository.GetLecturerById(id);
            ViewData["action"] = "EditLecturer";
            return View("SaveLecturer", lecturer);
        }

        [HttpPost]
        public IActionResult EditLecturer([FromForm] Lecturer lecturer)
        {
            _repository.UpdateLecturer(lecturer);
            return RedirectToAction("AllLecturers");
        }


        //Create Lecturer
        [HttpGet]
        public IActionResult CreateLecturer()
        {
            var lecturer = new Lecturer();
            ViewData["action"] = "CreateLecturer";
            return View("SaveLecturer", lecturer);
        }

        [HttpPost]
        public IActionResult CreateLecturer([FromForm] Lecturer lecture)
        {
            _repository.CreateLecturer(lecture);
            return RedirectToAction("AllLecturers");
        }


        //Delete Lecturer
        public IActionResult DeleteLecturer(int id)
        {
            _repository.DeleteLecturer(id);
            return RedirectToAction("AllLecturers");
        }

    }
}
