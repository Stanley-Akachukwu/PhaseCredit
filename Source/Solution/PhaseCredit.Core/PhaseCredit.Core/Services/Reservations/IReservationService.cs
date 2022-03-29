using PhaseCredit.Data.Entities.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.Services.Reservations
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetReservationAsync();

    }
}
