using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo_MVC.Models;
using WebAPI.Models;

namespace WebAPI.Repositories.Interfaces
{
    public interface ITimeTableRepository
    {
        public List<Teacherinfo> GetTeacherinfos();
        public (List<string>, List<Timetable>) Getsatnders();
        public (List<string>, List<Timetable>) GetTeacherTimetable();
    }
}