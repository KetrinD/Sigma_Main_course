namespace ASP.NET.Demo.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ASP.NET.Demo.Controllers;
    using ASP.NET.Demo.ViewModels;
    using DataAccess.ADO;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using Models.Models;
    using Moq;
    using NSubstitute;
    using Services;
    using Xunit;

    public class HomeTaskControllerTests
    {
        // Create
        [Fact]
        public void Create_ReturnsBadRequest_WhenHomeTaskParameterIsNull()
        {
            //Arrange
            HomeTask homeTask = null;
            HomeTaskService homeTaskService = Substitute.For<HomeTaskService>();
            HomeTaskController controller = new HomeTaskController(homeTaskService, null);
            int x = 5;

            //Act
            IActionResult actual = controller.Create(homeTask, x);

            //Assert
            actual.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public void Create_ReturnsViewResult_WhenStudentModelStateIsInvalid()
        {
            //Arrange
            HomeTask homeTask = new HomeTask { Title = "New HomeTask" };
            HomeTaskService homeTaskService = Substitute.For<HomeTaskService>();
            HomeTaskController controller = new HomeTaskController(homeTaskService, null);
            int x = 5;

            //Act
            controller.ModelState.AddModelError("test", "test");  //makes model invalid => if (!ModelState.IsValid)
            IActionResult actual = controller.Create(homeTask, x);

            //Assert
            Assert.IsAssignableFrom<ViewResult>(actual);
            actual.Should().BeAssignableTo<ViewResult>();
        }

        //Edit tests

        [Fact]
        public void Edit_ReturnNotFound_WhenHomeTaskDoesNotExist()
        {
            //Arrange
            HomeTaskService homeTaskService = Substitute.For<HomeTaskService>();
            HomeTaskController controller = new HomeTaskController(homeTaskService, null);

            //Act
            IActionResult actual = controller.Edit(0);

            //Assert
            var expected = new NotFoundResult();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Edit_ReturnViewResultWithModel_WhenHomeTaskExists()
        {
            //Arrange
            HomeTask homeTask = new HomeTask { Title = "Test Home Task" };
            HomeTaskService homeTaskService = Substitute.For<HomeTaskService>();
            homeTaskService.GetHomeTaskById(5).Returns(homeTask);
            HomeTaskController controller = new HomeTaskController(homeTaskService, null);

            //Act
            IActionResult actual = controller.Edit(5);

            //Assert
            actual.Should().BeAssignableTo<ViewResult>();
            ViewResult viewResult = (ViewResult)actual;
            viewResult.Model.Should().BeEquivalentTo(homeTask);
        }

        [Fact]
        public void Edit_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            //Arrange
            HomeTask homeTask = new HomeTask { Title = "Test Home Task" };
            HomeTaskService homeTaskService = Substitute.For<HomeTaskService>();
            HomeTaskController controller = new HomeTaskController(homeTaskService, null);

            //Act
            controller.ModelState.AddModelError("test", "test");  //makes model invalid => if (!ModelState.IsValid)
            IActionResult actual = controller.Edit(homeTask);

            //Assert
            Assert.IsAssignableFrom<ViewResult>(actual);
            actual.Should().BeAssignableTo<ViewResult>();
        }

        //Delete
        [Fact]
        public void Delete_RedirectsToEditCourse_WhenHomeTaskExists()
        {
            //Arrange
            Course course = new Course() { Name = "Test course", Id = 5 };
            CourseService courseService = Substitute.For<CourseService>();
            courseService.GetCourse(5).Returns(course);

            HomeTask homeTask = new HomeTask { Title = "Test Home Task", Id = 3 };
            HomeTaskService homeTaskService = Substitute.For<HomeTaskService>();
            HomeTaskController controller = new HomeTaskController(homeTaskService, null);

            //Act
            var actionResult = controller.Delete(homeTask.Id, course.Id);
            // Assert
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Edit", "Course");
        }
    }
}



//[Fact]
//public void Edit_RedirectsToEditCourse_WhenModelStateIsValid()
//{
//    //Arrange
//    Course course = new Course() { Name = "Test course" };
//    CourseService courseService = Substitute.For<CourseService>();
//    courseService.GetCourse(5).Returns(course);

//    HomeTask homeTask = new HomeTask { Title = "Test Home Task" };
//    HomeTaskService homeTaskService = Substitute.For<HomeTaskService>();
//    HomeTaskController controller = new HomeTaskController(homeTaskService, null);
//    homeTaskService.GetHomeTaskById(3).Returns(homeTask);

//    //Act
//    var actionResult = controller.Edit(homeTask);
//    homeTaskService.Received().UpdateHomeTask(homeTask);
//    var routeValueDictionary = new RouteValueDictionary();
//    routeValueDictionary.Received().Add("id", course.Id);

//    // Assert
//    actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Edit", "Course");
//}