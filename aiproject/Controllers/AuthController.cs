using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using aiproject.Dto;
using aiproject.Entities;
using aiproject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace aiproject.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly RoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly DatabaseContext _databaseContext;

        public AuthController(IAuthRepository authRepository, RoleRepository roleRepository, IConfiguration configuration, DatabaseContext databaseContext)
        {
            _authRepository = authRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
            _databaseContext = databaseContext;
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            if (User.Claims.ToList().Count == 0)
                return Unauthorized();
            
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
                                   string.Empty);

            var user = _databaseContext.Set<UserEntity>()
                .Include(u => u.RoleEntity).FirstOrDefault(u=> u.Id == userId);

            return Ok(new UserResponse()
            {
                Username = user?.Username,
                Email = user?.Email,
                RoleName = user?.RoleEntity.Name
            });

        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterRequest userRegisterRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            userRegisterRequest.Username = userRegisterRequest.Username.ToLower();

            if (_authRepository.UserExists(userRegisterRequest.Username))
                return BadRequest("Username already taken");
            
            // ReSharper disable once PossibleNullReferenceException
            var roleId = _roleRepository.GetAll().FirstOrDefault(role =>
                string.Equals(role.Name, "Normal", StringComparison.Ordinal)).Id;


            var newUser = new UserEntity
            {
                Username = userRegisterRequest.Username,
                Email = userRegisterRequest.Email,
                RoleId = roleId,
                RoleEntity = _databaseContext.Set<RoleEntity>().Find(roleId)
            };

            _authRepository.Register(newUser, userRegisterRequest.Password);

            return Ok();
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
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.NameIdentifier, repositoryUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, repositoryUser.Username),
                    new Claim(ClaimTypes.Email, repositoryUser.Email),
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new LoginResponse
            {
                User = new UserResponse()
                {
                    Username = userLoginRequest.Username,
                    Email = repositoryUser.Email,
                    RoleName = repositoryUser.RoleEntity.Name
                },
                Token = tokenString
            });
        }
    }
}