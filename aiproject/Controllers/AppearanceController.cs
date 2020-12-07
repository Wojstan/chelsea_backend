using System.Linq;
using aiproject.Dto;
using aiproject.Entities;
using aiproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace aiproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppearanceController : ControllerBase
    {
        private readonly AppearanceRepository _appearanceRepository;
        private readonly DatabaseContext _databaseContext;
        

        public AppearanceController(AppearanceRepository ratingRepository, DatabaseContext databaseContext)
        {
            _appearanceRepository = ratingRepository;
            _databaseContext = databaseContext;
        }

        [HttpGet("{id}")] //Apps from specific match
        public IActionResult GetMatchApps(int id)
        {
            var apps = _appearanceRepository.GetAllApps().Where(app => app.MatchId == id).ToList();

            return Ok(apps.Select(appearance => new MatchAppsResponse
            {
                Id = appearance.Id, PlayerAppsResponse =
                    new PlayerAppsResponse
                    {
                        Id = appearance.PlayerEntity.Id, Number = appearance.PlayerEntity.Number,
                        Name = appearance.PlayerEntity.Name, Surname = appearance.PlayerEntity.Surname,
                    }
            }));
        }

        [HttpPost]
        public ActionResult AddAppearance(AppearanceRequest appearanceRequest)
        {
            var appearance = new AppearanceEntity()
            {
                MatchId = appearanceRequest.MatchId,
                PlayerId = appearanceRequest.PlayerId,
                MatchEntity = _databaseContext.Set<MatchEntity>().Find(appearanceRequest.MatchId),
                PlayerEntity = _databaseContext.Set<PlayerEntity>().Find(appearanceRequest.PlayerId)
            };

            _appearanceRepository.Add(appearance);

            return Ok(new MatchAppsResponse
            {
                Id = appearance.Id,
                PlayerAppsResponse = new PlayerAppsResponse
                {
                    Id = appearance.PlayerEntity.Id, Number = appearance.PlayerEntity.Number,
                    Name = appearance.PlayerEntity.Name, Surname = appearance.PlayerEntity.Surname,
                }
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<AppearanceEntity> DeleteAppearance(int id)
        {
            var appearance = _appearanceRepository.Delete(id);
            if (appearance == null)
            {
                return NotFound();
            }

            return appearance;
        }
    }
}