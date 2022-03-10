using PhaseCredit.Data.Entities.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.DTOs.Resrvations
{
    public class ListReservationResponse:Response
    {
        public List<Reservation> Reservations { get; set; }
    }
}
