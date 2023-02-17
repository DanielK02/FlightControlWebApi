using FlightControlWebAPI.DAL;
using FlightControlWebAPI.Models;
using FlightControlWebAPI.Services.Interfaces;
using FlightControlWebAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace FlightControlWebAPI.Services
{
    public class FlightService : IFlightService
    {
        private readonly DataContext _data;
     //   private readonly IFlightControlService _flightControlService;
        private readonly ITerminalService _terminalService;
        
        public FlightService(DataContext dataContext, ITerminalService terminalService)
        {
            this._data = dataContext;
           // _flightControlService = flightService;
            _terminalService = terminalService;
        }       

        public async Task<IEnumerable<Flight>> GetFlights()
        {
            return await _data.Flights.ToListAsync();
        }

        public async Task<Flight> GetFlightById(int? id)
        {
            return await _data.Flights.FirstOrDefaultAsync(f => f.Id == id.Value);
        }

        public async Task AddFlight(Flight flight)
        {
            _data.Flights.Add(flight);
            await _data.SaveChangesAsync();

            await _terminalService.ApproachAirport(flight);
            // Implement Queue
            // Add to queue
            //await _flightService.EnterTerminal1(flight);
        }

        public async Task DeleteFlight(int? id)
        {
            var flight = await _data.Flights.FindAsync(id);
            if (flight != null)
            {
                _data.Flights.Remove(flight);
                await _data.SaveChangesAsync();
            }
            // what if not found
        }

        public async Task UpdateFlight(int? id)
        {
            var flight = await _data.Flights.FindAsync(id);
            if (flight != null)
            {
                if (id == flight.Id)
                {
                    _data.Flights.Update(flight);
                    await _data.SaveChangesAsync();
                }
            }

            // rest here
        }

        public async Task DeleteFlights()
        {
            var flights = await _data.Flights.ToListAsync();
            foreach (var flight in flights)
            {
                _data.Flights.Remove(flight);
            }
            await _data.SaveChangesAsync();
        }




    }
}
