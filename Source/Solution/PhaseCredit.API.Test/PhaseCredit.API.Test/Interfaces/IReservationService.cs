using PhaseCredit.Core.DTOs.Resrvations;

namespace PhaseCredit.API.Test.Interfaces
{
    public interface IReservationService
    {
        Task<ListReservationResponse> GetListAsync(string jwt);
    }
}
