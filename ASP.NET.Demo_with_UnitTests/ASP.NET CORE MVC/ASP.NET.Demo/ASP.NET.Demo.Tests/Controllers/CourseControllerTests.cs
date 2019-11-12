namespace ASP.NET.Demo.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ASP.NET.Demo.Controllers;
    using ASP.NET.Demo.ViewModels;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Models.Models;
    using NSubstitute;
    using Services;
    using Xunit;

    public class CourseControllerTests
    {
        [Fact]
        public async Task Courses_ReturnsViewResult_WithListOfCourses()
        {
            // Arrange
            // Arrange
            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.GetAllCourses().Returns(this.GetCoursesList());
            var controller = new CourseController(courseServiceMock, null, null);

            // Act
            var result = controller.Courses();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Course>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count);
        }


        // Create

        [Fact]
        public void Create_ReturnsBadRequest_WhenCourseParameterIsNull()
        {
            //Arrange
            Course course = null;
            CourseService courseService = Substitute.For<CourseService>();
            CourseController controller = new CourseController(courseService, null, null);

            //Act
            IActionResult actual = controller.Create(course);

            //Assert
            actual.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public void Create_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            //Arrange
            Course course = new Course { Name = "Test_2" };
            CourseService courseService = Substitute.For<CourseService>();
            CourseController controller = new CourseController(courseService, null, null);

            //Act
            controller.ModelState.AddModelError("test", "test");  //makes model invalid => if (!ModelState.IsValid)
            IActionResult actual = controller.Create(course);

            //Assert
            Assert.IsAssignableFrom<ViewResult>(actual);
            actual.Should().BeAssignableTo<ViewResult>();
        }

        [Fact]
        public void Create_RedirectsToCoursesAndCreatesCourses_WhenRequestIsValid()
        {
            //Arrange
            Course course = new Course { Name = "Test_2" };
            CourseService courseService = Substitute.For<CourseService>();
            CourseController controller = new CourseController(courseService, null, null);

            //Act
            var actionResult = controller.Create(course);
            courseService.Received().CreateCourse(course);

            // Assert
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Courses");

        }


        //Edit tests

        [Fact]
        public void Edit_ReturnNotFound_WhenCourseDoesNotExist()
        {
            //Arrange
            CourseService courseService = Substitute.For<CourseService>();
            CourseController controller = new CourseController(courseService, null, null);

            //Act
            IActionResult actual = controller.Edit(0);

            //Assert
            var expected = new NotFoundResult();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Edit_ReturnViewResultWithModel_WhenCourseExists()
        {
            //Arrange
            Course course = new Course { Name = "Test one" };
            CourseService courseService = Substitute.For<CourseService>();
            courseService.GetCourse(5).Returns(course);
            CourseController controller = new CourseController(courseService, null, null);

            //Act
            IActionResult actual = controller.Edit(5);

            //Assert
            actual.Should().BeAssignableTo<ViewResult>();
            ViewResult viewResult = (ViewResult)actual;
            viewResult.Model.Should().BeEquivalentTo(course);
        }

        [Fact]
        public void Edit_ReturnsBadRequest_WhenCourseParameterIsNull()
        {
            //Arrange
            Course course = null;
            CourseService courseService = Substitute.For<CourseService>();
            CourseController controller = new CourseController(courseService, null, null);

            //Act
            IActionResult actual = controller.Edit(course);

            //Assert
            actual.Should().BeAssignableTo<BadRequestResult>();
        }

        [Fact]
        public void Edit_RedirectsToCourses_WhenCourseParameterIsNotNull()
        {
            //Arrange
            Course course = new Course() { Name = "Test course" };
            CourseService courseService = Substitute.For<CourseService>();
            courseService.GetCourse(5).Returns(course);
            CourseController controller = new CourseController(courseService, null, null);

            //Act
            var actionResult = controller.Edit(course);
            courseService.Received().UpdateCourse(course);

            // Assert
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Courses");
        }

        //Delete
        [Fact]
        public void Delete_RedirectsToCourses_WhenCourseExists()
        {
            //Arrange
            Course course = new Course() { Name = "Test course" };
            CourseService courseService = Substitute.For<CourseService>();
            courseService.GetCourse(5).Returns(course);
            CourseController controller = new CourseController(courseService, null, null);

            //Act
            var actionResult = controller.Delete(course.Id);

            // Assert
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Courses");
        }

        // Assign Students
        [Fact]
        public async Task AssignStudents_ReturnsViewResult_WithViewModel()
        {
            // Arrange
            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.GetCourse(Arg.Any<int>()).Returns(new Course());
            var studentService = Substitute.For<StudentService>();
            studentService.GetAllStudents().Returns(new List<Student>());
            var controller = new CourseController(courseServiceMock, studentService, null);

            // Act
            var result = controller.AssignStudents(0);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CourseStudentAssignmentViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task AssignStudents_ReturnsViewResult_WithCoursesAndStudentsAreAssigned()
        {
            // Arrange
            const int courseId = 1;
            const int assignedStudentId = 11;
            const int nonAssignedStudentId = 22;
            const string courseName = "test";

            CourseStudentAssignmentViewModel expectedModel = new CourseStudentAssignmentViewModel()
            {
                Id = courseId,
                Name = courseName,
                Students = new List<StudentViewModel>()
                {
                 new StudentViewModel(){StudentId = assignedStudentId,StudentFullName = "Test1", IsAssigned = true},
                 new StudentViewModel(){StudentId = nonAssignedStudentId,StudentFullName = "Test2", IsAssigned = false}
                }

            };
            var courseServiceMock = Substitute.For<CourseService>();

            courseServiceMock.GetCourse(courseId).Returns(new Course()
            {
                Id = courseId,
                Name = courseName,
                Students = new List<StudentCourse>() { new StudentCourse() { StudentId = assignedStudentId } }
            });

            var studentService = Substitute.For<StudentService>();
            var students = new List<Student>()
            {
               new Student() { Id = assignedStudentId, Name = "Test1" },
               new Student() { Id = nonAssignedStudentId, Name = "Test2" }
            };
            studentService.GetAllStudents().Returns(students);
            var controller = new CourseController(courseServiceMock, studentService, null);

            // Act
            var result = controller.AssignStudents(courseId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actualModel = Assert.IsAssignableFrom<CourseStudentAssignmentViewModel>(viewResult.ViewData.Model);
            actualModel.Should().BeEquivalentTo(expectedModel);
        }


        [Fact]
        public async Task AssignStudents_ReturnNotFound_WhenNonExistingCourseId()
        {
            //Arrange
            CourseService courseService = Substitute.For<CourseService>();
            CourseController controller = new CourseController(courseService, null, null);

            //Act
            IActionResult actual = controller.AssignStudents(0);

            //Assert
            var expected = new NotFoundResult();
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task AssignStudents_ReturnsBadRequest_WhenAssignmentViewModelIsEmpty()
        {
            //Arrange
            CourseStudentAssignmentViewModel expectedModel = null;
            CourseService courseService = Substitute.For<CourseService>();
            StudentService studentService = Substitute.For<StudentService>();
            var controller = new CourseController(courseService, studentService, null);

            //Act
            IActionResult actual = controller.AssignStudents(expectedModel);

            //Assert
            actual.Should().BeAssignableTo<BadRequestResult>();
        }

        //  [Fact]
        public async Task AssignStudents_SetStudentsToCoursesAndRedirectsToCourses_WhenCoursesAndStudentsAreAssigned()
        {
            //Arrange
            const int courseId = 1;
            const int assignedStudentId = 11;
            const int nonAssignedStudentId = 22;
            const string courseName = "test";

            CourseStudentAssignmentViewModel expectedModel = new CourseStudentAssignmentViewModel()
            {
                Id = courseId,
                Name = courseName,
                Students = new List<StudentViewModel>()
                {
                 new StudentViewModel(){StudentId = assignedStudentId,StudentFullName = "Test1", IsAssigned = true},
                 new StudentViewModel(){StudentId = nonAssignedStudentId,StudentFullName = "Test2", IsAssigned = false}
                }

            };
            var courseServiceMock = Substitute.For<CourseService>();

            courseServiceMock.GetCourse(courseId).Returns(new Course()
            {
                Id = courseId,
                Name = courseName,
                Students = new List<StudentCourse>() { new StudentCourse() { StudentId = assignedStudentId } }
            });

            var studentService = Substitute.For<StudentService>();
            var students = new List<Student>()
            {
               new Student() { Id = assignedStudentId, Name = "Test1" },
               new Student() { Id = nonAssignedStudentId, Name = "Test2" }
            };
            studentService.GetAllStudents().Returns(students);
            var controller = new CourseController(courseServiceMock, studentService, null);

            // Act
            var actionResult = controller.AssignStudents(courseId);

            // Assert
            actionResult.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Courses");

        }


        // Assign Lecturers

        [Fact]
        public async Task AssignLecturers_ReturnsViewResult_WithViewModel()
        {
            // Arrange
            var courseServiceMock = Substitute.For<CourseService>();
            courseServiceMock.GetCourse(Arg.Any<int>()).Returns(new Course());
            LecturerService lecturerService = Substitute.For<LecturerService>();
            lecturerService.GetAllLecturers().Returns(new List<Lecturer>());
            var controller = new CourseController(courseServiceMock, null, lecturerService);

            // Act
            var result = controller.AssignLecturers(0);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CourseLecturerAssignmentViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task AssignLecturers_ReturnsViewResult_WithCoursesAndStudentsAreAssigned()
        {
            // Arrange
            const int courseId = 1;
            const int assignedLecturerId = 11;
            const int nonassignedLecturerId = 22;
            const string courseName = "test";

            CourseLecturerAssignmentViewModel expectedModel = new CourseLecturerAssignmentViewModel()
            {
                Id = courseId,
                Name = courseName,
                Lecturers = new List<LecturersViewModel>()
                {
                 new LecturersViewModel(){LecturerId = assignedLecturerId, Name = "Test1", IsAssigned = true},
                 new LecturersViewModel(){LecturerId = nonassignedLecturerId, Name = "Test2", IsAssigned = false}
                }

            };
            var courseServiceMock = Substitute.For<CourseService>();

            courseServiceMock.GetCourse(courseId).Returns(new Course()
            {
                Id = courseId,
                Name = courseName,
                Lecturers = new List<LecturerCourse>() { new LecturerCourse() { LecturerId = assignedLecturerId } }
            });

            var lecturerService = Substitute.For<LecturerService>();
            var lecturers = new List<Lecturer>()
            {
               new Lecturer() { Id = assignedLecturerId, Name = "Test1" },
               new Lecturer() { Id = nonassignedLecturerId, Name = "Test2" }
            };
            lecturerService.GetAllLecturers().Returns(lecturers);
            var controller = new CourseController(courseServiceMock, null, lecturerService);

            // Act
            var result = controller.AssignLecturers(courseId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var actualModel = Assert.IsAssignableFrom<CourseLecturerAssignmentViewModel>(viewResult.ViewData.Model);
            actualModel.Should().BeEquivalentTo(expectedModel);
        }

        [Fact]
        public async Task AssignLecturers_ReturnNotFound_WhenNonExistingCourseId()
        {
            //Arrange
            CourseService courseService = Substitute.For<CourseService>();
            CourseController controller = new CourseController(courseService, null, null);

            //Act
            IActionResult actual = controller.AssignLecturers(0);

            //Assert
            var expected = new NotFoundResult();
            actual.Should().BeEquivalentTo(expected);

        }

        [Fact]
        public async Task AssignLecturers_ReturnsBadRequest_WhenAssignmentViewModelIsEmpty()
        {
            //Arrange
            CourseLecturerAssignmentViewModel model = null;
            CourseService courseService = Substitute.For<CourseService>();
            LecturerService LecturerService = Substitute.For<LecturerService>();
            CourseController controller = new CourseController(courseService, null, LecturerService);

            //Act
            IActionResult actual = controller.AssignLecturers(model);

            //Assert
            actual.Should().BeAssignableTo<BadRequestResult>();
        }

        private List<Course> GetCoursesList()
        {
            return new List<Course>() { new Course(), new Course() };
        }
    }
}

