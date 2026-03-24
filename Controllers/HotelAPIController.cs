using HotelBookingSystem.Entity.Model;
using HotelBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelAPIController : ControllerBase
    {
        private readonly IHotelTblServices _hotelTblService;

        public HotelAPIController(IHotelTblServices _hotelTblService)
        {
           this._hotelTblService = _hotelTblService;
        }
        // GET: api/<HotelAPIController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _hotelTblService.GetByAllHotelList());
        }

        // GET api/<HotelAPIController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _hotelTblService.GetByHotelId(id));
        }

        // POST api/<HotelAPIController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] HotelTbl Model)
        {
            return Ok(await _hotelTblService.AddHotel(Model));
        }

        // PUT api/<HotelAPIController>/5
        [HttpPut("{id}")]
        public async  Task<IActionResult> Put(int id, [FromBody] HotelTbl Model)
        {
            return Ok(await _hotelTblService.UpdateHotel(id, Model));
        }

        // DELETE api/<HotelAPIController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _hotelTblService.DeleteHotel(id));
        }
    }
}
