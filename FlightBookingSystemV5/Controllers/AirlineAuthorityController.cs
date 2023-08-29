using FlightBookingSystemV5.Auth;
using FlightBookingSystemV5.Models;
using FlightBookingSystemV5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingSystemV5.Controllers
{
    //[Authorize(Roles = "Airline")]
    [ApiController]
    [Route("api/[controller]")]
        
    public class AirlineAuthorityController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly ApplicationDbContext context;
        public AirlineAuthorityController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JourneyDetail>>> GetAirlineDetail()
        {
            if (context.JourneyDetails == null)
            {
                return NotFound();
            }
            return await context.JourneyDetails.ToListAsync();
        }

        [Authorize(Roles = "Airline")]
        [Route("GetAllFlightOfAirline")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<JourneyDetail>>> GetAirlineDetail(AirlineData airlineData)
        {
            if (context.JourneyDetails == null)
            {
                return NotFound("No Any Flight To Show!");
            }

            return await context.JourneyDetails.Where(j => j.AirlineName == airlineData.AirlineName).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JourneyDetail>> GetAirlineDetail(int id /*ActionResult<IEnumerable<JourneyDetail>> flight*/)
        {
            if (context.JourneyDetails == null)
            {
                return NotFound();
            }
            var Flight = await context.JourneyDetails.FindAsync(id);
            if (Flight == null)
            {
                return NotFound();
            }
            return Flight;
        }

        [Authorize(Roles = "Airline")]
        [HttpPost]
        public async Task<ActionResult<JourneyDetail>> PostTrainDetail(JourneyDetail flight)
        {
            context.JourneyDetails.Add(flight);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAirlineDetail), new { id = flight.JourneyId }, flight);
        }

        [Authorize(Roles = "Airline")]
        [HttpPut]
        public async Task<IActionResult> PutflightDetail(JourneyDetail flight)
        {
            //if (id != flight.JourneyId)
            //{
            //    return BadRequest();
            //}
            context.Entry(flight).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }

        [Authorize(Roles = "Airline")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteflightDetail(int id)
        {
            if (context.JourneyDetails == null)
            {
                return NotFound();
            }
            var flight = await context.JourneyDetails.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            context.JourneyDetails.Remove(flight);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
