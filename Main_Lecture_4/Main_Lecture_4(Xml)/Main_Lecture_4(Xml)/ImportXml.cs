using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Lecture_4_Xml_
{
    public static class ImportXml
    {
        public static void ImportStudents(List<Student> ImportStudents)
        {
            //ImportStudents?.Clear();

            XDocument documentImpStudent = new XDocument();
            documentImpStudent = XDocument.Load("Input_students.xml");
            var studentsElement = documentImpStudent.Elements().First();           // root

            foreach (var student in studentsElement.Elements())          //child
            {
                Student importStudent = new Student();
                importStudent.FirstName = student.Attribute("firstName").Value;
                importStudent.LastName = student.Attribute("lastName").Value;
                importStudent.Birthday = ImportStudentBirthday(student);
                importStudent.Phone = student.Element("PhoneNumber").Value;
                importStudent.Email = student.Element("Email").Value;
                importStudent.GitHubLink = student.Element("GitHubLink").Value;

                ImportExtraData(importStudent, student.Elements().SelectMany(p => p.Elements("ExtraDataElement")));
                ImportCourses(importStudent, student.Elements().SelectMany(p => p.Elements("Course")));
                ImportStudents.Add(importStudent);
            }

        }
        private static DateTime ImportStudentBirthday(XElement student)
        {
            DateTime birthDate = DateTime.ParseExact(student.Element("BirthDate").Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            return birthDate;
        }

        private static void ImportExtraData(Student importStudent, IEnumerable<XElement> extraDataElements)
        {
            foreach (XElement extraDataElement in extraDataElements)
            {
                importStudent.ExtraData[extraDataElement.Attribute("name").Value] = extraDataElement.Value;
            }
        }

        private static void ImportCourses(Student importedStudent, IEnumerable<XElement> courseElements)
        {
            foreach (XElement courseElement in courseElements)
            {
                importedStudent.Courses.Add(new Course(courseElement.Value));
            }
        }

        public static void ImportCourses(List<Course> ImportCourses)
        {

            XDocument documentImpCourses = new XDocument();
            documentImpCourses = XDocument.Load("Input_courses.xml");
            var courseElement = documentImpCourses.Elements().First();           // root

            foreach (var course in courseElement.Elements())          //child
            {
                Course importCourse = new Course();
                importCourse.CourseTitle = course.Attribute("CourseTitle").Value;
                importCourse.StartDay = ImportCourseStartDay(course);
                importCourse.EndDay = ImportCourseEndDay(course);
                importCourse.PassingScore = Convert.ToInt32(course.Element("PassingScore").Value);

                ImportCourseStudents(importCourse, course.Elements().SelectMany(p => p.Elements("Student")));
                ImportCourseHomeTasksList(importCourse, course.Elements().SelectMany(p => p.Elements("HomeTask")));
                ImportCourseTeachers(importCourse, course.Elements().SelectMany(p => p.Elements("Teacher")));

                ImportCourses.Add(importCourse);
            }
        }

        private static DateTime ImportCourseStartDay(XElement course)
        {
            DateTime courseStartDay = DateTime.ParseExact(course.Element("StartDay").Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            return courseStartDay;
        }

        private static DateTime ImportCourseEndDay(XElement course)
        {
            DateTime courseEndDay = DateTime.ParseExact(course.Element("EndDay").Value, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            return courseEndDay;
        }

        private static void ImportCourseStudents(Course importedCourse, IEnumerable<XElement> studentElements)
        {
            foreach (XElement studentElement in studentElements)
            {
                importedCourse.CourseStudents.Add(new Student(studentElement.Value));
            }
        }

        private static void ImportCourseHomeTasksList(Course importedCourse, IEnumerable<XElement> courseElements)
        {
            foreach (XElement courseElement in courseElements)
            {
                importedCourse.CourseHomeTasksList.Add(new HomeTaks(courseElement.Value));
            }
        }

        private static void ImportCourseTeachers(Course importedCourse, IEnumerable<XElement> courseElements)
        {
            foreach (XElement courseElement in courseElements)
            {
                importedCourse.CourseTeachers.Add(new Teacher(courseElement.Value));
            }
        }
    }
}
