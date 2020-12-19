using System.Text.Json.Serialization;
using aiproject.Entities;

namespace aiproject.Dto
{
    public class MatchAppsResponse
    {
        public int Id { get; set; }
        [JsonPropertyName("player")]
        public PlayerAppsResponse PlayerAppsResponse { get; set; }
    }

    public class PlayerAppsResponse
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}