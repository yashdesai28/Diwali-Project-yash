using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}