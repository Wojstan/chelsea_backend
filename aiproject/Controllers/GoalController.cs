using System.Collections.Generic;
using System.Linq;
using aiproject.Dto;
using aiproject.Entities;
using aiproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace aiproject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalController : ControllerBase
    {
        private readonly GoalRepository _goalRepository;
        private readonly DatabaseContext _databaseContext;

        public GoalController(GoalRepository goalRepository, DatabaseContext databaseContext)
        {
            _goalRepository = goalRepository;
            _databaseContext = databaseContext;
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<GoalResponse>> GetMatchGoals(int id)
        {
            var goals = _goalRepository.GetAllGoals()
                .Where(goal => goal.AppearanceEntity.MatchId == id).ToList();

            return Ok(goals.Select(goal => new GoalResponse
            {
                Id = goal.Id,
                Player = new PlayerGoalResponse
                {
                    Id = goal.AppearanceEntity.PlayerEntity.Id,
                    Number = goal.AppearanceEntity.PlayerEntity.Number,
                    Surname = goal.AppearanceEntity.PlayerEntity.Surname
                }
            }));
        }

        [HttpPost]
        public ActionResult<GoalResponse> AddGoal(GoalRequest goalRequest)
        {
            var goal = new GoalEntity
            {
                AppearanceId = goalRequest.AppearanceId,
                AppearanceEntity = _databaseContext.Set<AppearanceEntity>().Find(goalRequest.AppearanceId)
            };
            
            _goalRepository.Add(goal);

            return Ok(_goalRepository.GetAllGoals().Where(resGoal => resGoal.Id == goal.Id)
                .Select(newGoal =>
                    new GoalResponse
                    {
                        Id = goal.Id,
                        Appearance = goal.AppearanceId,
                        Player = new PlayerGoalResponse
                        {
                            Id = goal.AppearanceEntity.PlayerEntity.Id,
                            Number = goal.AppearanceEntity.PlayerEntity.Number,
                            Surname = goal.AppearanceEntity.PlayerEntity.Surname
                        }
                    }
                ).ToList().First()
            );
        }


        [HttpDelete("{id}")]
        public ActionResult<GoalResponse> DeleteGoal(int id)
        {
            var goal = _goalRepository.Delete(id);
            if (goal == null)
            {
                return NotFound();
            }

            return Ok(goal.Id);
        }
    }
}