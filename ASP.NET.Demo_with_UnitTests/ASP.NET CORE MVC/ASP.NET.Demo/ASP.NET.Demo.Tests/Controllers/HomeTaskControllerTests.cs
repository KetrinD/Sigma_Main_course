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

    public class HomeTaskControllerTests
    {
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
    }
}

