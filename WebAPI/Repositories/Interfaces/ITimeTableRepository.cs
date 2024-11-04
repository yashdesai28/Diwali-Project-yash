using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Repositories.Interfaces
{
    public interface ITimeTableRepository
    {
        public List<Teacherinfo> GetTeacherinfos();
    }
}