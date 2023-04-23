using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Connection;
using WebApplication1.Controllers;
using WebApplication1.Model.Attend;
using WebApplication1.Model.Meetings;
using WebApplication1.Model.User;

namespace WebApplication1.Area
{

    public class AttendenceController : BaseController
    {

        private readonly MyDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttendenceController(MyDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet("GetAllAttendance")]
        public async Task<IActionResult> GetAllAttendance()
        {
            try
            {
                var AllMeetingDb = await _context.Attendances.ToListAsync();

                return Ok(AllMeetingDb);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("GetAllAttendanceComing")]
        public async Task<IActionResult> GetAllAttendanceComing()
        {
            try
            {
                var AllMeetingDb = await _context.Attendances.Where(x=>x.Attended).ToListAsync();

                return Ok(AllMeetingDb);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        [HttpGet("GetAllAttendanceNotComing")]
        public async Task<IActionResult> GetAllAttendanceNotComing()
        {
            try
            {
                var AllMeetingDb = await _context.Attendances.Where(x=>!x.Attended).ToListAsync();

                return Ok(AllMeetingDb);
            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpPost("attendUserMeeting")]
        public async Task<IActionResult> attendUserMeeting(Attendance model)
        {
            try
            {
                if (ModelState.IsValid)
                    return BadRequest(ModelState);

                if (await _context.Meetings.FirstOrDefaultAsync(x => x.Id == model.MeetingId) == null)
                    return NotFound("Not Found This Meeting");

                if (await _userManager.FindByIdAsync(model.UserId) == null)
                    return NotFound("Not Found This User");

                model.Attended = true;
                model.TimeComing = DateTime.UtcNow;

                _context.Attendances.Add(model);
                _context.SaveChanges();
                return Ok($"Success Create Meeting {model}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("attendNotComingUserMeeting")]
        public async Task<IActionResult> attendNotComingUserMeeting(Attendance model)
        {
            try
            {
                if (ModelState.IsValid)
                    return BadRequest(ModelState);

                if (await _context.Meetings.FirstOrDefaultAsync(x => x.Id == model.MeetingId) == null)
                    return NotFound("Not Found This Meeting");

                if (await _userManager.FindByIdAsync(model.UserId) == null)
                    return NotFound("Not Found This User");

                model.Attended = false;
                model.TimeComing = DateTime.UtcNow;

                _context.Attendances.Add(model);
                _context.SaveChanges();
                return Ok($"Success Create Meeting {model}");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
