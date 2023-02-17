using FlightControlWebAPI.Models;

namespace FlightControlWebAPI.Services.Interfaces
{
    public interface IFlightService
    {
        public Task<IEnumerable<Flight>> GetFlights();
        public Task<Flight> GetFlightById(int? id);

        public Task AddFlight(Flight flight);

        public Task DeleteFlight(int? id);

        public Task DeleteFlights();
        public Task UpdateFlight(int? id);

    }
}
