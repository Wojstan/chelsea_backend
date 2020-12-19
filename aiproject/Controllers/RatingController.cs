using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using aiproject.Dto;
using aiproject.Entities;
using aiproject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aiproject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RatingController : ControllerBase
    {
        private readonly RatingRepository _ratingRepository;
        private readonly DatabaseContext _databaseContext;

        public RatingController(RatingRepository ratingRepository, DatabaseContext databaseContext)
        {
            _ratingRepository = ratingRepository;
            _databaseContext = databaseContext;
        }

        [HttpGet("{id}")] //Get ratings from match
        public ActionResult<IEnumerable<RatingResponse>> GetMatchRatings(int id)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
                                  string.Empty);

            var ratings = _ratingRepository.GetAllRatings().Where(rating => rating.AppearanceEntity.MatchEntity.Id == id && rating.UserId == userId).ToList();

            return Ok(ratings.Select(rating => new RatingResponse
            {
                Id = rating.Id, Value = rating.Value,
                Player = new RatingPlayerResponse
                {
                    Id = rating.AppearanceEntity.PlayerId,
                    Img = rating.AppearanceEntity.PlayerEntity.Img,
                    Name = rating.AppearanceEntity.PlayerEntity.Name,
                    Number = rating.AppearanceEntity.PlayerEntity.Number,
                    Surname = rating.AppearanceEntity.PlayerEntity.Surname
                }
            }));
        }

        [HttpPost]
        public ActionResult<RatingEntity> AddRating(RatingRequest ratingRequest)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
                 string.Empty);
            
            var rating = new RatingEntity()
            {
                Value = ratingRequest.Value,
                AppearanceId = ratingRequest.AppearanceId,
                UserId = userId,
                AppearanceEntity = _databaseContext.Set<AppearanceEntity>().Find(ratingRequest.AppearanceId),
                UserEntity = _databaseContext.Set<UserEntity>().Find(userId)
            };
            
            _ratingRepository.Add(rating);

            return Ok(
                _ratingRepository.GetAllRatings().Where(resGoal => resGoal.Id == rating.Id)
                    .Select(newGoal =>
                        new RatingResponse
                        {
                            Id = rating.Id, Value = rating.Value,
                            Appearance = rating.AppearanceId,
                            Player = new RatingPlayerResponse
                            {
                                Id = rating.AppearanceEntity.PlayerId,
                                Img = rating.AppearanceEntity.PlayerEntity.Img,
                                Name = rating.AppearanceEntity.PlayerEntity.Name,
                                Number = rating.AppearanceEntity.PlayerEntity.Number,
                                Surname = rating.AppearanceEntity.PlayerEntity.Surname
                            }
                        }
                    ).ToList().First()
            );
        }

        [HttpPut("{id}")]
        public ActionResult<RatingEntity> ModifyRating(int id, ModifyRatingRequest modifyRatingRequest)
        {
            var rating = _ratingRepository.GetAllRatings().First(rate => rate.Id == id);
            rating.Value = modifyRatingRequest.Value;
            
            Console.WriteLine(rating.Value+ " "+ " "+id);
            _ratingRepository.Update(rating);

            return Ok(new RatingResponse
            {
                Id = rating.Id, Value = rating.Value,
                Appearance = rating.AppearanceId,
                Player = new RatingPlayerResponse
                {
                    Id = rating.AppearanceEntity.PlayerId,
                    Img = rating.AppearanceEntity.PlayerEntity.Img,
                    Name = rating.AppearanceEntity.PlayerEntity.Name,
                    Number = rating.AppearanceEntity.PlayerEntity.Number,
                    Surname = rating.AppearanceEntity.PlayerEntity.Surname
                }
            });
        }
        
    }
}