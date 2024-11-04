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
    public class TimeTableController : ControllerBase
    {
        private readonly TimeTableRepository timeTable;
        public TimeTableController(IConfiguration configuration){
            timeTable=new (configuration);
        }

        [HttpGet]
        [Route("gettecherinfo")]
        public IActionResult gettecherinfo()
        {
            Console.WriteLine("hello");
            List<Teacherinfo>teacherinfos=timeTable.GetTeacherinfos();

            return Ok(teacherinfos);
        }
    }
}