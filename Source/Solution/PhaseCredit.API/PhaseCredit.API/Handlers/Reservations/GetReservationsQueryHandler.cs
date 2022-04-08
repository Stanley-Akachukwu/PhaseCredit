using PhaseCredit.API.Queries.Reservations;
using PhaseCredit.Core.DTOs.Resrvations;
using PhaseCredit.Core.Services.Reservations;
using SimpleSoft.Mediator;
using System.Net;

namespace PhaseCredit.API.Handlers.Reservations
{
    public class GetReservationsQueryHandler : IQueryHandler<GetReservationsQuery, ListReservationResponse>
    {
        private readonly IMediator _mediator;
        private readonly IReservationService _reservationService;

        public GetReservationsQueryHandler(IMediator mediator, IReservationService reservationService)
        {
            _mediator = mediator;
            _reservationService = reservationService;
        }
       
        public async Task<ListReservationResponse> HandleAsync(GetReservationsQuery query, CancellationToken ct)
        {
          var reservations = await _reservationService.GetReservationAsync();
            if (reservations == null)
            {
                return new ListReservationResponse
                {
                    ResponseCode = HttpStatusCode.NotFound,
                    ResponseMessage = "reservation list not found"
                };
            }

            return new ListReservationResponse
            {
                Reservations= reservations.ToList(),
                ResponseCode= HttpStatusCode.OK,
                ResponseMessage ="Successful"
            };
        }
    }
}
