using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlightBookingSystemV5.Models
{
    public class AirlineAuthority
    {
        [Key]
        public int Id { get; set; }
        //[Index(IsUnique = true)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Invalid Email Address.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Address is Required")]
        public string Email { get; set; }
        //[Index(IsUnique = true)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Airline Name is Required")]
        public string AirlineName { get; set; }
        [StringLength(50, MinimumLength = 8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
