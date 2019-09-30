using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Globalization;

namespace Main_Lecture_4_Xml_
{
    class Program
    {

        static void Main(string[] args)
        {
            //Marks
            Marks mark_A = new Marks("A", "01.10.2019");
            Marks mark_B = new Marks("B", "15.10.2019");
            Marks mark_C = new Marks("C", "01.11.2019");
            Marks mark_D = new Marks("D", "15.11.2019");
            Marks mark_F = new Marks("F", "01.12.2019");

            //Course
            Course course_1 = new Course("Sigma: c# basic course", "01.03.2020", "15.05.2020", 65);
            Course course_2 = new Course("Sigma:c# main course", "16.09.2019", "30.11.2019", 75);
            Course course_3 = new Course("Intellias: c# basic course", "01.03.2019", "30.05.2019", 50);
            Course course_4 = new Course("Intellias: c# main course", "14.06.2019", "30.08.2019", 70);

            //Teacher
            Teacher teacher_1 = new Teacher("Oleksii", "Kachmar", "31.07.1983");
            Teacher teacher_2 = new Teacher("Oleh", "Zarevuch", "15.05.1992");

            //HomeTasks
            HomeTaks homeTask_1 = new HomeTaks("Watch 1-st season of Games of Thrones", "Discuss Daenerys character with friends", "15.10.2019");
            HomeTaks homeTask_2 = new HomeTaks("Watch 2-st season of Games of Thrones", "Discuss Tyrion Lannister character with friends", "30.10.2019");
            HomeTaks homeTask_3 = new HomeTaks("Watch 3-st season of Games of Thrones", "Discuss Jaime Lannister character with friends", "15.10.2019");
            HomeTaks homeTask_4 = new HomeTaks("Watch 4-st season of Games of Thrones", "Discuss Arya Stark character with friends", "30.10.2019");

            // corurses added to Teacher 
            // Teacher added to courses
            AddingHelper.AssignTeacherToCourse(teacher_1, course_1);
            AddingHelper.AssignTeacherToCourse(teacher_1, course_2);
            AddingHelper.AssignTeacherToCourse(teacher_1, course_3);
            AddingHelper.AssignTeacherToCourse(teacher_2, course_1);
            AddingHelper.AssignTeacherToCourse(teacher_2, course_3);
            AddingHelper.AssignTeacherToCourse(teacher_2, course_4);

            // Teacher has next courses
            DisplayingData.TeacherHasNextCourses(teacher_1);
            DisplayingData.TeacherHasNextCourses(teacher_2);

            // Course has next Teachers:
            DisplayingData.CourseHasNextTeachers(course_1);
            DisplayingData.CourseHasNextTeachers(course_2);
            DisplayingData.CourseHasNextTeachers(course_3);
            DisplayingData.CourseHasNextTeachers(course_4);

            //Course has next home tasks:      
            course_2.CourseHomeTasksList.Add(homeTask_1);
            course_2.CourseHomeTasksList.Add(homeTask_2);
            course_3.CourseHomeTasksList.Add(homeTask_3);
            course_3.CourseHomeTasksList.Add(homeTask_4);

            DisplayingData.CourseHasNextHomeTasks(course_1);
            DisplayingData.CourseHasNextHomeTasks(course_2);
            DisplayingData.CourseHasNextHomeTasks(course_3);
            DisplayingData.CourseHasNextHomeTasks(course_4);

            //STUDENT
            List<Student> ImportStudents = new List<Student>();

            //Add new student
            Student newStudent_1 = new Student()
            {
                FirstName = "Ketrin",
                LastName = "Shynkarenko",
                Birthday = new DateTime(1987, 12, 05),
                Email = "katja.shynkarenko@gmail.com",
                Phone = "0936233319",
                GitHubLink = "katya-shynkarenko"
            };
            ImportStudents.Add(newStudent_1);
            newStudent_1.ExtraData.Add("SkypeId", "kdubovets");
            newStudent_1.ExtraData.Add("Marriage", "No,Thanks God");

            // corurses added to student 
            // student added to courses
            AddingHelper.AssignStudentToCourse(newStudent_1, course_2);
            AddingHelper.AssignStudentToCourse(newStudent_1, course_3);

            // all hometasks and its mark added to student
            // student added to home tasks
            AddingHelper.AssignStudentToHometask(newStudent_1, homeTask_1, mark_B);
            AddingHelper.AssignStudentToHometask(newStudent_1, homeTask_2, mark_A);
            AddingHelper.AssignStudentToHometask(newStudent_1, homeTask_3, mark_B);
            AddingHelper.AssignStudentToHometask(newStudent_1, homeTask_4, mark_B);

            //Course Has Next Students
            DisplayingData.CourseHasNextStudents(course_1);
            DisplayingData.CourseHasNextStudents(course_2);
            DisplayingData.CourseHasNextStudents(course_3);
            DisplayingData.CourseHasNextStudents(course_4);

            //Student has next courses
            DisplayingData.StudentHasNextCourses(newStudent_1);

            //Student has next Home Tasks
            DisplayingData.StudentHasNextHomeTasks(newStudent_1);

            //Home task has next Students and their marks 
            DisplayingData.HomeTaskHasNextStudents(homeTask_1);
            DisplayingData.HomeTaskHasNextStudents(homeTask_2);
            DisplayingData.HomeTaskHasNextStudents(homeTask_3);
            DisplayingData.HomeTaskHasNextStudents(homeTask_4);
            

            //*********************************************

            //// Import
            //Import(ImportStudents);
            //Console.WriteLine("\nV.1");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"List after Import Students. Count - {ImportStudents.Count}");
            //Console.ResetColor();
            //foreach (var st in ImportStudents)
            //{
            //    Console.WriteLine(st.ToString());
            //}

            //// V.2  I just want to try this method but I do not know how to add ExtraDataElement and Course
            //XDocument document2 = XDocument.Load("Input_students.xml");
            //var studentsElement2 = from xe in document2.Element("Students").Elements("Student")   //child
            //                       select new Student
            //                       {
            //                           FirstName = xe.Attribute("firstName").Value,
            //                           LastName = xe.Attribute("lastName").Value,
            //                           Birthday = DateTime.ParseExact(xe.Element("BirthDate").Value, "dd.MM.yyyy", CultureInfo.InvariantCulture),
            //                           Email = xe.Element("Email").Value,
            //                           Phone = xe.Element("PhoneNumber").Value,
            //                           GitHubLink = xe.Element("GitHubLink").Value

            //                       };
            //Console.WriteLine("\nV.2");

            //foreach (var item in studentsElement2)
            //{
            //    Console.WriteLine($"{item.FirstName} - {item.LastName} - {item.GitHubLink}");
            //}

            ////Export
            //var result = Export(ImportStudents);
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"\nFile after Export Students. Count - {ImportStudents.Count}");
            //Console.ResetColor();
            //Console.WriteLine($"\n{result}");

