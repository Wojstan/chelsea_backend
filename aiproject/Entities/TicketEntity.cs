namespace aiproject.Entities
{
    public class TicketEntity : IEntity
    {
        public int Id { get; set; }
        public int Seat { get; set; }
        public string Row { get; set; }
        public int MatchId { get; set; }

        public int UserId { get; set; }

        public UserEntity UserEntity { get; set; }
    }
}