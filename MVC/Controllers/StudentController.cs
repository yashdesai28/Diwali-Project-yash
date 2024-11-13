using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MVC.Controllers
{
    //[Route("[controller]")]
    public class StudentController : Controller
    {

        public IActionResult SchoolInfo() => View();
        public IActionResult UpdateProfile() => View();
        public IActionResult StudentTimeTable() => View();
        public IActionResult StudentFeesDetails() => View();
        public IActionResult ShowStudentFeedbacks()=> View();

        [HttpGet]
        public IActionResult FeedbackByStudent()
        {
            ViewBag.UserID = 101;
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}