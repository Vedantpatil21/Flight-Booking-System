using FlightBookingSystemV5.Auth;
using FlightBookingSystemV5.Models;
using FlightBookingSystemV5.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Reflection;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace FlightBookingSystemV5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public SearchController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        //[Route("Search")]
        public async Task<IActionResult> Search(SearchPage searchPage)
        {
            DateTime sDate = DateTime.Parse(searchPage.StartDate);
            DateTime nDate = sDate.Date;
            DateTime tDate = sDate.AddDays(1).Date;
            List<JourneyDetail> res = context.JourneyDetails.Where(
                        bd => bd.StartLoc == searchPage.StartLoc && bd.EndLoc == searchPage.EndLoc
                        && bd.StartTime >= nDate && bd.StartTime < tDate
                    ).ToList();
            if(res.Count > 0)
            {
                //string resJson = "";
                ////var JsonOutput = JsonConvert.SerializeObject(new {  = jd });
                //var resJ = null;
                //foreach (var jd in res)
                //{
                //    resJson = resJson + JsonConvert.SerializeObject(
                //    new
                //    {
                //        flight = jd
                //    });
                //}
                var json = JsonSerializer.Serialize(res);
                return Ok(res);
                //return Ok(new Response { Status = "Success", Message = "Search Succeed!" });
            }
            //return Ok(new Response { Status = "Flights not available", Message = "Sorry, not any flight is available according to your requirements!" });
            return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Flights not available", Message = "Sorry, not any flight is available according to your requirements!" });


        }
    }
}
