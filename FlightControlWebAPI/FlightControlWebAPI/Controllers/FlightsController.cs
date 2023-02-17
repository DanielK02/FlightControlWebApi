using FlightControlWebAPI.DAL;
using FlightControlWebAPI.Models;
using FlightControlWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightControlWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        public FlightsController(IFlightService data) => this._flightService = data;

        [HttpGet]
        public async Task<IEnumerable<Flight>> GetFlights() => await _flightService.GetFlights();

        [HttpPost]
        public async Task<IActionResult> AddFlight(Flight flight)
        {
            await _flightService.AddFlight(flight);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            await _flightService.DeleteFlight(id);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllFlights()
        {
            await _flightService.DeleteFlights();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id)
        {
            await _flightService.UpdateFlight(id);
            return NoContent();
        }
    }
}
