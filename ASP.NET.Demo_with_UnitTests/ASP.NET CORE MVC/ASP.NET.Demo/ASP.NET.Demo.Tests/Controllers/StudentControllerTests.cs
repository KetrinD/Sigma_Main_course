﻿namespace ASP.NET.Demo.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ASP.NET.Demo.Controllers;
    using ASP.NET.Demo.ViewModels;
    using DataAccess.ADO;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Models.Models;
    using Moq;
    using NSubstitute;
    using Services;
    using Xunit;

    public class StudentControllerTests
    {
        [Fact]
        public void Students_ReturnsViewResult_WithListOfStudents()
        {
            // Arrange
            StudentService studentService = Substitute.For<StudentService>();
            studentService.GetAllStudents().Returns(this.GetStudentsList());
            StudentController controller = new StudentController(studentService);

            // Act
            var result = controller.Students();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Student>>(viewResult.ViewData.Model);
            Assert.Equal(4, model.Count);
        }

        // Create
        [Fact]
        public void Create_ReturnsBadRequest_WhenStudentParameterIsNull()
        {
            //Arrange
            Student student = null;
            StudentService studentService = Substitute.For<StudentService>();
            StudentController controller = new StudentController(studentService);

            //Act
            IActionResult actual = controller.Create(student);

            //Assert
            actual.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public void Create_ReturnsViewResult_WhenStudentModelStateIsInvalid()
        {
            //Arrange
            Student student = new Student { Name = "New Student" };
            StudentService studentService = Substitute.For<StudentService>();
            StudentController controller = new StudentController(studentService);

            //Act
            controller.ModelState.AddModelError("test", "test");  //makes model invalid => if (!ModelState.IsValid)
            IActionResult actual = controller.Create(student);

            //Assert
            Assert.IsAssignableFrom<ViewResult>(actual);
            actual.Should().BeAssignableTo<ViewResult>();
        }

        [Fact]
        public void Create_RedirectsToStudentsAndCreatesStudent_WhenRequestIsValid()
        {
            //Arrange
            Student student = new Student { Name = "New Student" };
            StudentService studentService = Substitute.For<StudentService>();
            StudentController controller = new StudentController(studentService);

            //Act
            var actionResult = controller.Create(student);
            studentService.Received().CreateStudent(student);

            // Assert
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Students");
        }


        //Edit tests

        [Fact]
        public void Edit_ReturnNotFound_WhenStudentDoesNotExist()
        {
            //Arrange
            StudentService studentService = Substitute.For<StudentService>();
            StudentController controller = new StudentController(studentService);

            //Act
            IActionResult actual = controller.Edit(0);

            //Assert
            var expected = new NotFoundResult();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Edit_ReturnViewResultWithModel_WhenStudentExists()
        {
            //Arrange
            Student student = new Student { Name = "Test student" };
            StudentService studentService = Substitute.For<StudentService>();
            studentService.GetStudentById(5).Returns(student);
            StudentController controller = new StudentController(studentService);

            //Act
            IActionResult actual = controller.Edit(5);

            //Assert
            actual.Should().BeAssignableTo<ViewResult>();
            ViewResult viewResult = (ViewResult)actual;
            viewResult.Model.Should().BeEquivalentTo(student);
        }

        [Fact]
        public void Edit_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            //Arrange
            Student student = new Student { Name = "Test student" };
            StudentService studentService = Substitute.For<StudentService>();
            StudentController controller = new StudentController(studentService);

            //Act
            controller.ModelState.AddModelError("test", "test");  //makes model invalid => if (!ModelState.IsValid)
            IActionResult actual = controller.Edit(student);

            //Assert
            Assert.IsAssignableFrom<ViewResult>(actual);
            actual.Should().BeAssignableTo<ViewResult>();
        }

        [Fact]
        public void Edit_RedirectsToStudents_WhenModelStateIsValid()
        {
            //Arrange
            Student student = new Student { Name = "Test student" };
            StudentService studentService = Substitute.For<StudentService>();
            studentService.GetStudentById(5).Returns(student);
            StudentController controller = new StudentController(studentService);

            //Act
            var actionResult = controller.Edit(student);
            studentService.Received().UpdateStudent(student);

            // Assert
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Students");
        }

        //Delete
        [Fact]
        public void Delete_RedirectsToStudents_WhenStudentExists()
        {
            //Arrange
            Student student = new Student() { Name = "Test Student" };
            StudentService studentService = Substitute.For<StudentService>();
            studentService.GetStudentById(5).Returns(student);
            StudentController controller = new StudentController(studentService);

            //Act
            var actionResult = controller.Delete(student.Id);
            // Assert
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Students");
        }

        private List<Student> GetStudentsList()
        {
            return new List<Student>() { new Student(), new Student(), new Student(), new Student() };
        }
    }
}

