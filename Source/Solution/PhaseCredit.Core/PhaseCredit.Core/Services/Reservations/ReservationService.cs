using PhaseCredit.Data.Entities.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.Services.Reservations
{
    public class ReservationService: IReservationService
    {
        public async Task<List<Reservation>> GetReservations()
        {
            List<Reservation> rList = new List<Reservation> {
            new Reservation { Id=1, Name = "Ankit", StartLocation = "New York", EndLocation="Beijing" },
            new Reservation { Id=2, Name = "Bobby", StartLocation = "New Jersey", EndLocation="Boston" },
            new Reservation { Id=3, Name = "Jacky", StartLocation = "London", EndLocation="Paris" }
            };
            return await Task.FromResult(rList);
        }

        public async Task<IEnumerable<Reservation>> GetReservationAsync()
        {
            var reservations = await GetReservations();
            return await Task.FromResult(reservations);
        }
    }
}
