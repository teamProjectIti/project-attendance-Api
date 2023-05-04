using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Connection;
using WebApplication1.Controllers;
using WebApplication1.Dto.Common;
using WebApplication1.Dto.MeetingUser;
using WebApplication1.Model.MeetingUser;

namespace WebApplication1.Area
{

    public class MeetingUserUserController : BaseController
    {

        private readonly MyDbContext _context;
 
        public MeetingUserUserController(MyDbContext context )
        {
            _context = context;
         }
        [HttpGet("GetAllMeetingUser")]
        public async Task<IActionResult> GetAllMeetingUser([FromQuery]PagnationDto Model)
        {
            try
            {
                var skipCount = (Model.page - 1) * Model.pageSize;
                var totalItems = await _context.MeetingUsers.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)Model.pageSize);

                var data = await _context.MeetingUsers
                    .OrderBy(x => x.Id)
                    .Skip(skipCount)
                    .Take(Model.pageSize)
                    .Select(x => new { meetingname = x.Meeting.Name, User = x.ManageUser.UserName, MeetingId = x.MeetingId, userId = x.UserId, x.Id, x.date, x.CreateAt })
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

        [HttpPost("createMeetingUser")]
        public async Task<IActionResult> createMeetingUser(MeetingUserDto model)
        {
            try
            {
                var obj = new MeetingUsers()
                {
                    MeetingId= model.MeetingId,
                    UserId= model.UserId,
                    date= DateTime.UtcNow,
                };

                _context.MeetingUsers.Add(obj);
                _context.SaveChanges();
                return Ok(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
         [HttpPut("UpdateMeetingUser")]
        public async Task<IActionResult> UpdateMeetingUser(MeetingUserDto model)
        {
            try
            {
                 
                var resultDb = await _context.MeetingUsers.FirstOrDefaultAsync(x => x.Id == model.Id);

                if (resultDb is null)
                    return BadRequest("Not Found This User");


                resultDb.UserId = model.UserId;
                resultDb.MeetingId = model.MeetingId;
                resultDb.date = DateTime.UtcNow;
               
                _context.MeetingUsers.Update(resultDb);
                _context.SaveChanges();

                return Ok($"Success Update MeetingUser {model}");
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpDelete("DeleteMeetingUser")]
        public async Task<IActionResult> DeleteMeetingUser([FromQuery] long id)
        {
            try
            {
                if (id == null || id == 0)
                    return BadRequest("This Id Not Found");


                var resultDb = await _context.MeetingUsers.FirstOrDefaultAsync(x => x.Id == id);

                if (resultDb is null)
                    return BadRequest("Not Found This User");

                _context.MeetingUsers.Remove(resultDb);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
     
    [HttpGet("GetDetailsMeetingUser")]
        public async Task<IActionResult> GetDetailsMeetingUser([FromQuery] long id)
        {
            try
            {
                if (id == null || id == 0)
                    return BadRequest("This Id Not Found");


                var resultDb = await _context.MeetingUsers.Select(x=>new { meetingname=x.Meeting.Name,User=x.ManageUser.UserName,MeetingId=x.MeetingId, userId=x.UserId,x.Id,x.date,x.CreateAt}).FirstOrDefaultAsync(x => x.Id == id);

                if (resultDb is null)
                    return BadRequest("Not Found This MeetingUser");


                return Ok(resultDb);
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
