using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Models;

namespace Web.Api.Demo.Controllers
{
    [Route("Api")]
    public class StudentsController : ControllerBase
    {
        private readonly Repository _repository;
        private readonly IConfiguration _configuration;

        public StudentsController(Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        //CreateStudent
        [Route("Students")]
        [HttpPost]
        public ActionResult<Student> CreateStudent([FromBody] Student student)
        {
            var newStudent = _repository.CreateStudent(student);
            return newStudent;
        }

        //GetStudentById
        [Route("Students/{id}")]
        [HttpGet]
        public ActionResult<Student> GetStudentById([FromRoute] int id)
        {
            var student = _repository.GetStudentById(id);


            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        //GetAllStudents
        [Route("Students")]
        [HttpGet]
        public ActionResult<List<Student>> GetAllStudents()
        {
            var students = _repository.GetAllStudents();
            return students;
        }

        //UpdateStudent
        [Route("Students")]
        [HttpPut]
        public ActionResult<Student> UpdateStudentById([FromBody]Student student)
        {
            _repository.UpdateStudent(student);
            return student;
        }

        //DeleteStudent
        [Route("Students/{id}")]
        [HttpDelete]
        public string DeleteStudentById([FromRoute] int id)
        {
            _repository.DeleteStudent(id);
            return "ok";
        }
    }
}
