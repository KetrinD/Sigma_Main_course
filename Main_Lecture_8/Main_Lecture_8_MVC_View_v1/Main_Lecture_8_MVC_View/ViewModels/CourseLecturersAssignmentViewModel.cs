using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main_Lecture_8_MVC_View.ViewModels
{
        public class CourseLecturersAssignmentViewModel
        {
            public string Name { get; set; }

            public int Id { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }

            public int PassCredits { get; set; }

            public List<LectureViewModel> Lecturers { get; set; }
        }

        public class LectureViewModel
        {
            public int LectureId { get; set; }

            public string LectureFullName { get; set; }

            public bool IsAssigned { get; set; }
        }
    }

