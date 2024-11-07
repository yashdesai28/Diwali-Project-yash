using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly TimeTableRepository timeTable;
        private readonly ClassWiseStudentRepository classWiseStudent;
        public TeacherController(IConfiguration configuration)
        {
            timeTable = new(configuration);
            classWiseStudent = new(configuration);
        }

        [HttpGet]
        [Route("gettecherinfo")]
        public IActionResult gettecherinfo()
        {
            Console.WriteLine("hello");
            List<Teacherinfo> teacherinfos = timeTable.GetTeacherinfos();
            return Ok(teacherinfos);
        }


        [HttpGet]
        [Route("getclassWiseStudent")]
        public IActionResult getclassWiseStudent(int id)
        {
            List<Student.StudentDetailsWithFees> studentDetailsWithFees = classWiseStudent.StudentDetailsWithFees(id);
            return Ok(studentDetailsWithFees);
        }

        [HttpDelete]
        [Route("deleteclassWiseStudent")]
        public IActionResult deleteclassWiseStudent(int id)
        {
            int row = classWiseStudent.DeleteStudentDetailsWithFees(id);
            if (row > 0)
            {
                return Ok(new { Message = "Student deleted successfully" });
            }
            else
            {
                return Ok(new { Message = "Student not deleted" });
            }

        }

        [HttpPatch]
        [Route("updateclassWiseStudent")]
        public IActionResult updateclassWiseStudent(int id, string standers, bool studying)
        {

            int row = classWiseStudent.updateStudentDetailsWithFees(id, standers, studying);
            if (row > 0)
            {
                return Ok(new { Message = "Student Update successfully" });
            }
            else
            {
                return Ok(new { Message = "Student not update" });
            }
        }

        [HttpGet]
        [Route("timetable")]
        public IActionResult timetable()
        {
            Console.WriteLine("thime table");
            (List<string> standers, List<Timetable> timetables) = timeTable.Getsatnders();
            return Ok(new { standers, timetables });
        }

        [HttpGet]
        [Route("timetableForTeacher")]
        public IActionResult timetableForTeacher()
        {
            Console.WriteLine("thime table");
            (List<string> standers, List<Timetable> timetables) = timeTable.GetTeacherTimetable();
            return Ok(new { standers, timetables });
        }

    }
}