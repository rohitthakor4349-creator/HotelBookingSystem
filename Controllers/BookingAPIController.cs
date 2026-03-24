using HotelBookingSystem.Entity.Model;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [Route("api/Hotels/{HotelId}/Bookings")]
    [ApiController]
    public class BookingAPIController : ControllerBase
    {

        private readonly IBookingTblServices _bookingTblServices;

        public BookingAPIController(IBookingTblServices _bookingTblServices)
        {
            this._bookingTblServices = _bookingTblServices;
        }
        // GET: api/<BookingAPIController>
        [HttpGet]
        public async Task<IActionResult> Get(int HotelId,DateTime? startDate,DateTime? endDate)
        {
            var data = await _bookingTblServices.GetByBookingList();

            var result = data.Where(b => b.HotelId == HotelId);

            if (startDate.HasValue && endDate.HasValue)
            {
                result = result.Where(b => b.CheckInDate >= startDate && b.CheckOutDate <= endDate);
            }
            return Ok(result);
          
        }

        // GET api/<BookingAPIController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {


            return Ok(await _bookingTblServices.getByBookingId(id));
        }

        // POST api/<BookingAPIController>
        [HttpPost]
        [Authorize(Roles = "staff,reception")]
        public async Task<IActionResult> Post(int HotelId, [FromForm] BookingTbl Model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Model.HotelId = HotelId;
            Model.CreatedBy = userId;

            var Data = await _bookingTblServices.AddBooking(Model);
            SendNotification(Model);
            return Ok(Data);
        }

        // PUT api/<BookingAPIController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] BookingTbl Model)
        {
            return Ok(await _bookingTblServices.UpdateBooking(id, Model));
        }

        // DELETE api/<BookingAPIController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _bookingTblServices.DeleteBooking(id));
        }

        private void SendNotification(BookingTbl booking)
        {
            Console.WriteLine($" New Booking Created for HotelId: {booking.HotelId}, Guest: {booking.guestName}");
        }
    }
}
