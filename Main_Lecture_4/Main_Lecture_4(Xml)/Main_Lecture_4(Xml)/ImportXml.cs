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
        public static void Import(List<Student> ImportStudents)
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
    }
}
