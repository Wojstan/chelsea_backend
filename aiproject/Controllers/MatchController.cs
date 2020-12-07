using aiproject.Entities;
using aiproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace aiproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly MatchRepository _matchRepository;

        public MatchController(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }
        
        [HttpPost]
        public ActionResult<MatchEntity> AddMatch(int id)
        {
            var match = new MatchEntity(id);
            _matchRepository.Add(match);

            return Ok(match);
        }
    }
}