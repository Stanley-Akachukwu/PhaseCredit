using PhaseCredit.API.Queries.Reservations;
using PhaseCredit.Core.DTOs.Resrvations;
using PhaseCredit.Data.Abstract;
using SimpleSoft.Mediator;

namespace PhaseCredit.API.Handlers.Reservations
{
    public class GetReservationsQueryHandler : IQueryHandler<GetReservationsQuery, ListReservationResponse>
    {
        private readonly IMediator _mediator;
        private readonly IReservationRepository _reserveRepository;

        public GetReservationsQueryHandler(IMediator mediator, IReservationRepository reserveRepository)
        {
            _mediator = mediator;
            _reserveRepository = reserveRepository;
        }
       
        public async Task<ListReservationResponse> HandleAsync(GetReservationsQuery query, CancellationToken ct)
        {
          var reservations = await _reserveRepository.GetReservationAsync();
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
