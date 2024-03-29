﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main_Lecture_8_MVC_View.ViewModels
{
        public class CourseStudentAssignmentViewModel
        {
            public string Name { get; set; }

            public int Id { get; set; }

            public DateTime StartDate { get; set; }

            public DateTime EndDate { get; set; }

            public int PassCredits { get; set; }

            public List<StudentViewModel> Students { get; set; }
        }

        public class StudentViewModel
        {
            public int StudentId { get; set; }

            public string StudentFullName { get; set; }

            public bool IsAssigned { get; set; }
        }
    }

