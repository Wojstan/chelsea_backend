namespace aiproject.Dto
{
    public class AppearanceRequest
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }

        public AppearanceRequest(int matchId, int playerId)
        {
            MatchId = matchId;
            PlayerId = playerId;
        }
    }
}