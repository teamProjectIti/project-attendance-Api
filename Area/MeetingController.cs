using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Connection;
using WebApplication1.Controllers;
using WebApplication1.Dto.Common;
using WebApplication1.Dto.Meeting;
using WebApplication1.Model.Meetings;
using WebApplication1.Model.User;

namespace WebApplication1.Area
{

    public class MeetingController : BaseController
    {

        private readonly MyDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MeetingController(MyDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet("GetAllMeeting")]
        public async Task<IActionResult> GetAllMeeting([FromQuery]PagnationDto Model)
        {
            try
            {
                var skipCount = (Model.page - 1) * Model.pageSize;
                var totalItems = await _context.Meetings.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)Model.pageSize);

                var data = await _context.Meetings
                    .OrderBy(x => x.Id)
                    .Skip(skipCount)
                    .Take(Model.pageSize)
                    .ToListAsync();

                return Ok(new
                {
                    totalPages,
                    data
                });

            }
            catch (Exception)
            {
                throw;
            }
        }



            [HttpPost("createMeeting")]
        public async Task<IActionResult> createMeeting(MeetingDto model)
        {
            try
            {
                var obj = new Meeting()
                {
                    Name= model.Name,
                    Date= DateTime.Now,
                };



                _context.Meetings.Add(obj);
                _context.SaveChanges();
                return Ok($"Success Create Meeting Id = {obj.Id}");
            }
            catch (Exception)
            {
                throw;
            }
        }
         [HttpPut("UpdateMeeting")]
        public async Task<IActionResult> UpdateMeeting(MeetingDto model)
        {
            try
            {
                 
                var resultDb = await _context.Meetings.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (resultDb is null)
                    return BadRequest("Not Found This User");


                resultDb.Name = model.Name;
                resultDb.Date = DateTime.Now;
               
                _context.Meetings.Update(resultDb);
                _context.SaveChanges();

                return Ok($"Success Update Meeting {model}");
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpDelete("DeleteMeeting")]
        public async Task<IActionResult> DeleteMeeting([FromQuery] long id)
        {
            try
            {
                if (id == null || id == 0)
                    return BadRequest("This Id Not Found");


                var resultDb = await _context.Meetings.FirstOrDefaultAsync(x => x.Id == id);

                if (resultDb is null)
                    return BadRequest("Not Found This User");

                _context.Meetings.Remove(resultDb);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }




     
    [HttpGet("GetDetailsMeeting")]
        public async Task<IActionResult> GetDetailsMeeting([FromQuery] long id)
        {
            try
            {
                if (id == null || id == 0)
                    return BadRequest("This Id Not Found");


                var resultDb = await _context.Meetings.Select(x=>new {x.Name,x.Id,x.Date,x.CreateAt}).FirstOrDefaultAsync(x => x.Id == id);

                if (resultDb is null)
                    return BadRequest("Not Found This Meeting");


                return Ok(resultDb);
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
