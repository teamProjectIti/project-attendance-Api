using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Connection;
using WebApplication1.Controllers;
using WebApplication1.Dto.User;
using WebApplication1.Extension;
using WebApplication1.Model.User;

namespace WebApplication1.Area
{
    public class AuthController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly MyDbContext context;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthController(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager,
           MyDbContext context, IConfiguration configuration, IWebHostEnvironment WebHostEnvironment)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
            this.configuration = configuration;
            _webHostEnvironment = WebHostEnvironment;
        }
        [HttpPost("Register")]

        public async Task<IActionResult> Register([FromBody] registerVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("plz, provide all The required fields");
            }
            var emailAddressExist = await userManager.FindByEmailAsync(registerVM.emailAddress);
            if (emailAddressExist != null)
            {
                return BadRequest($"User = {registerVM.emailAddress} already exists");
            }

            string imagesPAth = @"\ImageUser\default.png";
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    Guid d = Guid.NewGuid();

                    string host = _webHostEnvironment.WebRootPath;
                    string ImageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(host, "ImageUser", d + ImageName), FileMode.Create);
                    files[0].CopyTo(fileStream);

                    imagesPAth = @"\InstructorePhoto\" + d + ImageName;
                }
            }
            else
            {
                return BadRequest("plz, provide all The required fields");
            }
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Email = registerVM.emailAddress,
                Status = registerVM.Status,
                Address = registerVM.Address,
                NameFatherChurch = registerVM.NameFatherChurch,
                NameJop = registerVM.NameJop,
                BrithDate = registerVM.BrithDate,
                City = registerVM.City,
                UserName = registerVM.userName,
                CountChildren = registerVM.CountChildren,
                IdFaceBook = registerVM.IdFaceBook,
                Photo = imagesPAth,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(user, registerVM.Password);

            if (result.Succeeded)
            {
                switch (registerVM.Role)
                {

                    case UserRole.Manager:
                        await userManager.AddToRoleAsync(user, UserRole.Manager);
                        break;
                    case UserRole.user:
                        await userManager.AddToRoleAsync(user, UserRole.user);
                        break;
                    default:
                        break;
                }


                var tokenValue = await GenraterJwtTokenAsync(user);
                return Ok(tokenValue);
            };
            return BadRequest("user could not be create");
        }
        [HttpPost("login-user")]
        public async Task<IActionResult> login([FromBody] loginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("plz, provide all The required fields");
            }
            var emailAddressExist = await userManager.FindByEmailAsync(loginVM.emailAddress);
            if (emailAddressExist != null && await userManager.CheckPasswordAsync(emailAddressExist, loginVM.Password))
            {
                var tokenValue = await GenraterJwtTokenAsync(emailAddressExist);
                return Ok(tokenValue);
            }
            return Unauthorized();
        }
        [Authorize]

        [HttpGet("GetCrrentUser")]
        public async Task<ActionResult<AuthResultVM>> GetCrrentUser()
        {
            try
            {
                // var email=HttpContext.User?.Claims?.FirstOrDefault(x=>x.Type==ClaimTypes.Email)?.Value;
                // var user=await _userManager.FindByEmailAsync(email);
                var user = await userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
                var tokenValue = await GenraterJwtTokenAsync(user);
                return Ok(tokenValue);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("UpdateUser")]
        public async Task<ActionResult<AuthResultVM>> UpdateUser([FromBody] registerVM registerVM)
        {
            var user = await userManager.FindByEmailWithAddressAsync(HttpContext.User);
            user.City = registerVM.userName;
            user.Email = registerVM.emailAddress;
            user.FirstName = registerVM.FirstName;
            user.LastName = registerVM.LastName;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                switch (registerVM.Role)
                {

                    case UserRole.Manager:
                        await userManager.AddToRoleAsync(user, UserRole.Manager);
                        break;
                    case UserRole.user:
                        await userManager.AddToRoleAsync(user, UserRole.user);
                        break;
                    default:
                        break;
                }


                var tokenValue = await GenraterJwtTokenAsync(user);
                return Ok(tokenValue);
            };
            return BadRequest();
        }

        private async Task<AuthResultVM> GenraterJwtTokenAsync(ApplicationUser user)
        {
            var AuthClims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //add user role claims
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var item in userRoles)
            {
                AuthClims.Add(new Claim(ClaimTypes.Role, item));
            }
            var authreisterKey =
                 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecrtKey"]));
            var token = new JwtSecurityToken(
             issuer: configuration["JWT:ValidIssuer"],
             audience: configuration["JWT:ValidAudience"],
             expires: DateTime.UtcNow.AddHours(5),
             claims: AuthClims,
             signingCredentials: new SigningCredentials(authreisterKey, SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var role = await userManager.GetRolesAsync(user);
            bool state = false;
            string data = "";
            foreach (var item in role)
            {
                data = item;

            }

            AuthResultVM response = new AuthResultVM()
            {
                Token = jwtToken,
                ExpiredTime = token.ValidTo,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = data
            };
            return response;
        }
    }
}