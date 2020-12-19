using System.Linq;
using System.Security.Claims;
using aiproject.Dto;
using aiproject.Dto.MatchDto;
using aiproject.Entities;
using aiproject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aiproject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MatchController : ControllerBase
    {
        private readonly MatchRepository _matchRepository;
        private readonly AppearanceRepository _appearanceRepository;
        private readonly GoalRepository _goalRepository;
        private readonly RatingRepository _ratingRepository;


        public MatchController(MatchRepository matchRepository, AppearanceRepository appearanceRepository,
            GoalRepository goalRepository, RatingRepository ratingRepository)
        {
            _matchRepository = matchRepository;
            _appearanceRepository = appearanceRepository;
            _goalRepository = goalRepository;
            _ratingRepository = ratingRepository;
        }

        [HttpGet("{id}")]
        public ActionResult<MatchEntity> GetMatch(int id)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
                                   string.Empty);
            
            var match = _matchRepository.Get(id);
            if (match == null)
            {
                return Ok(new MatchEntity(0));
            }

            var apps = _appearanceRepository.GetAllApps().Where(app => app.MatchId == id).ToList();
            var goals = _goalRepository.GetAllGoals().Where(goal => goal.AppearanceEntity.MatchId == id).ToList();
            var ratings = _ratingRepository.GetAllRatings().Where(rating => rating.AppearanceEntity.MatchId == id && rating.UserId==userId)
                .ToList();

            var matchResponse = new MatchResponse
            {
                Id = match.Id,
                Lineup = apps.Select(appearance => new MatchAppsResponse
                {
                    Id = appearance.Id, 
                    PlayerAppsResponse =
                        new PlayerAppsResponse
                        {
                            Id = appearance.PlayerEntity.Id, Number = appearance.PlayerEntity.Number,
                            Img = appearance.PlayerEntity.Img,
                            Name = appearance.PlayerEntity.Name, Surname = appearance.PlayerEntity.Surname,
                        }
                }).ToList(),
                Ratings = ratings.Where(rating => rating.UserId == userId).Select(rating => new RatingResponse
                {
                    Id = rating.Id, Value = rating.Value,
                    Appearance = rating.AppearanceEntity.Id,
                    Player = new RatingPlayerResponse
                    {
                        Id = rating.AppearanceEntity.PlayerId,
                        Img = rating.AppearanceEntity.PlayerEntity.Img,
                        Name = rating.AppearanceEntity.PlayerEntity.Name,
                        Number = rating.AppearanceEntity.PlayerEntity.Number,
                        Surname = rating.AppearanceEntity.PlayerEntity.Surname
                    }
                }).ToList(),
                Goals = goals.Select(goal => new GoalResponse
                {
                    Id = goal.Id,
                    Appearance = goal.AppearanceEntity.Id,
                    Player = new PlayerGoalResponse
                    {
                        Id = goal.AppearanceEntity.PlayerEntity.Id,
                        Number = goal.AppearanceEntity.PlayerEntity.Number,
                        Surname = goal.AppearanceEntity.PlayerEntity.Surname
                    }
                }).ToList()
            };

            return Ok(matchResponse);
        }

        [HttpPost("{id}")]
        public ActionResult<MatchEntity> AddMatch(int id)
        {
            var match = new MatchEntity(id);
            _matchRepository.Add(match);

            return Ok(match);
        }
    }
}