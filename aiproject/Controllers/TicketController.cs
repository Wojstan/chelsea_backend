using System.Linq;
using System.Security.Claims;
using aiproject.Dto.TicketDto;
using aiproject.Entities;
using aiproject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace aiproject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly TicketRepository _ticketRepository;
        private readonly DatabaseContext _databaseContext;

        public TicketController(TicketRepository ticketRepository, DatabaseContext databaseContext)
        {
            _ticketRepository = ticketRepository;
            _databaseContext = databaseContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetTickets(int id)
        {
            return Ok(_ticketRepository.GetAll().Where(ticket => ticket.MatchId == id).Select(ticket =>
                new TicketResponse
                {
                    Id = ticket.Id,
                    Row = ticket.Row,
                    Seat = ticket.Seat
                }).ToList());
        }
        
        [HttpPost]
        public IActionResult AddTicket(TicketRequest ticketRequest)
        {
            
            var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
                                   string.Empty);
            
            var ticket = new TicketEntity
            {
                Seat = ticketRequest.Seat,
                Row= ticketRequest.Row,
                MatchId = ticketRequest.MatchId,
                UserId = userId,
                UserEntity = _databaseContext.Set<UserEntity>().Find(userId)
            };

            _ticketRepository.Add(ticket);

            return Ok();
        }
    }
}