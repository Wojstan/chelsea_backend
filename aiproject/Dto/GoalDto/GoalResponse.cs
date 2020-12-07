using System.Text.Json.Serialization;

namespace aiproject.Dto
{
    public class GoalResponse
    {
        public int Id { get; set; }
        
        [JsonPropertyName("player")]
        public PlayerGoalResponse PlayerGoalResponse { get; set; }
    }

    public class PlayerGoalResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Surname { get; set; }
    }
}