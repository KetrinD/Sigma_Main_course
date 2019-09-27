using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_4_Xml_
{
    public class Course
    {
        public string CourseTitle { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public int PassingScore { get; set; }
        public List<Student> CourseStudents { get; set; }
        public Course()
        { }


        public Course(string st)
        {
            CourseTitle = st;
            CourseStudents = new List<Student>();
        }

        public Course(string st,string startDay, string endDay, int passingScore)
        {
            CourseTitle = st;
            StartDay = DateTime.ParseExact(startDay, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            EndDay = DateTime.ParseExact(endDay, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            PassingScore = passingScore;
            CourseStudents = new List<Student>();
        }

        public override string ToString()
        {
            return CourseTitle;
        }
    }
} 
