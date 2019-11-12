namespace ASP.NET.Demo.Tests
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

    public class LecturerControllerTests
    {
        private object actualModel;

        [Fact]
        public async Task Lecturers_ReturnsViewResult_WithListOfLecturers()
        {
            // Arrange
            LecturerService lecturersService = Substitute.For<LecturerService>();
            lecturersService.GetAllLecturers().Returns(this.GetLecturersList());
            var controller = new LecturerController(lecturersService);

            // Act
            var result = controller.Lecturers();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Lecturer>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count);
        }

        // Create
        [Fact]
        public void Create_ReturnsBadRequest_WhenLecturerParameterIsNull()
        {
            //Arrange
            Lecturer lecturer = null;
            LecturerService lecturerService = Substitute.For<LecturerService>();
            LecturerController controller = new LecturerController(lecturerService);

            //Act
            IActionResult actual = controller.Create(lecturer);

            //Assert
            actual.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public void Create_ReturnsViewResult_WhenLecturerModelStateIsInvalid()
        {
            //Arrange
            Lecturer lecturer = new Lecturer { Name = "New Lecturer" };
            LecturerService lecturerService = Substitute.For<LecturerService>();
            LecturerController controller = new LecturerController(lecturerService);

            //Act
            controller.ModelState.AddModelError("test", "test");  //makes model invalid => if (!ModelState.IsValid)
            IActionResult actual = controller.Create(lecturer);

            //Assert
            Assert.IsAssignableFrom<ViewResult>(actual);
            actual.Should().BeAssignableTo<ViewResult>();
        }

        [Fact]
        public void Create_RedirectsToLecturersAndCreatesLecturer_WhenRequestIsValid()
        {
            //Arrange
            Lecturer lecturer = new Lecturer { Name = "New Lecturer" };
            LecturerService lecturerService = Substitute.For<LecturerService>();
            LecturerController controller = new LecturerController(lecturerService);

            //Act
            var actionResult = controller.Create(lecturer);
            lecturerService.Received().CreateLecturer(lecturer);

            // Assert
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Lecturers");
        }

        //Edit tests

        [Fact]
        public void Edit_ReturnNotFound_WhenLecturerDoesNotExist()
        {
            //Arrange
            LecturerService lecturerService = Substitute.For<LecturerService>();
            LecturerController controller = new LecturerController(lecturerService);

            //Act
            IActionResult actual = controller.Edit(0);

            //Assert
            var expected = new NotFoundResult();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Edit_ReturnViewResultWithModel_WhenLecturerExists()
        {
            //Arrange
            Lecturer lecturer = new Lecturer { Name = "Test Lecturer" };
            LecturerService lecturerService = Substitute.For<LecturerService>();
            lecturerService.GetLecturerById(5).Returns(lecturer);
            LecturerController controller = new LecturerController(lecturerService);

            //Act
            IActionResult actual = controller.Edit(5);

            //Assert
            actual.Should().BeAssignableTo<ViewResult>();
            ViewResult viewResult = (ViewResult)actual;
            viewResult.Model.Should().BeEquivalentTo(lecturer);
        }

        [Fact]
        public void Edit_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            //Arrange
            Lecturer lecturer = new Lecturer { Name = "Test Lecturer" };
            LecturerService lecturerService = Substitute.For<LecturerService>();
            LecturerController controller = new LecturerController(lecturerService);

            //Act
            controller.ModelState.AddModelError("test", "test");  //makes model invalid => if (!ModelState.IsValid)
            IActionResult actual = controller.Edit(lecturer);

            //Assert
            Assert.IsAssignableFrom<ViewResult>(actual);
            actual.Should().BeAssignableTo<ViewResult>();
        }

        [Fact]
        public void Edit_RedirectsToLecturers_WhenModelStateIsValid()
        {
            //Arrange
            Lecturer lecturer = new Lecturer { Name = "Test Lecturer" };
            LecturerService lecturerService = Substitute.For<LecturerService>();
            lecturerService.GetLecturerById(5).Returns(lecturer);
            LecturerController controller = new LecturerController(lecturerService);

            //Act
            var actionResult = controller.Edit(lecturer);
            lecturerService.Received().UpdateLecturer(lecturer);

            // Assert
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Lecturers");
        }

        //Delete
        [Fact]
        public void Delete_RedirectsToLecturers_WhenLecturerExists()
        {
            //Arrange
            Lecturer lecturer = new Lecturer() { Name = "Test Lecturer" };
            LecturerService lecturersService = Substitute.For<LecturerService>();
            lecturersService.GetLecturerById(5).Returns(lecturer);
            LecturerController controller = new LecturerController(lecturersService);

            //Act
            var actionResult = controller.Delete(lecturer.Id);
            // Assert
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Lecturers");
        }

        private List<Lecturer> GetLecturersList()
        {
            return new List<Lecturer>() { new Lecturer(), new Lecturer() };
        }
    }
}

