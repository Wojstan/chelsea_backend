using System.Collections.Generic;

namespace aiproject.Dto.MatchDto
{
    public class MatchResponse
    {
        public int Id { get; set; }
        public List<MatchAppsResponse> Lineup { get; set; }

        public List<GoalResponse> Goals { get; set; }
        
        public List<RatingResponse> Ratings { get; set; }
    }
}