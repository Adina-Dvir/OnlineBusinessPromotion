using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBusinessPromotion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IService<UserDto> service;
        private readonly IConfiguration config;
        public LoginController(IService<UserDto> service,IConfiguration config)
        {
            this.service = service;
            this.config = config;
        }
        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public async Task< string> Get(int id)
        {
            return "value";
        }

        // POST api/<LoginController>
        [HttpPost]
        public async Task Post([FromBody] UserDto user)
        {
            await service.AddItem(user);
        }
        // POST api/<LoginController>
       
        // [HttpPost("login")]
        //public async Task<string> Login([FromBody] UserLogin ul)
        //{
        //    var user=await Authenticate(ul);
        //    if (user!=null)
        //    {
        //        var token =  Generate(user);
        //        return token;

        //    }
        //    return "user not found or password is incorrect";
        //}
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin ul)
        {
            var user = await Authenticate(ul);
            UserDto u=GetCurrentUser();
            if (user != null)
            {
                var token = Generate(user);
              //  return Ok(new { token }); // יחזיר JSON: { "token": "..." }
                return Ok(new { token, user = new { user.UserName, user.UserEmail, user.UserPassword } }); // יחזיר JSON: { "token": "...", "user": { "UserName": "...", "UserEmail": "...", "UserPassword": "..." } }
            }

            return BadRequest(new { message = "User not found or password is incorrect" });
        }


        private string Generate(UserDto user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Email,user.UserEmail),
            new Claim(ClaimTypes.PostalCode,user.UserPassword),
            //new Claim(ClaimTypes.GivenName,user.Name)
            };
            //var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],
            //    claims,
            //    expires: DateTime.Now.AddMinutes(30),
            //    signingCredentials: credentials);
            var token = new JwtSecurityToken(
             config["Jwt:Issuer"],
             config["Jwt:Audience"],
             claims,
             expires: DateTime.Now.AddMinutes(30),
             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        private UserDto GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var UserClaim = identity.Claims;
                return new UserDto()
                {
                    UserName = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                    UserEmail = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                    UserPassword = UserClaim.FirstOrDefault(x => x.Type == ClaimTypes.PostalCode)?.Value,
                };
            }
            return null;
        }
        //האם המשתמש קיים?
        private async Task< UserDto> Authenticate(UserLogin ul)
        {
            var allUsers = await service.GetAll();
            var user = allUsers.FirstOrDefault(x => x.UserPassword == ul.Password && x.UserEmail == ul.UserMail);
            if (user != null)
                return user;
            return null;
        }


        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
        }
    }
}
