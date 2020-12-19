using aiproject.Entities;

namespace aiproject.Repositories
{
    public class TicketRepository : BaseRepository<TicketEntity, DatabaseContext>
    {
        public TicketRepository(DatabaseContext context) : base(context)
        {
        }
    }
}