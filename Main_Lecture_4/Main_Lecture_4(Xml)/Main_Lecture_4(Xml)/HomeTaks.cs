 using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_4_Xml_
{
    public class HomeTaks
    {
        public Dictionary<Student, Marks> HomeTaksMarks { get; }
        public string HomeworkTitle { get; set; }
        public string HomeworkDescription { get; set; }
        public DateTime HomeworkDueDate { get; set; }
        public static int Count = 0;
        public void HomeTasksCount()
        {
            Count++;
        }

        public HomeTaks(string hwTitle, string hmDescription, string hwDueDate)
        {
            HomeworkTitle = hwTitle;
            HomeworkDescription = hmDescription;
            HomeworkDueDate = DateTime.ParseExact(hwDueDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            HomeTaksMarks = new Dictionary<Student, Marks>();
        }

        public override string ToString()
        {
            return ($"Home Task: {HomeworkTitle} \nDescription: {HomeworkDescription} \nDueDate: {HomeworkDueDate}\n");
        }

    }
}
