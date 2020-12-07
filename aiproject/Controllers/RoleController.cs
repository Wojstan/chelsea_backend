using aiproject.Entities;
using aiproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace aiproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {

        private readonly RoleRepository _roleRepository;

        public RoleController(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_roleRepository.GetAll());
        }

        [HttpPost]
        public IActionResult AddRole(string name)
        {
            var role = new RoleEntity{Name = name};
            
            return Ok(_roleRepository.Add(role));
        }
    }
}