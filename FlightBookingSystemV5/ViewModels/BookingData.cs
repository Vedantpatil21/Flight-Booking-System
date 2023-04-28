using FlightBookingSystemV5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemV5.ViewModels
{
    public class BookingData
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string TicketType { get; set; }
        public int JourneyId { get; set; }
    }
}
