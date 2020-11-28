using System.Collections.Generic;
using System.Threading.Tasks;
using aiproject.Dto;
using aiproject.Entities;
using aiproject.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace aiproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserEntity>> Get(int id)
        {
            var user = await _repository.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<UserEntity>> AddUser(UserRequest userRequest)
        {
            var newUser = new UserEntity();
            newUser.Username = userRequest.Username;
            newUser.Email = userRequest.Email;
            await _repository.Add(newUser);
            return CreatedAtAction("AddUser", new {id = newUser.Id}, newUser);
        }

        [HttpDelete]
        public async Task<ActionResult<UserEntity>> DeleteUser(int id)
        {
            var user = await _repository.Delete(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserEntity>> ModifyUser(int id, UserEntity userEntity)
        {
            if (id != userEntity.Id)
            {
                return BadRequest();
            }

            await _repository.Update(userEntity);
            return userEntity;
        }
        
    }
}