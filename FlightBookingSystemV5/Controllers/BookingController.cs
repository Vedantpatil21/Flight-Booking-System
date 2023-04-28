using FlightBookingSystemV5.Auth;
using FlightBookingSystemV5.Models;
using FlightBookingSystemV5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlightBookingSystemV5.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("[controller]")]

    public class BookingController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext context;
        public BookingController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            this._userManager = userManager;
            this.context = context;
        }
        [HttpPost]
        //[Route("Booking")]
        public async Task<IActionResult> Booking([FromForm] BookingData bookingData)
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //var userId = user.Id;
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userData = _userManager.FindByIdAsync(userId);
            string userId = "7900fcc6-4328-4f20-a0da-46de7bfbd971";

            if (userId != null)
            {
                int sJId = bookingData.JourneyId;
                JourneyDetail journeyDetail = context.JourneyDetails.FirstOrDefault(jd => jd.JourneyId == sJId);
                BookingDetail bookingDetail = new BookingDetail();
                bookingDetail.UserName = bookingData.UserName;
                bookingDetail.Age = bookingData.Age;
                bookingDetail.Gender = bookingData.Gender;
                bookingDetail.UserId = userId;
                bookingDetail.JourneyId = sJId;

                if (bookingData.TicketType == "E" && journeyDetail.EClassAvailableSeats > 0)
                {
                    bookingDetail.SeatNo = "E" + (journeyDetail.EClassCapacity - journeyDetail.EClassAvailableSeats + 1);
                    journeyDetail.EClassAvailableSeats -= 1;
                }
                else
                {
                    bookingDetail.SeatNo = "B" + (journeyDetail.BClassCapacity - journeyDetail.BClassAvailableSeats + 1);
                    journeyDetail.BClassAvailableSeats -= 1;
                }

                context.BookingDetails.Add(bookingDetail);
                context.SaveChanges();
                if (bookingDetail.BookingId != 0)
                {
                    return Ok(new
                    {
                        Status = "Success",
                        Message = "Booking Successfull",
                        BookingId = bookingDetail.BookingId.ToString(),
                        SeatNo = bookingDetail.SeatNo,
                        Airline = journeyDetail.AirlineName
                    });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "User not signed in", Message = "Please Signin!" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Unable to book the ticket", Message = "Please try again!" });
        }
    }
}
