using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repositories;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly SchoolInfoRepository _schoolInfoRepository;
        private readonly StudentRepository _studentRepository;
        private readonly FeesRepository _feesRepository;

        public StudentController(IConfiguration configuration)
        {
            _schoolInfoRepository = new SchoolInfoRepository(configuration);
            _studentRepository = new StudentRepository(configuration);
            _feesRepository = new FeesRepository(configuration);
        }


        [HttpPut("UpdateStudentProfile/{userId}")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        public IActionResult UpdateStudentProfile(int userId, [FromForm] User.UpdateDetails profile)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid student profile data.");

            profile.UserId = userId;

            try
            {
                var isUpdated = _studentRepository.UpdateStudentProfile(profile);
                if (isUpdated)
                    return Ok("Student profile updated successfully.");
                else
                    return NotFound("Student not found.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }


        [HttpGet("GetProfileById/{userId}")]
        public ActionResult<User.UpdateDetails> GetProfileById(int userId)
        {
            try
            {
                // Corrected the reference to the correct repository and used FirstOrDefault correctly
                var studentProfile = _studentRepository.GetUpdateProfileListById(userId).FirstOrDefault();

                if (studentProfile == null)
                {
                    return NotFound("Student not found.");
                }

                return Ok(studentProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetFeesDetails/{userId}")]
        [Produces("application/json")]
        public ActionResult<FeesInfo.FeesDetailsForStudent> GetFeesDetails(int userId)
        {
            try
            {
                var studentDetails = _feesRepository.GetFeesDetailsStudent(userId);

                if (studentDetails == null || !studentDetails.Any())
                {
                    return NotFound("Student Fees Details not found.");
                }

                return Ok(studentDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("GetSchoolInfo")]
        public ActionResult<SchoolInfo.Get> GetSchoolInfo()
        {
            try
            {
                var schoolInfo = _schoolInfoRepository.GetSchoolInfo();
                return Ok(schoolInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
