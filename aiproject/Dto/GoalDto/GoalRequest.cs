namespace aiproject.Dto
{
    public class GoalRequest
    {
        public int AppearanceId { get; set; }

        public GoalRequest(int appearanceId)
        {
            AppearanceId = appearanceId;
        }
    }
}