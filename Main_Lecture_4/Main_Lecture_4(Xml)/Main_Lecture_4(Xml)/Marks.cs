using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Lecture_4_Xml_
{
    public class Marks
    {
        public string MarkTitle { get; set; }
        public DateTime MarksDate { get; set; }

        public Marks(string markTitle, string marksDate)
        {
            MarkTitle = markTitle;
            MarksDate = DateTime.ParseExact(marksDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        }

        public override string ToString()
        {
            return ($"Mark: {MarkTitle}");
        }
    }
}
