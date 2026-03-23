using HotelBookingSystem.Entity.Model;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBookingSystem.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Get()
        {
            return Ok(await _bookingTblServices.GetByBookingList());
        }

        // GET api/<BookingAPIController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _bookingTblServices.getByBookingId(id));
        }

        // POST api/<BookingAPIController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] BookingTbl Model)
        {
            return Ok(await _bookingTblServices.AddBooking(Model));
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
    }
}
