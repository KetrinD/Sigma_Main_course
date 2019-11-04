using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Models;

namespace Web.Api.Demo.Controllers
{
    public class CoursesController : ControllerBase
    {
        private readonly Repository _repository;
        private readonly IConfiguration _configuration;

        public CoursesController(Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        //CreateCourse
        [HttpPost]
        public ActionResult<Course> CreateCourse([FromBody] Course course)
        {
            var newCourse = _repository.CreateCourse(course);
            return newCourse;
        }

        //GetCourseById
        [HttpGet]
        public ActionResult<Course> GetCourseById([FromRoute] int id)
        {
            var course = _repository.GetCourse(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        //GetAllCourses
        [HttpGet]
        public ActionResult<List<Course>> GetAllCourses()
        {
            var courses = _repository.GetAllCourses();
            return courses;
        }

        //UpdateCourseById
        [HttpPut]
        public ActionResult<Course> UpdateCourseById([FromBody]Course course)
        {
            _repository.UpdateCourse(course);
            return course;
        }

        //DeleteCourseById
        [HttpDelete]
        public string DeleteCourseById([FromRoute] int id)
        {
            _repository.DeleteCourse(id);
            return "ok";
        }

        //SetStudentsToCourse
        [HttpPost]
        [Route("Courses/SetStudentsToCourse/{courseId}")]
        public Course SetStudentsToCourse([FromRoute] int courseId, [FromBody] IEnumerable<int> studentsId)
        {
            _repository.SetStudentsToCourse(courseId, studentsId);
            var course = _repository.GetCourse(courseId);
            return course;
        }


        //GetStudentsByCourseId
        [HttpGet]
        [Route("Courses/GetStudentsByCourseId/{courseId}")]
        public ActionResult<List<Student>> GetStudentsByCourseId(int courseId)
        {
            List<Student> students = _repository.GetStudentsByCourseId(courseId);
            return students;
        }

        //DeleteStudentsFromCourse
        [HttpDelete]
        [Route("Courses/DeleteStudentFromCourse/{courseId}")]
        public Course DeleteStudentFromCourse([FromRoute] int courseId, [FromBody] int studentId)
        {
            _repository.DeleteStudentFromCourse(courseId, studentId);
            var course = _repository.GetCourse(courseId);
            return course;
        }

        [HttpDelete]
        [Route("Courses/DeleteSeveralStudentsFromCourse/{courseId}")]
        public Course DeleteSeveralStudentsFromCourse([FromRoute] int courseId, [FromBody] IEnumerable<int> studentsId)
        {
            _repository.DeleteSeveralStudentsFromCourse(courseId, studentsId);
            var course = _repository.GetCourse(courseId);
            return course;
        }
    }
}





