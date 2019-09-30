using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            List<Course> ImportCourses = new List<Course>();

            Course course_1 = new Course("Sigma: c# basic course", "01.03.2020", "15.05.2020", 65);
            Course course_2 = new Course("Sigma:c# main course", "16.09.2019", "30.11.2019", 75);
            Course course_3 = new Course("Intellias: c# basic course", "01.03.2019", "30.05.2019", 50);
            Course course_4 = new Course("Intellias: c# main course", "14.06.2019", "30.08.2019", 70);

            ImportCourses.Add(course_1);
            ImportCourses.Add(course_2);
            ImportCourses.Add(course_3);
            ImportCourses.Add(course_4);

            //Teacher
            Teacher teacher_1 = new Teacher("Oleksii", "Kachmar", "31.07.1983");
            Teacher teacher_2 = new Teacher("Oleh", "Zarevuch", "15.05.1992");

            //HomeTasks
            HomeTaks homeTask_1 = new HomeTaks("Watch 1-st season of Games of Thrones", "Discuss Daenerys character with friends", "15.10.2019");
            HomeTaks homeTask_2 = new HomeTaks("Watch 2-st season of Games of Thrones", "Discuss Tyrion Lannister character with friends", "30.10.2019");
            HomeTaks homeTask_3 = new HomeTaks("Watch 3-st season of Games of Thrones", "Discuss Jaime Lannister character with friends", "15.10.2019");
            HomeTaks homeTask_4 = new HomeTaks("Watch 4-st season of Games of Thrones", "Discuss Arya Stark character with friends", "30.10.2019");
            HomeTaks homeTask_5 = new HomeTaks("Watch 5-st season of Games of Thrones", "Discuss Sansa Stark character with friends", "15.10.2019");
            HomeTaks homeTask_6 = new HomeTaks("Watch 6-st season of Games of Thrones", "Discuss Cersei Lannister character with friends", "30.10.2019");

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

            //Home Tasks added to Course:      
            course_2.CourseHomeTasksList.Add(homeTask_1);
            course_2.CourseHomeTasksList.Add(homeTask_2);
            course_3.CourseHomeTasksList.Add(homeTask_3);
            course_3.CourseHomeTasksList.Add(homeTask_4);
            course_1.CourseHomeTasksList.Add(homeTask_5);
            course_4.CourseHomeTasksList.Add(homeTask_6);

            //Course has next home tasks: 
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

            newStudent_1.ExtraData.Add("SkypeId", "kdubovets");
            newStudent_1.ExtraData.Add("Marriage", "No,Thanks God");

            ImportStudents.Add(newStudent_1);

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
          
            // Import
            ImportXml.Import(ImportStudents);
            Console.WriteLine("\nV.1");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"List after Import Students. Count - {ImportStudents.Count}");
            Console.ResetColor();
            foreach (var st in ImportStudents)
            {
                Console.WriteLine(st.ToString());
            }

            // V.2  I just want to try this method but I do not know how to add ExtraDataElement and Course
            XDocument document2 = XDocument.Load("Input_students.xml");
            var studentsElement2 = from xe in document2.Element("Students").Elements("Student")   //child
                                   select new Student
                                   {
                                       FirstName = xe.Attribute("firstName").Value,
                                       LastName = xe.Attribute("lastName").Value,
                                       Birthday = DateTime.ParseExact(xe.Element("BirthDate").Value, "dd.MM.yyyy", CultureInfo.InvariantCulture),
                                       Email = xe.Element("Email").Value,
                                       Phone = xe.Element("PhoneNumber").Value,
                                       GitHubLink = xe.Element("GitHubLink").Value

                                   };
            Console.WriteLine("\nV.2");

            foreach (var item in studentsElement2)
            {
                Console.WriteLine($"{item.FirstName} - {item.LastName} - {item.GitHubLink}");
            }

            //Export Students
            var result2 = ExportXml.ExportStudents(ImportStudents);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nFile after Export Students. Count - {ImportStudents.Count}");
            Console.ResetColor();
            Console.WriteLine($"\n{result2}");

            //Add new student
            Student newStudent = new Student()
            {
                FirstName = "Daenerys",
                LastName = "Targaryen",
                Birthday = new DateTime(1985, 05, 01),
                Email = "Daenerys.Targaryen@gmail.com",
                Phone = "0938597451",
                GitHubLink = "Daenerys-Targaryen"
            };

            newStudent.ExtraData.Add("SkypeId", "DaenerysT");
            newStudent.ExtraData.Add("Marriage", "No,Thanks God");

            ImportStudents.Add(newStudent);

            // corurses added to student 
            // student added to courses
            AddingHelper.AssignStudentToCourse(newStudent, course_1);
            AddingHelper.AssignStudentToCourse(newStudent, course_4);

            // all hometasks and its mark added to student
            // student added to home tasks
            AddingHelper.AssignStudentToHometask(newStudent, homeTask_5, mark_B);
            AddingHelper.AssignStudentToHometask(newStudent, homeTask_6, mark_A);

            //Course Has Next Students
            DisplayingData.CourseHasNextStudents(course_1);
            DisplayingData.CourseHasNextStudents(course_2);
            DisplayingData.CourseHasNextStudents(course_3);
            DisplayingData.CourseHasNextStudents(course_4);

            //Student has next courses
            DisplayingData.StudentHasNextCourses(newStudent);

            //Student has next Home Tasks
            DisplayingData.StudentHasNextHomeTasks(newStudent);

            //Home task has next Students and their marks 
            DisplayingData.HomeTaskHasNextStudents(homeTask_1);
            DisplayingData.HomeTaskHasNextStudents(homeTask_2);
            DisplayingData.HomeTaskHasNextStudents(homeTask_3);
            DisplayingData.HomeTaskHasNextStudents(homeTask_4);
            DisplayingData.HomeTaskHasNextStudents(homeTask_5);
            DisplayingData.HomeTaskHasNextStudents(homeTask_6);


            //List after added new Student. Count

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nList after added new Student. Count - {ImportStudents.Count}");
            Console.ResetColor();
            foreach (var st in ImportStudents)
            {
                Console.WriteLine(st.ToString());
            }

            //File after added new Student. Count

            var result3 = ExportXml.ExportStudents(ImportStudents);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nFile after added new Student. Count - {ImportStudents.Count}");
            Console.ResetColor();
            Console.WriteLine($"{result3}");

            //List after second Import Students. Count

            ImportXml.Import(ImportStudents);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nList after second Import Students. Count - {ImportStudents.Count}");
            Console.ResetColor();
            foreach (var st in ImportStudents)
            {
                Console.WriteLine(st.ToString());
            }

            //Export Course
            var resultCourse = ExportXml.ExportCourse(ImportCourses);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nFile after Export Courses. Count - {ImportCourses.Count}");
            Console.ResetColor();
            Console.WriteLine($"\n{resultCourse}");

            Console.ReadKey();
        }
    }
}