            ////Add new student
            //Student newStudent = new Student()
            //{
            //    FirstName = "Ketrin",
            //    LastName = "Shynkarenko",
            //    Birthday = new DateTime(1987, 12, 05),
            //    Email = "katja.shynkarenko@gmail.com",
            //    Phone = "0936233319",
            //    GitHubLink = "katya-shynkarenko"
            //};
            //ImportStudents.Add(newStudent);
            //newStudent.ExtraData.Add("SkypeId", "kdubovets");
            //newStudent.ExtraData.Add("Marriage", "No,Thanks God");
            //newStudent.Courses.Add(course_3);
            //newStudent.Courses.Add(course_2);

            //Console.ForegroundColor = ConsoleColor.Magenta;
            //Console.WriteLine($"\nStudent {newStudent.FirstName} {newStudent.LastName} has next courses:");
            //foreach (var course in newStudent.Courses)
            //{
            //    Console.WriteLine(course);
            //}
            //Console.ResetColor();

            //course_3.CourseStudents.Add(newStudent);
            //course_4.CourseStudents.Add(newStudent);

            //Console.WriteLine($"\nStudents on Course: {course_3}");
            //foreach (var c in course_3.CourseStudents)
            //{
            //    Console.WriteLine(c.ToString());
            //};

            //Console.WriteLine($"\nStudents on Course: {course_4}");
            //foreach (var c in course_4.CourseStudents)
            //{
            //    Console.WriteLine(c.ToString());
            //};


            ////List after added new Student. Count

            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine($"\nList after added new Student. Count - {ImportStudents.Count}");
            //Console.ResetColor();
            //foreach (var st in ImportStudents)
            //{
            //    Console.WriteLine(st.ToString());
            //}

            ////File after added new Student. Count

            //var result2 = Export(ImportStudents);
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"\nFile after added new Student. Count - {ImportStudents.Count}");
            //Console.ResetColor();
            //Console.WriteLine($"{result2}");

            ////List after second Import Students. Count

            //Import(ImportStudents);
            //Console.ForegroundColor = ConsoleColor.Blue;
            //Console.WriteLine($"\nList after second Import Students. Count - {ImportStudents.Count}");
            //Console.ResetColor();
            //foreach (var st in ImportStudents)
            //{
            //    Console.WriteLine(st.ToString());
            //}

            Console.ReadKey();
        }

        private static void Import(List<Student> ImportStudents)
        {
            ImportStudents?.Clear();

            XDocument documentImp = new XDocument();
            documentImp = XDocument.Load("Input_students.xml");
            var studentsElement = documentImp.Elements().First();           // root

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

        public static XDocument Export(List<Student> ImportStudents)
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
    }
}



