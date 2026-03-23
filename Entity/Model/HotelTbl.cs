using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingSystem.Entity.Model
{
    [Table("HotelTbl")]
    public class HotelTbl
    {
        [Key]
        public int HotelId { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }

        public List<BookingTbl>? Bookings { get; set; }

    }
}
