using PhaseCredit.Data.Entities.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Data.Abstract
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservationAsync();
    }
}
