using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main_Lecture_8_MVC_View.ViewModels
{
    public class CourseHomeTasksViewModel
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int PassCredits { get; set; }

        public List<HomeTasksViewModel> HomeTasks { get; set; }
    }

    public class HomeTasksViewModel
    {
        public int HomeTasksId { get; set; }

        public string HomeTasksTitle { get; set; }

        public string HomeTasksDescription { get; set; }
        
        public int HomeTasksNumber { get; set; }

        public DateTime HomeTasksDate { get; set; }


    }

}

