using Microsoft.AspNetCore.Mvc;
using PhaseCredit.API.Test.Interfaces;
using PhaseCredit.Data.Entities.Reservations;
using System.Net;

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

            if (response.ResponseCode == HttpStatusCode.Unauthorized)
            {
                HttpContext.Session.SetString("message", response.ResponseMessage);
                return RedirectToAction("Login", "Authentication");
            }
            if (response.ResponseCode == HttpStatusCode.Forbidden)
            {
                HttpContext.Session.SetString("message", "You are not permitted to the resource.");
                return RedirectToAction("Login", "Authentication");
            }
            if (response.ResponseCode == HttpStatusCode.OK)
            {
                reservationList = response.Reservations;
            }
            return View(reservationList);
        }
        
    }
}
