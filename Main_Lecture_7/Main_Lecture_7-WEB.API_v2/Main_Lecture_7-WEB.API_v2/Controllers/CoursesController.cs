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
        [Route("Courses")]
        [HttpPost]
        public ActionResult<Course> CreateCourse([FromBody] Course course)
        {
            var newCourse = _repository.CreateCourse(course);
            return newCourse;
        }

        //GetCourseById
        [Route("Courses/{id}")]
        [HttpGet]
        public ActionResult<Course> GetCourseById ([FromRoute] int id)
        {
            var course = _repository.GetCourse(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        //GetAllCourses
        [Route("Courses")]
        [HttpGet]
        public ActionResult<List<Course>> GetAllCourses()
        {
            var courses = _repository.GetAllCourses();
            return courses;
        }

        //UpdateCourseById
        [Route("Courses")]
        [HttpPut]
        public ActionResult<Course> UpdateCourseById([FromBody]Course course)
        {
            _repository.UpdateCourse(course);
            return course;
        }

        //DeleteCourseById
        [Route("Courses/{id}")]
        [HttpDelete]
        public string DeleteCourseById([FromRoute] int id)
        {
            _repository.DeleteCourse(id);
            return "ok";
        }

        //SetStudentsToCourse
        [HttpPost]
        [Route("Courses/assign-students/{courseId}")]
        public Course SetStudentsToCourse([FromRoute] int courseId, [FromBody] IEnumerable<int> studentIds)
        {
            _repository.SetStudentsToCourse(courseId, studentIds);
            var course = _repository.GetCourse(courseId);
            return course;
        }

        //GetStudentsByCourseId
        [Route("Courses/assign-students/{courseId}")]
        [HttpGet]  
        public ActionResult<List<Student>> GetStudentsByCourseId(int courseId)
        {
            List<Student> students = _repository.GetStudentsByCourseId(courseId);
            return students;
        }

        //DeleteStudentsFromCourse
        [Route("Courses/assign-students/{courseId}")]
        [HttpDelete]
        public Course DeleteSeveralStudentsFromCourse([FromRoute] int courseId, [FromBody] IEnumerable<int> studentIds)
        {
            _repository.DeleteSeveralStudentsFromCourse(courseId, studentIds);
            var course = _repository.GetCourse(courseId);
            return course;
        }
    }
}





