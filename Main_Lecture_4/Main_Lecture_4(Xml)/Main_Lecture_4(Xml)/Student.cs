using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_4_Xml_
{
    public class Student
    {
        public Dictionary<string, string> ExtraData { get; }
        public List<Course> Courses { get; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string GitHubLink { get; set; }
        public string Phone { get; set; }
 

        public Student()
        {
            ExtraData = new Dictionary<string, string>();
            Courses = new List<Course>();
        }

        public override string ToString()
        {
            return LastName + " " + FirstName + " " + Birthday;
        }
    }
}
