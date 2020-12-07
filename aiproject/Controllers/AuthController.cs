using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using aiproject.Dto;
using aiproject.Entities;
using aiproject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace aiproject.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _databaseContext;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration, DatabaseContext databaseContext)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _databaseContext = databaseContext;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            userRegisterRequest.Username = userRegisterRequest.Username.ToLower();

            if (_authRepository.UserExists(userRegisterRequest.Username))
                return BadRequest("Username already taken");

            var newUser = new UserEntity
            {
                Username = userRegisterRequest.Username,
                Email = userRegisterRequest.Email,
                RoleId = 4,
                RoleEntity = _databaseContext.Set<RoleEntity>().Find(4)
            };

            return Ok(_authRepository.Register(newUser, userRegisterRequest.Password));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest userLoginRequest)
        {
            var repositoryUser = _authRepository.Login(userLoginRequest.Username.ToLower(), userLoginRequest.Password);
            if (repositoryUser == null)
                return Unauthorized();
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, repositoryUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, repositoryUser.Username),
                    new Claim(ClaimTypes.Email, repositoryUser.Email),
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {tokenString});
        }
        
        
    }
}