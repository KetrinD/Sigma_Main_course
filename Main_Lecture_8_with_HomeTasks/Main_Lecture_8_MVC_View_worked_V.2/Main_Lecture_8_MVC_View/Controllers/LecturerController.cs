using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Models;
namespace Web.Api.Demo.Controllers
{
    //worked version
    public class LecturerController : Controller
    {
        private readonly Repository _repository;
        private readonly IConfiguration _configuration;

        public LecturerController(Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Lecturers()
        {
            var allLecturers = _repository.GetAllLecturers();
            return View("Lecturers", allLecturers);
        }

        //Edit Lecturer
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var lecturer = _repository.GetLecturerById(id);
            ViewData["action"] = "Edit";
            return View("Edit", lecturer);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Lecturer lecturer)
        {
            _repository.UpdateLecturer(lecturer);
            return RedirectToAction("Lecturers");
        }

        //Create Lecturer
        [HttpGet]
        public IActionResult Create()
        {
            var lecturer = new Lecturer();
            ViewData["action"] = "Create";
            return View("Edit", lecturer);
        }

        [HttpPost]
        public IActionResult Create(Lecturer lecture)
        {
            _repository.CreateLecturer(lecture);
            return RedirectToAction("Lecturers");
        }

        //Delete Lecturer
        public IActionResult Delete(int id)
        {
            _repository.DeleteLecturer(id);
            return RedirectToAction("Lecturers");
        }
    }
}

