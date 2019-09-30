using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_4_Xml_
{
    public static class DisplayingData
    {
        // Teacher has next courses
        public static void TeacherHasNextCourses(Teacher teacher)
        {

            Console.WriteLine($"\nFirst Teacher: {teacher.TeacherFirstName} {teacher.TeacherLastName} has next courses: ");
            foreach (var c in teacher.Courses)
            {
                Console.WriteLine(c.ToString());
            }
        }

        // Course has next Teachers:
        public static void CourseHasNextTeachers(Course course)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\nCourse: {course} has next Teachers: ");
            Console.ResetColor();
            foreach (var t in course.CourseTeachers)
            {
                Console.WriteLine(t.ToString());
            }
        }

        //Course has next home tasks:   
        public static void CourseHasNextHomeTasks(Course course)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nCourse: {course} has next home tasks: ");
            Console.ResetColor();
            foreach (var c in course.CourseHomeTasksList)
            {
                Console.WriteLine(c.ToString());
            }

        }
        // Course Has Next Students
        public static void CourseHasNextStudents(Course course)
        {
            Console.WriteLine($"\nStudents on Course: {course}");
            foreach (var c in course.CourseStudents)
            {
                Console.WriteLine(c.ToString());
            };
        }

        //Student has next courses
        public static void StudentHasNextCourses(Student student)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nStudent {student.FirstName} {student.LastName} has next courses:");
            Console.ResetColor();
            foreach (var course in student.Courses)
            {
                Console.WriteLine(course);
            }
            Console.ResetColor();
        }

        ////Student has next Home Tasks
        public static void StudentHasNextHomeTasks(Student student)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nStudent {student.FirstName} {student.LastName} has next home tasks:");
            Console.ResetColor();
            foreach (var task in student.AllHomeTasksMarks)
            {
                Console.WriteLine(task);
            }
            Console.ResetColor();
        }

        //Home task has next Students and their marks 
        public static void HomeTaskHasNextStudents(HomeTaks homeTask)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nHomeTask{homeTask.HomeworkTitle} has next Student and mark:");
            Console.ResetColor();
            foreach (var st in homeTask.HomeTaksMarks)
            {
                Console.WriteLine(st);
            }
            Console.ResetColor();
        }
    }
}
