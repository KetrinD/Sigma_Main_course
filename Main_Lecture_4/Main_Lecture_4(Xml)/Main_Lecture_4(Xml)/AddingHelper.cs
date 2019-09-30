using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_4_Xml_
{
    public static class AddingHelper
    {
        // corurses added to Teacher 
        // Teacher added to courses
        public static void AssignTeacherToCourse(Teacher teacher, Course course)
        {
            teacher.Courses.Add(course);
            course.CourseTeachers.Add(teacher);
        }

        // corurses added to student 
        // student added to courses
        public static void AssignStudentToCourse(Student student, Course course)    
        {
            student.Courses.Add(course);
            course.CourseStudents.Add(student);
        }

        // all hometasks and its mark added to student
        // student added to home tasks
        public static void AssignStudentToHometask(Student student, HomeTaks homeTaks, Marks mark)
        {
            student.AllHomeTasksMarks.Add(homeTaks, mark);
            homeTaks.HomeTaksMarks.Add(student, mark);
        }
    }
}
