using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Connection;
using WebApplication1.Controllers;
using WebApplication1.Dto.User;
using WebApplication1.Model.User;

namespace YourProject.Controllers
{
    public class UserController : BaseController
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public UserController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/User?pageNumber=1&pageSize=10
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(int pageNumber = 1, int pageSize = 10)
        {
            var totalPages = await _context.ManageUsers.CountAsync();
            var users = await _context.ManageUsers
                .OrderBy(u => u.UserName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var data = _mapper.Map<List<UserDto>>(users);

            return Ok(new
            {
                totalPages,
                data
            });
        }

        // GET: api/User/5
        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.ManageUsers.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }
        [HttpGet("SearchUser")]
        public async Task<ActionResult<UserDto>> SearchUser(string? name,string? father, string? Address)
        {
            var user = await _context.ManageUsers.Where(x=>x.UserName.Contains(name) || x.NameFatherChurch.Contains(father) || x.Address.Contains(Address)).ToListAsync();

            if (user == null)
            {
                return NotFound();
            }

            var data = _mapper.Map<List<UserDto>>(user);

            return Ok(data);

        }

        // POST: api/User
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<ManageUser>(userDto);

            _context.ManageUsers.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, _mapper.Map<UserDto>(user));
        }

        // PUT: api/User/5
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var user = await _context.ManageUsers.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(userDto, user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.ManageUsers.FirstOrDefaultAsync(x=>x.Id==id);

            if (user == null)
            {
                return NotFound();
            }

            _context.ManageUsers.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.ManageUsers.Any(u => u.Id == id);
        }
    }
}
