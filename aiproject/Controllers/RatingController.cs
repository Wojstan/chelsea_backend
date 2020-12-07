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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
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

            var ratings = _ratingRepository.GetAllRatings()
                .Where(rating => rating.AppearanceEntity.MatchEntity.Id == id && rating.UserId == userId).ToList();

            return Ok(ratings.Select(rating => new RatingResponse
            {
                Id = rating.Id, Value = rating.Value,
                RatingPlayerResponse = new RatingPlayerResponse
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

            return Ok(new RatingResponse
            {
                Id = rating.Id, Value = rating.Value,
                RatingPlayerResponse = new RatingPlayerResponse
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