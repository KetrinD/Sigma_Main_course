using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_4_Xml_
{
    class Teacher
    {
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
        public DateTime Birthday { get; set; }
        public List<Course> Courses { get; }

        public Teacher(string FirstName, string LastName, string DB)
        {
            TeacherFirstName = FirstName;
            TeacherLastName = LastName;
            Birthday = DateTime.ParseExact(DB, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            Courses = new List<Course>();
        }
    }
}
