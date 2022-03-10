using Microsoft.AspNetCore.Mvc;
using PhaseCredit.API.Test.Interfaces;
using PhaseCredit.Data.Entities.Reservations;

namespace PhaseCredit.API.Test.Controllers
{
    public class TestReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        public TestReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
       
        public async Task<IActionResult> FlightReservation()
        {
            var jwt = Request.Cookies["jwtCookie"];
            var response =  await _reservationService.GetListAsync(jwt);
            List<Reservation> reservationList = new List<Reservation>();

            if (response.ResponseCode == StatusCodes.Status401Unauthorized)
            {
                HttpContext.Session.SetString("message", "Please Login again");
                return RedirectToAction("Login");
            }
            if (response.ResponseCode == StatusCodes.Status403Forbidden)
            {
                HttpContext.Session.SetString("message", "You are not permitted to the resource.");
                return RedirectToAction("Login");
            }
            if (response.ResponseCode == StatusCodes.Status200OK)
            {
                reservationList = response.Reservations;
            }
            return View(reservationList);
        }
        
    }
}
