using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemV5.Models
{
    public class JourneyDetail
    {
        [Key]
        public int JourneyId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Flight Id is Required")]
        public string FlightId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Airline Name is Required")]
        public string AirlineName { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(30, ErrorMessage = "Minimum 3 Characters Required", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Journey Start Location is Required")]
        public string StartLoc { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(30, ErrorMessage = "Minimum 3 Characters Required", MinimumLength = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Journey End Location is Required")]
        public string EndLoc { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Journey Start Time is Required")]
        public DateTime StartTime { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Journey End Time is Required")]
        public DateTime EndTime { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Economy Class Price is Required")]
        public int EClassPrice { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Business Class Price is Required")]
        public int BClassPrice { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Flight Capacity is Required")]
        public int EClassCapacity { get; set; }
        public int EClassAvailableSeats { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Flight Capacity is Required")]
        public int BClassCapacity { get; set; }
        public int BClassAvailableSeats { get; set; }
    }
}
