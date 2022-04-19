using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhaseCredit.API.Queries.Reservations;
using PhaseCredit.Core.DTOs.Resrvations;
using SimpleSoft.Mediator;

namespace PhaseCredit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
  [Authorize]
   //[AllowAnonymous]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("reservations")]
        public async Task<ListReservationResponse> GetListAsync(CancellationToken ct)
        {
            var response = new ListReservationResponse();
           
            response = await _mediator.FetchAsync(new GetReservationsQuery(),ct);
            return response;
        }
      
    }


}
