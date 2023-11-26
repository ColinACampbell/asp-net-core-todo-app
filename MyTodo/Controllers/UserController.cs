using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyTodo.Models;
using MyTodo.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyTodo.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        IBaseRepository<User> _userRepository;
        private readonly IConfiguration _config;

        public UserController(IBaseRepository<User> userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }
      
        [HttpPost()]
        public UserReturn CreateUserAdmin([FromBody] User user)
        {
            var newUser = _userRepository.Create(user);
            newUser.password = null;

            var userRtn = new UserReturn(newUser);
            userRtn.token = GenerateToken(userRtn);
            
            return userRtn;
        }

        [HttpPost()]
        public UserReturn LoginUser([FromBody] User user) {
            //user = _userRepository.
            return new UserReturn();
        }

        // To generate token
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}

