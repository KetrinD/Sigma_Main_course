﻿using System;
using System.Collections.Generic;
using System.Text;

namespace A_ASP.NET.Demo.Tests.Controllers
{
    public class CourseControllerTests
    {
        public void Edit_ReturnNotFound_WhenCourseDoesNotExist()
        {
            //Arrange

            //Act
            CourseController //courseController = new CourseController();

            //Assert
        
        }
    }
}


//private object actualModel;
//[Fact]
//public async Task Courses_ReturnsViewResult_WithListOfCourses()
//{
//     Arrange
//    var courseServiceMock = Substitute.For<CourseService>();
//    courseServiceMock.GetAllCourses().Returns(this.GetCoursesList());
//    var controller = new CourseController(courseServiceMock, null, null);

//     Act
//    var result = controller.Courses();

//     Assert
//    var viewResult = Assert.IsType<ViewResult>(result);
//    var model = Assert.IsAssignableFrom<List<Course>>(viewResult.ViewData.Model);
//    Assert.Equal(2, model.Count);
//}

//[Fact]
//public async Task AssignStudents_ReturnsViewResult_WithViewModel()
//{
//     Arrange
//    var courseServiceMock = Substitute.For<CourseService>();
//    courseServiceMock.GetCourse(Arg.Any<int>()).Returns(new Course());
//    var studentService = Substitute.For<StudentService>();
//    studentService.GetAllStudents().Returns(new List<Student>());
//    var controller = new CourseController(courseServiceMock, studentService, null);

//     Act
//    var result = controller.AssignStudents(0);

//     Assert
//    var viewResult = Assert.IsType<ViewResult>(result);
//    Assert.IsAssignableFrom<CourseStudentAssignmentViewModel>(viewResult.ViewData.Model);
//}

//[Fact]
//public async Task AssignStudents_ReturnsViewResult_WithCoursesAndStudentsAreAssigned()
//{
//     Arrange
//    const int courseId = 1;
//    const int assignedStudentId = 11;
//    const int nonAssignedStudentId = 22;
//    const string courseName = "test";

//    CourseStudentAssignmentViewModel expectedModel = new CourseStudentAssignmentViewModel()
//    {
//        Id = courseId,
//        Name = courseName,
//        Students = new List<StudentViewModel>()
//                                                                                     {
//                                                                                         new StudentViewModel(){StudentId = assignedStudentId,StudentFullName = "Test1", IsAssigned = true},
//                                                                                         new StudentViewModel(){StudentId = nonAssignedStudentId,StudentFullName = "Test2", IsAssigned = false}
//                                                                                     }
//    };
//    var courseServiceMock = Substitute.For<CourseService>();

//    courseServiceMock.GetCourse(courseId).Returns(new Course()
//    {
//        Id = courseId,
//        Name = courseName,
//        Students = new List<StudentCourse>() { new StudentCourse() { StudentId = assignedStudentId } }
//    });

//    var studentService = Substitute.For<StudentService>();
//    var students = new List<Student>()
//                               {
//                                   new Student() { Id = assignedStudentId, Name = "Test1" },
//                                   new Student() { Id = nonAssignedStudentId, Name = "Test2" }
//                               };
//    studentService.GetAllStudents().Returns(students);
//    var controller = new CourseController(courseServiceMock, studentService, null);

//     Act
//    var result = controller.AssignStudents(courseId);

//     Assert
//    var viewResult = Assert.IsType<ViewResult>(result);
//    var actualModel = Assert.IsAssignableFrom<CourseStudentAssignmentViewModel>(viewResult.ViewData.Model);
//    actualModel.Should().BeEquivalentTo(expectedModel);
//}

//[Fact]
//public async Task Create_ReturnsBadRequest_WhenCourseParameterIsNull()
//{

//}

//[Fact]
//public async Task Create_ReturnsViewResult_WhenModelStateIsInvalid()
//{

//}

//[Fact]
//public async Task Create_RedirectsToCoursesAndCreatesCourses_WhenRequestIsValid()
//{

//    var courseServiceMock = Substitute.For<CourseService>();
//    courseServiceMock.GetCourse(5).Returns(course);

//    assert
//    courseServiceMock.Received().GetCourse(5);
//}

//[Fact]
//public async Task AssignStudents_ReturnsBadRequest_WhenNonExistingCourseId()
//{

//}

//[Fact]
//public async Task AssignStudents_ReturnsBadRequest_WhenAssignmentViewModelIsEmpty()
//{

//}

//[Fact]
//public async Task AssignStudents_SetStudentsToCoursesAndRedirectsToCourses_WhenCoursesAndStudentsAreAssigned()
//{

//}

//private List<Course> GetCoursesList()
//{
//    return new List<Course>() { new Course(), new Course() };
//}
//    }