using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Main_Lecture_4_Xml_
{
    public static class ExportXml
    {
        public static XDocument ExportStudents(List<Student> ImportStudents)
        {
            XDocument documentExp = new XDocument();
            XElement root = new XElement("Students");
            documentExp.Add(root);

            foreach (var student in ImportStudents)
            {
                XElement studentElement = new XElement("Student",
                    new XAttribute("firstName", student.FirstName),
                    new XAttribute("lastName", student.LastName));

                root.Add(studentElement);

                studentElement.Add(new XElement("BirthDate", GetBirthDate(student.Birthday)));
                studentElement.Add(new XElement("PhoneNumber", student.Phone));
                studentElement.Add(new XElement("Email", student.Email));
                studentElement.Add(new XElement("GitHubLink", student.Email));

                XElement extraDataElement = new XElement("ExtraData");
                studentElement.Add(extraDataElement);

                foreach (var extraData in student.ExtraData)
                {
                    extraDataElement.Add(new XElement("ExtraDataElement", new XAttribute("name", extraData.Key), extraData.Value));
                }

                XElement allHomeTasksMarks = new XElement("AllHomeTasksMarks");
                studentElement.Add(allHomeTasksMarks);

                foreach (var homeTasks in student.AllHomeTasksMarks)
                {
                    allHomeTasksMarks.Add(new XElement("AllHomeTasksMarks", new XAttribute("name", homeTasks.Key), homeTasks.Value));
                }

                XElement coursesElement = new XElement("Courses");
                studentElement.Add(coursesElement);
                foreach (var course in student.Courses)
                {
                    coursesElement.Add(new XElement("Course", course));
                }
            }

            documentExp.Save("Input_students.xml");
            return documentExp;
        }

        private static string GetBirthDate(DateTime studentBirthday)
        {
            return studentBirthday.ToString("dd.MM.yyy", CultureInfo.InvariantCulture);
        }


        public static XDocument ExportCourse(List<Course> ImportCourses)
        {
            XDocument documentExp = new XDocument();
            XElement root = new XElement("Courses");
            documentExp.Add(root);

            foreach (var course in ImportCourses)
            {
                XElement courseElement = new XElement("Course",
                    new XAttribute("CourseTitle", course.CourseTitle));

                root.Add(courseElement);

                courseElement.Add(new XElement("StartDay", GetStartDayCourse(course.StartDay)));
                courseElement.Add(new XElement("EndDay", GetEndDayCourse(course.EndDay)));
                courseElement.Add(new XElement("PassingScore", course.PassingScore));

                XElement studentsElement = new XElement("CourseStudents");
                courseElement.Add(studentsElement);
                foreach (var student in course.CourseStudents)
                {
                    studentsElement.Add(new XElement("Student", student));
                }

                XElement courseHomeTasksElement = new XElement("CourseHomeTasksList");
                courseElement.Add(courseHomeTasksElement);
                foreach (var homeTask in course.CourseHomeTasksList)
                {
                    courseHomeTasksElement.Add(new XElement("HomeTask", homeTask));
                }

                XElement courseTeacherElement = new XElement("CourseTeachers");
                courseElement.Add(courseTeacherElement);
                foreach (var teacher in course.CourseTeachers)
                {
                    courseTeacherElement.Add(new XElement("Teacher", teacher));
                }
            }

            documentExp.Save("Input_courses.xml");
            return documentExp;
        }

        private static string GetStartDayCourse(DateTime startDayCourse)
        {
            return startDayCourse.ToString("dd.MM.yyy", CultureInfo.InvariantCulture);
        }

        private static string GetEndDayCourse(DateTime endDayCourse)
        {
            return endDayCourse.ToString("dd.MM.yyy", CultureInfo.InvariantCulture);
        }
    }
}
