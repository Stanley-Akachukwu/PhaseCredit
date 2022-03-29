using PhaseCredit.API.Queries.Reservations;
using PhaseCredit.Core.DTOs.Resrvations;
using PhaseCredit.Core.Services.Reservations;
using SimpleSoft.Mediator;

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
                    ResponseCode = StatusCodes.Status404NotFound,
                    ResponseMessage = "reservation list not found"
                };
            }

            return new ListReservationResponse
            {
                Reservations= reservations.ToList(),
                ResponseCode=StatusCodes.Status200OK,
                ResponseMessage="Successful"
            };
        }
    }
}
