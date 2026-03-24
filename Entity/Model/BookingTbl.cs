using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace HotelBookingSystem.Entity.Model
{
    [Table("BookingTbl")]
    public class BookingTbl
    {
        [Key]
        public int BookingId { get; set; }
        public int HotelId { get; set; }
        public string? guestName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string? CreatedBy { get; set; }

        public HotelTbl? Hotel { get; set; }
    }


}
