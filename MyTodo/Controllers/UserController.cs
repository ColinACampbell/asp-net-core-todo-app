﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyTodo.Models;
using MyTodo.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyTodo.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        IBaseRepository<User> _userRepository;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserController(IBaseRepository<User> userRepository, IConfiguration config, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _config = config;
            _passwordHasher = passwordHasher;
        }

        [HttpPost()]
        public UserReturn CreateAdmin([FromBody] User user)
        {
            var newUser = _userRepository.Create(user);
            newUser.password = null;

            var userRtn = new UserReturn(newUser);
            userRtn.token = GenerateToken(userRtn);

            return userRtn;
        }

        [HttpPost()]
        public async Task<ActionResult> Auth([FromBody] User user)
        {
            var userRtn = await _userRepository.Find(x => x.email == user.email);

            if (userRtn == null)
            {
                return NotFound();
            }
            else
            {
                var passvalid = ComparePasswords(userRtn, user.password, userRtn.password);
                if (passvalid)
                {
                    UserReturn userReturn = new UserReturn(userRtn);
                    userReturn.token = GenerateToken(userRtn);
                    return Ok(userReturn);
                }
                else
                {
                    return Forbid();
                }
                //return Ok(userRtn);
            }
        }

        // To generate token
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _config["JWTSettings:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud, _config["JWTSettings:Audience"]),
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private bool ComparePasswords(User user, string unhashed, string hashed)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashed, unhashed);
            if (result.Equals(PasswordVerificationResult.Success))
                return true;
            else
                return false;
        }
    }
}

